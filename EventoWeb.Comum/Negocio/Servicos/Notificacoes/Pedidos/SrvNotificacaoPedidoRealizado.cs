using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Entidades.Financeiro;
using EventoWeb.Comum.Negocio.Entidades.Notificacoes;
using EventoWeb.Comum.Negocio.Repositorios;
using System.Text.Json;

namespace EventoWeb.Comum.Negocio.Servicos.Notificacoes.Pedidos
{
    public class SrvNotificacaoPedidoRealizado(IModelosMensagemNotificacao modelosNotificacao, IPersistencia<MensagemNotificacao> mensagens)
    {
        private readonly IModelosMensagemNotificacao m_ModelosNotificacao = modelosNotificacao;
        private readonly IPersistencia<MensagemNotificacao> m_Mensagens = mensagens;

        public void Notificar(Pedido pedido, DadosRetornoIntegracaoExterna? dadosRetorno)
        {
            var modelos = m_ModelosNotificacao.ListarPorTipo(pedido.Inscricoes.First().Evento.Id, EnumTipoNotificacao.PedidoRealizado);
            foreach (var modelo in modelos)
            {
                var destinatario = "";
                if (modelo.Meio == EnumMeioNotificacao.EMail)
                {
                    destinatario = pedido.Pagador.Email.Endereco;
                }
                else
                {
                    destinatario = pedido.Pagador.CelularWP.Numero;
                }

                var tipoPedido = "";
                switch (pedido.Tipo)
                {
                    case EnumTipoPedido.Debito:
                        tipoPedido = "Débito";
                        break;
                    case EnumTipoPedido.Desconto:
                        tipoPedido = "Solicitação de desconto";
                        break;
                    case EnumTipoPedido.Isencao:
                        tipoPedido = "Solicitação de isenção";
                        break;
                }

                var tipoTransacao = "";
                switch(dadosRetorno?.TipoTransacao)
                {
                    case EnumTipoPagamento.CartaoCredito:
                        tipoTransacao = "Cartão Crédito";
                        break;
                    case EnumTipoPagamento.PIX:
                        tipoTransacao = "PIX";
                        break;
                }

                var mensagem = new MensagemNotificacao(
                    modelo, 
                    destinatario,
                    JsonSerializer.Serialize(
                        new
                        {
                            NomeEvento = pedido.Inscricoes.First().Evento.Nome,
                            pedido.Valor.Valor,
                            TipoPedido = tipoPedido,
                            TipoTransacao = tipoTransacao,
                            dadosRetorno?.ImagemQRCodePixBase64,
                            dadosRetorno?.PixCopiaECola,
                            dadosRetorno?.LinkPagamento,
                        }
                    )
                );
                m_Mensagens.Incluir(mensagem);
            }
        }
    }
}
