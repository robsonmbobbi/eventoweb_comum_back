using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Entidades.Financeiro;
using EventoWeb.Comum.Negocio.ObjetosValor;
using EventoWeb.Comum.Negocio.Repositorios;

namespace EventoWeb.Comum.Aplicacao.Pedidos;

public class AppPedidoInclusao : AppBase
{
    private readonly IInscricoes m_Inscricoes;
    private readonly IPersistencia<Pedido> m_Pedidos;
    private readonly IPessoas m_Pessoas;
    private readonly IPersistencia<FormaPagamento> m_FormasPagamento;

    public AppPedidoInclusao(
        IContexto contexto, 
        IInscricoes inscricoes,
        IPersistencia<Pedido> pedidos,
        IPersistencia<FormaPagamento> formasPagamento,
        IPessoas pessoas) : base(contexto)
    {
        m_Inscricoes = inscricoes;
        m_Pedidos = pedidos;
        m_Pessoas = pessoas;
        m_FormasPagamento = formasPagamento;
    }

    public void Incluir(DTOPedido dtoPedido)
    {
        Pedido? pedido = null;
        ExecutarSeguramente(() =>
        {
            var pessoa = GerenciarPessoa(dtoPedido);
            FormaPagamento? forma = null;

            if (dtoPedido.Tipo == EnumTipoPedido.Debito)
            {
                forma = dtoPedido.IdFormaPagamento.HasValue
                    ? m_FormasPagamento.Obter(dtoPedido.IdFormaPagamento.Value)
                    : throw new Exception("Forma de pagamento deve ser informada para pedidos do tipo débito.");
            }
            
            pedido = new Pedido(
                pessoa,
                dtoPedido.IdsInscricoes.Select(id =>
                    m_Inscricoes.Obter(id) ?? throw new Exception($"Inscrição não encontrada com o id {id}")),
                new ValorMonetario(dtoPedido.Valor),
                dtoPedido.Tipo,
                forma
            );
            
            m_Pedidos.Incluir(pedido);
        });
        
        if (dtoPedido.Tipo == EnumTipoPedido.Debito)
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