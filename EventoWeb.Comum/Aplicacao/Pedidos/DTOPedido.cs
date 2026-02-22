using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Servicos;

namespace EventoWeb.Comum.Aplicacao.Pedidos;

public class DTOPedido
{
    public IList<int> IdsInscricoes { get; set; }
    public decimal Valor { get; set; }
    public EnumTipoPedido Tipo { get; set; }
    public int? IdFormaPagamento { get; set; }
    public string NomePagador { get; set; }
    public string CPFPagador { get; set; }
    public string CelularPagador { get; set; }
    public string EmailPagador { get; set; }
    public DadosCartaoCredito? DadosCartaoCredito { get; set; }
    public string? Motivo { get; set; }
}