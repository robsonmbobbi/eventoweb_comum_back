using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Entidades.IntegracaoFinanceira;
using EventoWeb.Comum.Negocio.Repositorios;
using EventoWeb.Comum.Negocio.Servicos.IntegracaoFinanceira;
using EventoWeb.Comum.Negocio.Servicos.Notificacoes.Inscricoes;
using EventoWeb.Comum.Negocio.Servicos.Notificacoes.Pedidos;

namespace EventoWeb.Comum.Negocio.Servicos
{
    public class SrvInclusaoPedido
    {
        private readonly IInscricoes m_Inscricoes;
        private readonly IPersistencia<Pedido> m_Pedidos;
        private readonly IIntegracaoFinanceiraPorFormasPagamentos m_Integracoes;
        private readonly IDictionary<EnumIntegracaoExterna, IIntegracaoExterna> m_IntegracoesExternas;
        private readonly IPersistencia<RegistroIntegracaoFinanceira> m_RegistrosIntegracao;
        private readonly SrvNotificacaoInscricaoRecebida m_SrvNotificacaoInscricoes;
        private readonly SrvNotificacaoPedidoRealizado m_SrvNotificacaoPedidoRealizado;

        public SrvInclusaoPedido(
            IInscricoes inscricoes, 
            IPersistencia<Pedido> pedidos, 
            IDictionary<EnumIntegracaoExterna, IIntegracaoExterna> integracoesExternas,
            IIntegracaoFinanceiraPorFormasPagamentos integracoes,
            IPersistencia<RegistroIntegracaoFinanceira> registrosIntegracao,
            SrvNotificacaoInscricaoRecebida srvNotificacaoInscricoes,
            SrvNotificacaoPedidoRealizado srvNotificacaoPedidoRealizado)
        {
            m_Inscricoes = inscricoes;
            m_Pedidos = pedidos;
            m_IntegracoesExternas = integracoesExternas;
            m_Integracoes = integracoes;
            m_RegistrosIntegracao = registrosIntegracao;
            m_SrvNotificacaoPedidoRealizado = srvNotificacaoPedidoRealizado;
            m_SrvNotificacaoInscricoes = srvNotificacaoInscricoes;
        }

        public DadosRetornoIntegracaoExterna? Incluir(Pedido pedido, int? numeroParcelas)
        {
            foreach(var inscricao in pedido.Inscricoes)
            {
                inscricao.TornarPendente();
                m_Inscricoes.Atualizar(inscricao);
            }

            m_Pedidos.Incluir(pedido);
            DadosRetornoIntegracaoExterna? retornoIntegracao = null;

            if (pedido.Tipo == EnumTipoPedido.Debito)
            {
                var servico = new SrvCriacaoCobranca(
                    m_IntegracoesExternas,
                    m_Integracoes,
                    m_RegistrosIntegracao,
                    m_Pedidos
                );

                retornoIntegracao = servico.Criar(pedido, numeroParcelas);
            }

            m_SrvNotificacaoInscricoes.Notificar(pedido.Inscricoes);
            m_SrvNotificacaoPedidoRealizado.Notificar(pedido, retornoIntegracao);

            return retornoIntegracao;
        }

    }
}
