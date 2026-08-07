using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Entidades.Financeiro;
using EventoWeb.Comum.Negocio.Entidades.IntegracaoFinanceira;
using EventoWeb.Comum.Negocio.ObjetosValor;
using static EventoWeb.Comum.Testes.Negocio.Fixtures.EventosFixtures;
using static EventoWeb.Comum.Testes.Negocio.Fixtures.PessoasFixtures;

namespace EventoWeb.Comum.Testes.Negocio.Fixtures
{
    /// <summary>
    /// Fixtures para criação de dados de teste reutilizáveis relacionados a Financeiro
    /// </summary>
    public static class FinanceiroFixtures
    {
        public static ContaBancaria CriarContaBancariaValida(string nome = "Banco Brasil - Corrente", Evento? evento = null)
        {
            evento ??= CriarEventoValido();
            return new ContaBancaria(evento, new String200(nome));
        }

        public static Conta CriarContaValida(
            Pessoa? pessoa = null,
            EnumTipoTransacao tipo = EnumTipoTransacao.Receita,
            decimal valor = 500.00m,
            DateTime? dataVencimento = null,
            Evento? evento = null)
        {
            pessoa ??= CriarPessoaValida();
            evento ??= CriarEventoValido();
            dataVencimento ??= DateTime.Now.AddDays(30);

            return new Conta(
                evento,
                pessoa,
                tipo,
                new ValorMonetario(valor),
                dataVencimento.Value
            );
        }

        public static Transacao CriarTransacaoValida(
            ContaBancaria? contaBancaria = null,
            EnumTipoTransacao tipo = EnumTipoTransacao.Receita,
            decimal valor = 300.00m,
            string? descricao = null,
            Evento? evento = null)
        {
            contaBancaria ??= CriarContaBancariaValida(evento: evento);
            evento ??= contaBancaria.Evento;
            descricao ??= "Pagamento recebido";

            return new Transacao(
                evento,
                tipo,
                contaBancaria,
                DateTime.Now,
                new ValorMonetario(valor),
                new String200(descricao)
            );
        }

        public static TransacaoConta CriarTransacaoContaValida(
            Conta? conta = null,
            decimal valorTransacao = 400.00m,
            decimal? multa = null,
            decimal? juros = null,
            decimal? desconto = null)
        {
            conta ??= CriarContaValida();

            return new TransacaoConta(
                CriarContaBancariaValida(evento: conta.Evento),
                conta,
                DateTime.Now,
                new ValorMonetario(valorTransacao),
                multa.HasValue ? new ValorMonetario(multa.Value) : null,
                juros.HasValue ? new ValorMonetario(juros.Value) : null,
                desconto.HasValue ? new ValorMonetario(desconto.Value) : null
            );
        }

        public static FormaPagamento CriarFormaPagamentoValida(
            string nome = "Cartão de Crédito",
            EnumTipoPagamento tipoPagamento = EnumTipoPagamento.CartaoCredito,
            Evento? evento = null)
        {
            evento ??= CriarEventoValido();
            return new FormaPagamento(evento, new String200(nome), tipoPagamento);
        }

        public static IntegradorFinanceiro CriarIntegradorFinanceiroValido(
            ContaBancaria? contaBancaria = null,
            string tokenAcesso = "token_teste_123",
            EnumIntegracaoExterna integracaoExterna = EnumIntegracaoExterna.Asaas,
            Evento? evento = null)
        {
            evento ??= CriarEventoValido();
            contaBancaria ??= CriarContaBancariaValida(evento: evento);

            return new IntegradorFinanceiro(
                evento,
                contaBancaria,
                new String1000(tokenAcesso),
                integracaoExterna
            );
        }

        public static IntegracaoFinanceiraPorFormaPag CriarIntegracaoFinanceiraPorFormaPagValida(
            IntegradorFinanceiro? integrador = null,
            FormaPagamento? formaPagamento = null)
        {
            integrador ??= CriarIntegradorFinanceiroValido();
            formaPagamento ??= CriarFormaPagamentoValida();

            return new IntegracaoFinanceiraPorFormaPag(integrador.Evento, integrador, formaPagamento);
        }

        public static RegistroIntegracaoFinanceira CriarRegistroIntegracaoFinanceiraValida(
            IntegradorFinanceiro? integrador = null,
            Conta? conta = null,
            decimal valor = 100.00m,
            EnumTipoPagamento tipoPagamento = EnumTipoPagamento.CartaoCredito,
            string identificacaoNoIntegrador = "INTEG_001",
            int? numeroParcelas = null)
        {
            integrador ??= CriarIntegradorFinanceiroValido();
            conta ??= CriarContaValida(evento: integrador.Evento);

            return new RegistroIntegracaoFinanceira(
                integrador.Evento,
                integrador,
                conta,
                new ValorMonetario(valor),
                tipoPagamento,
                new String1000(identificacaoNoIntegrador),
                numeroParcelas.HasValue ? new InteiroPositivo(numeroParcelas.Value) : null
            );
        }

        public static RegistroIntegracaoLog CriarRegistroIntegracaoLogValido(
            RegistroIntegracaoFinanceira? registro = null,
            string mensagem = "Teste de integração",
            EnumTipoLog tipo = EnumTipoLog.Info,
            string? dados = null)
        {
            registro ??= CriarRegistroIntegracaoFinanceiraValida();

            return new RegistroIntegracaoLog(
                registro,
                new String500(mensagem),
                tipo,
                dados != null ? new String4000(dados) : null
            );
        }
    }
}
