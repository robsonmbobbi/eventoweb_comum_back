using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Entidades.Financeiro;
using EventoWeb.Comum.Negocio.Entidades.IntegracaoFinanceira;
using EventoWeb.Comum.Negocio.ObjetosValor;
using EventoWeb.Comum.Negocio.Repositorios;
using EventoWeb.Comum.Negocio.Servicos;
using EventoWeb.Comum.Negocio.Servicos.Notificacoes.Inscricoes;
using EventoWeb.Comum.Negocio.Servicos.Notificacoes.Pedidos;

namespace EventoWeb.Comum.Aplicacao.Pedidos;

public class AppPedidoInclusao : AppBase
{
    private readonly IInscricoes m_Inscricoes;
    private readonly IPedidos m_Pedidos;
    private readonly IPessoas m_Pessoas;
    private readonly IFormasPagamento m_FormasPagamento;
    private readonly IIntegracaoFinanceiraPorFormasPagamentos m_Integracoes;
    private readonly IDictionary<EnumIntegracaoExterna, IIntegracaoExterna> m_IntegracoesExternas;
    private readonly IRegistrosIntegracoesFinanceiras m_RegistrosIntegracao;
    private readonly IModelosMensagemNotificacao m_ModelosNotificacao;
    private readonly IMensagens m_Mensagens;

    public AppPedidoInclusao(
        IContexto contexto, 
        IInscricoes inscricoes,
        IPedidos pedidos,
        IFormasPagamento formasPagamento,
        IPessoas pessoas,
        IDictionary<EnumIntegracaoExterna, IIntegracaoExterna> integracoesExternas, 
        IIntegracaoFinanceiraPorFormasPagamentos integracoes,
        IRegistrosIntegracoesFinanceiras registrosIntegracao,
        IModelosMensagemNotificacao modelosNotificacao,
        IMensagens mensagens) : base(contexto)
    {
        m_Inscricoes = inscricoes;
        m_Pedidos = pedidos;
        m_Pessoas = pessoas;
        m_FormasPagamento = formasPagamento;
        m_IntegracoesExternas = integracoesExternas;
        m_Integracoes = integracoes;
        m_RegistrosIntegracao = registrosIntegracao;
        m_ModelosNotificacao = modelosNotificacao;
        m_Mensagens = mensagens;
    }

    public DTOResultadoPedido Incluir(DTOPedidoInclusao dtoPedido)
    {
        DTOResultadoPedido? resultado = null;
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
            
            var pedido = new Pedido(
                pessoa,
                dtoPedido.IdsInscricoes.Select(id =>
                    m_Inscricoes.Obter(id) ?? throw new Exception($"Inscrição não encontrada com o id {id}")),
                new ValorMonetario(dtoPedido.Valor),
                dtoPedido.Tipo,
                forma,
                dtoPedido.Motivo
            );

            var servicoPedido = new SrvInclusaoPedido(
                m_Inscricoes, 
                m_Pedidos,
                m_IntegracoesExternas,
                m_Integracoes,
                m_RegistrosIntegracao,
                new SrvNotificacaoInscricaoRecebida(m_ModelosNotificacao, m_Mensagens),
                new SrvNotificacaoPedidoRealizado(m_ModelosNotificacao, m_Mensagens)
            );
            var resultadoIntegracao = servicoPedido.Incluir(pedido, dtoPedido.DadosCartaoCredito);

            resultado = new DTOResultadoPedido
            {
                IdPedido = pedido.Id,
                Valor = pedido.Valor.Valor,
                Tipo = pedido.Tipo,
                IdFormaPagamento = pedido.FormaPagamento?.Id
            };

            if (resultadoIntegracao != null)
            {
                resultado.Debito = new DTODebitoPedido
                {
                    TipoTransacao = resultadoIntegracao.TipoTransacao,
                    Status = resultadoIntegracao.Status,
                    ImagemQRCodePixBase64 = resultadoIntegracao.ImagemQRCodePixBase64,
                    PixCopiaECola = resultadoIntegracao.PixCopiaECola
                };
            }
        });

        return resultado!;
    }
    
    private Pessoa GerenciarPessoa(DTOPedidoInclusao dtoPedido)
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