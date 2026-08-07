using EventoWeb.Comum.Negocio.Entidades.Notificacoes;

namespace EventoWeb.Comum.Negocio.Servicos.Notificacoes
{
    public interface IEnvioNotificacao
    {
        Task Enviar(MensagemNotificacao mensagem);
    }
}
