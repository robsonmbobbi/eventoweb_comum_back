using AsaasClient;
using AsaasClient.Core;
using AsaasClient.Models.Common;
using AsaasClient.Models.Common.Enums;
using AsaasClient.Models.Customer;
using AsaasClient.Models.Payment;
using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Entidades.IntegracaoFinanceira;
using EventoWeb.Comum.Negocio.Servicos;

namespace EventoWeb.Comum.Persistencia.Integracoes.Asaas
{
    public class IntegracaoFinanceiraAsaas : IIntegracaoExterna
    {
        public async Task<DadosRetornoIntegracaoExterna> Enviar(IntegracaoFinanceiraPorFormaPag integrador, Pedido pedido, DadosCartaoCredito? dadosCartaoCredito)
        {
            AsaasApi asaasApi = new(new(integrador.Integrador.TokenAcesso, "EventoWeb4", AsaasEnvironment.SANDBOX));
            var customerResponse = await asaasApi.Customer.List(
                1,
                10,
                new CustomerListFilter
                {
                    CpfCnpj = pedido.Pagador.CPF.Numero
                });

            if (!customerResponse.WasSucessfull())
                throw customerResponse.TratarErros();

            Customer? customer = customerResponse.Data.FirstOrDefault();
            if (customer == null)
            {
                var createCustomerResponse = await asaasApi.Customer.Create(
                    new CreateCustomerRequest
                    {
                        Name = pedido.Pagador.Nome.Nome,
                        CpfCnpj = pedido.Pagador.CPF.Numero
                    });

                if (!createCustomerResponse.WasSucessfull())
                    throw createCustomerResponse.TratarErros();

                customer = createCustomerResponse.Data;
            }            

            var paymentRequest = new CreatePaymentRequest
            {
                CustomerId = customer.Id,
                Value = pedido.Valor.Valor,
                DueDate = DateTime.Now.AddDays(1),
                Description = $"Pagamento inscrições {pedido.Inscricoes.First().Evento.Nome}. Pedido: {pedido.Id}"
            };

            switch (integrador.TipoIntegracao)
            {
                case EnumTipoIntegracao.PIX:
                    paymentRequest.BillingType = BillingType.PIX;
                    break;
                case EnumTipoIntegracao.CreditoParcelado:
                case EnumTipoIntegracao.CreditoVista:
                    paymentRequest.BillingType = BillingType.CREDIT_CARD;

                    paymentRequest.CreditCard = new CreditCardRequest
                    {
                        HolderName = dadosCartaoCredito?.NomeImpressoCartao,
                        Number = dadosCartaoCredito?.NumeroCartao,
                        ExpiryMonth = dadosCartaoCredito?.MesExpiracao,
                        ExpiryYear = dadosCartaoCredito?.AnoExpiracao,
                        Ccv = dadosCartaoCredito?.CodigoSeguranca
                    };

                    paymentRequest.CreditCardHolderInfo = new CreditCardHolderInfoRequest
                    {
                        Name = dadosCartaoCredito?.NomeTitular,
                        Email = dadosCartaoCredito?.EmailTitular,
                        CpfCnpj = dadosCartaoCredito?.CPFouCNPJTitular,
                        AddressNumber = dadosCartaoCredito?.NumeroEnderecoTitular,
                        Phone = dadosCartaoCredito?.TelefoneTitular,
                        PostalCode = dadosCartaoCredito?.CEPTitular
                    };

                    if (integrador.TipoIntegracao == EnumTipoIntegracao.CreditoParcelado)
                    {
                        paymentRequest.InstallmentCount = dadosCartaoCredito?.NumeroParcelas;
                        paymentRequest.TotalValue = pedido.Valor.Valor;
                    }
                    break;
                default:
                    throw new Exception("Tipo de integração não suportado: " + integrador.TipoIntegracao);
            }

            var paymentResponse = await asaasApi.Payment.Create(paymentRequest);
            if (!paymentResponse.WasSucessfull())
                throw paymentResponse.TratarErros();

            var payment = paymentResponse.Data;
            var retorno = new DadosRetornoIntegracaoExterna
            {
                IdTransacao = payment.Id,
                Status = EnumStatusTransacao.Pendente,
                TipoTransacao = integrador.TipoIntegracao
            };

            if (integrador.TipoIntegracao == EnumTipoIntegracao.PIX)
            {
                var pixResponse = await asaasApi.Payment.GetPixQrCode(payment.Id);
                if (pixResponse.WasSucessfull())
                {
                    var pixData = pixResponse.Data;
                    retorno.ImagemQRCodePixBase64 = pixData.EncodedImage;
                    retorno.PixCopiaECola = pixData.Payload;
                }
                else
                {
                    throw pixResponse.TratarErros();
                }
            }

            return retorno;
        }
    }
}
