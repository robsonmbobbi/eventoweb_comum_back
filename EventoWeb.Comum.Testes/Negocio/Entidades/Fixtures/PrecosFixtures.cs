using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Entidades.Financeiro;
using EventoWeb.Comum.Negocio.ObjetosValor;
using static EventoWeb.Comum.Testes.Negocio.Fixtures.EventosFixtures;

namespace EventoWeb.Comum.Testes.Negocio.Fixtures
{
    /// <summary>
    /// Fixtures para criação de dados de teste reutilizáveis relacionados a Preço
    /// </summary>
    public static class PrecosFixtures
    {
        public static PrecoInscricao CriarPrecoInscricaoValido(Evento? evento = null)
        {
            evento ??= CriarEventoValido();
            return new PrecoInscricao(evento, new InteiroPositivo(17));
        }

        public static FormaPagamento CriarFormaPagamentoValida(
            string nome = "Cartão de Crédito",
            EnumTipoPagamento tipoPagamento = EnumTipoPagamento.CartaoCredito)
        {
            return new FormaPagamento(
                new String200(nome),
                tipoPagamento
            );
        }

        public static FormaPagamento CriarFormaPagamentoComParcelamento(
            int minParcelas = 1,
            int maxParcelas = 12)
        {
            var forma = CriarFormaPagamentoValida();
            forma.DefinirParcelas(new IntervaloInteiroPositivo(minParcelas, maxParcelas));
            return forma;
        }
    }
}
