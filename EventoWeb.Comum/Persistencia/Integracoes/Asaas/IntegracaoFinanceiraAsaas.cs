using AsaasClient;
using AsaasClient.Core;
using AsaasClient.Models.Common.Enums;
using AsaasClient.Models.Customer;
using AsaasClient.Models.Payment;
using AsaasClient.Models.Payment.Enums;
using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Entidades.Financeiro;
using EventoWeb.Comum.Negocio.Entidades.IntegracaoFinanceira;
using EventoWeb.Comum.Negocio.Servicos;

namespace EventoWeb.Comum.Persistencia.Integracoes.Asaas
{
    public class IntegracaoFinanceiraAsaas : IIntegracaoExterna
    {
        private readonly string _appName = "EventoWeb4";
        private readonly AsaasEnvironment _environment = AsaasEnvironment.PRODUCTION;

        public async Task<DadosRetornoIntegracaoExterna?> ConsultarCobranca(IntegradorFinanceiro integrador, string identificador)
        {
            var asaasApi = CriarAsaasApi(integrador);

            var paymentResponse = await asaasApi.Payment.Find(identificador);
            if (!paymentResponse.WasSucessfull())
                throw paymentResponse.TratarErros();

            var payment = paymentResponse.Data;
            var retorno = new DadosRetornoIntegracaoExterna
            {
                IdTransacao = payment.Id,
                Status = payment.Status switch { 
                    PaymentStatus.RECEIVED => EnumStatusTransacao.Recebida,
                    PaymentStatus.OVERDUE => EnumStatusTransacao.Cancelada,
                    _ => EnumStatusTransacao.Pendente
                },
                TipoTransacao = payment.BillingType switch { 
                    BillingType.PIX => EnumTipoPagamento.PIX,
                    BillingType.CREDIT_CARD => EnumTipoPagamento.CartaoCredito,
                    _ => throw new Exception("Tipo de pagamento não suportado: " + payment.BillingType)
                },
                Valor = payment.Value,
            };

            switch (payment.BillingType)
            {
                case BillingType.PIX:
                    if (payment.Status == PaymentStatus.PENDING)
                        await AdicionarDadosPix(asaasApi, payment.Id, retorno);
                    break;
                case BillingType.CREDIT_CARD:
                    retorno.LinkPagamento = payment.InvoiceUrl;
                    break;
            }

            return retorno;
        }

        public async Task<DadosRetornoIntegracaoExterna> CriarCobranca(IntegracaoFinanceiraPorFormaPag integrador, Pedido pedido, int? numeroParcelas)
        {
            var asaasApi = CriarAsaasApi(integrador.Integrador);
            var customer = await ObterOuCriarCliente(asaasApi, pedido.Pagador.CPF.Numero, pedido.Pagador.Nome.Nome);

            var paymentRequest = ConstruirPaymentRequest(
                customer.Id,
                pedido.Valor.Valor,
                integrador.FormaPagamento.Tipo,
                integrador.FormaPagamento.NrParcelasMinima,
                numeroParcelas,
                $"Pagamento inscrições {pedido.Inscricoes.First().Evento.Nome.Nome}. Pedido: {pedido.Id}"
            );

            var paymentResponse = await asaasApi.Payment.Create(paymentRequest);
            if (!paymentResponse.WasSucessfull())
                throw paymentResponse.TratarErros();

            return await ProcessarRespostaCobranca(asaasApi, paymentResponse.Data, integrador.FormaPagamento.Tipo);
        }

        public async Task<DadosRetornoIntegracaoExterna> CriarCobrancaPorConta(IntegracaoFinanceiraPorFormaPag integrador, Conta conta, decimal valor, EnumTipoPagamento tipoPagamento, int? numeroParcelas)
        {
            var asaasApi = CriarAsaasApi(integrador.Integrador);
            var customer = await ObterOuCriarCliente(asaasApi, conta.Pessoa.CPF.Numero, conta.Pessoa.Nome.Nome);

            var paymentRequest = ConstruirPaymentRequest(
                customer.Id,
                valor,
                tipoPagamento,
                integrador.FormaPagamento.NrParcelasMinima,
                numeroParcelas,
                $"Pagamento - Conta ID: {conta.Id}"
            );

            var paymentResponse = await asaasApi.Payment.Create(paymentRequest);
            if (!paymentResponse.WasSucessfull())
                throw paymentResponse.TratarErros();

            return await ProcessarRespostaCobranca(asaasApi, paymentResponse.Data, tipoPagamento);
        }

        /// <summary>
        /// Cria instância da API Asaas com as configurações necessárias
        /// </summary>
        private AsaasApi CriarAsaasApi(IntegradorFinanceiro integrador)
        {
            return new AsaasApi(new(integrador.TokenAcesso, _appName, _environment));
        }

        /// <summary>
        /// Obtém cliente existente ou cria novo cliente
        /// </summary>
        private async Task<Customer> ObterOuCriarCliente(AsaasApi asaasApi, string cpfCnpj, string nome)
        {
            var customerResponse = await asaasApi.Customer.List(1, 10, new CustomerListFilter { CpfCnpj = cpfCnpj });

            if (!customerResponse.WasSucessfull())
                throw customerResponse.TratarErros();

            var customer = customerResponse.Data.FirstOrDefault();
            if (customer != null)
                return customer;

            var createCustomerResponse = await asaasApi.Customer.Create(
                new CreateCustomerRequest { Name = nome, CpfCnpj = cpfCnpj });

            if (!createCustomerResponse.WasSucessfull())
                throw createCustomerResponse.TratarErros();

            return createCustomerResponse.Data;
        }

        /// <summary>
        /// Constrói o objeto CreatePaymentRequest com configurações apropriadas
        /// </summary>
        private CreatePaymentRequest ConstruirPaymentRequest(
            string customerId,
            decimal valor,
            EnumTipoPagamento tipoPagamento,
            int nrParcelasMinima,
            int? numeroParcelas,
            string descricao)
        {
            var paymentRequest = new CreatePaymentRequest
            {
                CustomerId = customerId,
                Value = valor,
                DueDate = DateTime.Now.AddDays(1),
                Description = descricao
            };

            switch (tipoPagamento)
            {
                case EnumTipoPagamento.PIX:
                    paymentRequest.BillingType = BillingType.PIX;
                    break;
                case EnumTipoPagamento.CartaoCredito:
                    paymentRequest.BillingType = BillingType.CREDIT_CARD;
                    if (nrParcelasMinima > 1)
                    {
                        paymentRequest.InstallmentCount = numeroParcelas;
                        paymentRequest.TotalValue = valor;
                    }
                    break;
                default:
                    throw new Exception($"Tipo de pagamento não suportado: {tipoPagamento}");
            }

            return paymentRequest;
        }

        /// <summary>
        /// Processa a resposta da criação de cobrança, obtendo dados adicionais conforme necessário
        /// </summary>
        private async Task<DadosRetornoIntegracaoExterna> ProcessarRespostaCobranca(
            AsaasApi asaasApi,
            Payment payment,
            EnumTipoPagamento tipoPagamento)
        {
            var retorno = new DadosRetornoIntegracaoExterna
            {
                IdTransacao = payment.Id,
                Status = EnumStatusTransacao.Pendente,
                TipoTransacao = tipoPagamento,
                Valor = payment.Value
            };

            switch (tipoPagamento)
            {
                case EnumTipoPagamento.PIX:
                    await AdicionarDadosPix(asaasApi, payment.Id, retorno);
                    break;
                case EnumTipoPagamento.CartaoCredito:
                    retorno.LinkPagamento = payment.InvoiceUrl;
                    break;
            }

            return retorno;
        }

        /// <summary>
        /// Adiciona dados do PIX (QR Code e código de cópia e cola) ao retorno
        /// </summary>
        private async Task AdicionarDadosPix(AsaasApi asaasApi, string paymentId, DadosRetornoIntegracaoExterna retorno)
        {
            var pixResponse = await asaasApi.Payment.GetPixQrCode(paymentId);
            if (!pixResponse.WasSucessfull())
                throw pixResponse.TratarErros();

            var pixData = pixResponse.Data;
            retorno.ImagemQRCodePixBase64 = pixData.EncodedImage;
            retorno.PixCopiaECola = pixData.Payload;
        }
    }
}
