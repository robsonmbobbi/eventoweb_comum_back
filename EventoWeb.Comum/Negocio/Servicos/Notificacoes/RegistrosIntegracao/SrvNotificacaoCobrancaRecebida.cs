using EventoWeb.Comum.Negocio.Entidades.Financeiro;
using EventoWeb.Comum.Negocio.Entidades.IntegracaoFinanceira;
using EventoWeb.Comum.Negocio.Entidades.Notificacoes;
using EventoWeb.Comum.Negocio.Repositorios;
using System.Text.Json;

namespace EventoWeb.Comum.Negocio.Servicos.Notificacoes.RegistrosIntegracao
{
    public class SrvNotificacaoCobrancaRecebida(IModelosMensagemNotificacao modelosNotificacao, IMensagens mensagens)
    {
        private readonly IModelosMensagemNotificacao m_ModelosNotificacao = modelosNotificacao;
        private readonly IMensagens m_Mensagens = mensagens;

        public void Notificar(RegistroIntegracaoFinanceira registro, int idEvento)
        {
            var modelos = m_ModelosNotificacao.ListarPorTipo(idEvento, EnumTipoNotificacao.PagamentoRecebido);
            foreach (var modelo in modelos)
            {
                var destinatario = "";
                if (modelo.Meio == EnumMeioNotificacao.EMail)
                {
                    destinatario = registro.Conta.Pessoa.Email.Endereco;
                }
                else
                {
                    destinatario = registro.Conta.Pessoa.CelularWP.Numero;
                }

                var tipoTransacao = "";
                switch (registro.Tipo)
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
                            NomeEvento = modelo.Evento.Nome.Nome,
                            TipoTransacao = tipoTransacao,
                            registro.Transacao.Valor.Valor,
                        }
                    )
                );
                m_Mensagens.Incluir(mensagem);
            }
        }
    }
}
