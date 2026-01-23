using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Repositorios;

namespace EventoWeb.Comum.Aplicacao.Pedidos;

public class AppPedidoInclusao : AppBase
{
    private readonly IInscricoes m_Inscricoes;
    private readonly IPersistencia<Pedido> m_Pedidos;
    private readonly IPessoas m_Pessoas;

    public AppPedidoInclusao(
        IContexto contexto, 
        IInscricoes inscricoes,
        IPersistencia<Pedido> pedidos,
        IPessoas pessoas) : base(contexto)
    {
        m_Inscricoes = inscricoes;
        m_Pedidos = pedidos;
        m_Pessoas = pessoas;
    }

    public void Incluir(DTOPedido dtoPedido)
    {
        Pedido? pedido = null;
        ExecutarSeguramente(() =>
        {
            var pessoa = GerenciarPessoa(dtoPedido);
            
            pedido = new Pedido(
                pessoa,
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
    
    private Pessoa GerenciarPessoa(DTOPedido dtoPedido)
    {
        var ehInclusao = false;
        var pessoa = m_Pessoas.ObterPorCPF(dtoPedido.CPFPagador);
        if (pessoa == null)
        {
            ehInclusao = true;
            
            pessoa = new Pessoa(
                new CPF(dtoPedido.CPFPagador),
                new NomeCompleto(dtoPedido.NomePagador),
                new EMail(dtoPedido.EmailPagador),
                new Telefone(dtoPedido.CelularPagador)
            );
        }
        else
        {
            pessoa.Nome = new NomeCompleto(dtoPedido.NomePagador);
            pessoa.Email = new EMail(dtoPedido.EmailPagador);
            pessoa.CelularWP = new Telefone(dtoPedido.CelularPagador);
        }

        if (ehInclusao)
        {
            m_Pessoas.Incluir(pessoa);
        }
        else
        {
            m_Pessoas.Atualizar(pessoa);
        }

        return pessoa;
    }
}