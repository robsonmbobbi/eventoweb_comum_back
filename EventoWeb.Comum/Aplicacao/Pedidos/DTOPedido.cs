using EventoWeb.Comum.Negocio.Entidades;

namespace EventoWeb.Comum.Aplicacao.Pedidos;

public class DTOPedido
{
    public IList<int> IdsInscricoes { get; set; }
    public double Valor { get; set; }
    public EnumFormaPagamento FormaPagamento { get; set; }
    public string NomePagador { get; set; }
    public string CPFPagador { get; set; }
    public string CelularPagador { get; set; }
    public string EmailPagador { get; set; }
}