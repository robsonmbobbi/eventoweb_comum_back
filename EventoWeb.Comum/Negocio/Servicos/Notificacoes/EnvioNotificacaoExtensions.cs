using EventoWeb.Comum.Negocio.Entidades.Notificacoes;
using EventoWeb.Comum.Negocio.ObjetosValor;
using EventoWeb.Comum.Negocio.Repositorios;

namespace EventoWeb.Comum.Negocio.Servicos.Notificacoes
{
    public static class EnvioNotificacaoExtensions
    {
        public static void EnviarERegistrar(
            this IEnvioNotificacao envioNotificacao,
            IPersistencia<MensagemNotificacao> mensagens,
            MensagemNotificacao mensagem)
        {
            try
            {
                _ = envioNotificacao.Enviar(mensagem);
            }
            catch (Exception ex)
            {
                mensagem.RegistrarErroFila(new StringClob(ex.Message));
                mensagens.Atualizar(mensagem);
            }
        }
    }
}
