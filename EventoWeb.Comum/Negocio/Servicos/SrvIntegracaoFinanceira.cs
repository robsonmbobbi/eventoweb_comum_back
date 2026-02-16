using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Entidades.IntegracaoFinanceira;
using EventoWeb.Comum.Negocio.Repositorios;

namespace EventoWeb.Comum.Negocio.Servicos
{
    public class SrvIntegracaoFinanceira
    {
        private readonly IIntegracaoFinanceiraPorFormasPagamentos m_Integracoes;
        private readonly IDictionary<EnumIntegracaoExterna, IIntegracaoExterna> m_IntegracoesExternas;
        private readonly IPersistencia<RegistroIntegracaoFinanceira> m_RegistrosIntegracao;
        private readonly IPersistencia<Pedido> m_Pedidos;


        public SrvIntegracaoFinanceira(IDictionary<EnumIntegracaoExterna, IIntegracaoExterna> integracoesExternas, IIntegracaoFinanceiraPorFormasPagamentos integracoes,
            IPersistencia<RegistroIntegracaoFinanceira> registrosIntegracao, IPersistencia<Pedido> pedidos)
        {
            m_Integracoes = integracoes;
            m_IntegracoesExternas = integracoesExternas;
            m_RegistrosIntegracao = registrosIntegracao;
            m_Pedidos = pedidos;
        }

        public DadosRetornoIntegracaoExterna ProcessarIntegracao(Pedido pedido, DadosCartaoCredito? dadosCartaoCredito)
        {
            if (pedido.Tipo != EnumTipoPedido.Debito) 
            { 
                throw new InvalidOperationException("A integração financeira só pode ser processada para pedidos do tipo Débito.");
            }
            
            var integracao = m_Integracoes.ObterPorFormaPagamento(pedido.FormaPagamento!);
            var integradorExterno = m_IntegracoesExternas[integracao.Integrador.IntegracaoExterna];

            var retorno = integradorExterno
                .Enviar(integracao, pedido, dadosCartaoCredito)
                .Result;

            var registroIntegracao = new RegistroIntegracaoFinanceira(
                integracao.Integrador,
                pedido.Conta,
                pedido.Valor,
                integracao.TipoIntegracao,
                retorno.IdTransacao,
                integracao.TipoIntegracao == EnumTipoIntegracao.CreditoParcelado ? dadosCartaoCredito?.NumeroParcelas : null
            );            

            if (retorno.Status == EnumStatusTransacao.Recebida)
            {
                pedido.Conta.AdicionarTransacao(
                    integracao.Integrador.ContaBancaria,
                    DateTime.Now,
                    pedido.Conta.Valor
                );
                m_Pedidos.Atualizar(pedido);

                var transacao = pedido.Conta.Transacoes.Last().Transacao;

                registroIntegracao.Concluir(transacao!);
            }

            m_RegistrosIntegracao.Incluir(registroIntegracao);

            return retorno;
        }
    }
}
