using EventoWeb.Comum.Negocio.Entidades.Financeiro;
using EventoWeb.Comum.Negocio.ObjetosValor;

namespace EventoWeb.Comum.Aplicacao.FormasPagamento
{
    public class DTOFormaPagamento
    {
        public required int Id { get; set; }
        public required string Nome{ get; set; }
        public required int NrParcelasMinima { get; set; }
        public required int NrParcelasMaxima { get; set; }
        public required EnumTipoPagamento Tipo { get; set; }
    }
}
