using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Repositorios;

namespace EventoWeb.Comum.Aplicacao.Pedidos;

public class AppPedidoInclusao : AppBase
{
    private readonly IInscricoes m_Inscricoes;
    private readonly IPersistencia<Pedido> m_Pedidos;

    public AppPedidoInclusao(IContexto contexto, IInscricoes inscricoes, IPersistencia<Pedido> pedidos) : base(contexto)
    {
        m_Inscricoes = inscricoes;
        m_Pedidos = pedidos;
    }

    public void Incluir(DTOPedido dtoPedido)
    {
        Pedido? pedido = null;
        ExecutarSeguramente(() =>
        {
            pedido = new Pedido(
                dtoPedido.IdsInscricoes.Select(id =>
                    m_Inscricoes.Obter(id) ?? throw new Exception($"Inscrição não encontrada com o id {id}")),
                dtoPedido.Valor,
                dtoPedido.FormaPagamento
            );
            
            m_Pedidos.Incluir(pedido);
        });
        
        if (dtoPedido.FormaPagamento == EnumFormaPagamento.DebitoAplicado)
        {
            // Integração com os meios de pagamento (negócio e empresa)
            //https://github.com/cloviscoli/asaas-sdk-net/tree/master/AsaasClient/Core
        }
    }
}