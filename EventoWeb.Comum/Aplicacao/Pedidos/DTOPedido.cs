using EventoWeb.Comum.Negocio.Entidades;

namespace EventoWeb.Comum.Aplicacao.Pedidos;

public class DTOPedido
{
    public IList<int> IdsInscricoes { get; set; }
    public double Valor { get; set; }
    public EnumFormaPagamento FormaPagamento { get; set; }
}