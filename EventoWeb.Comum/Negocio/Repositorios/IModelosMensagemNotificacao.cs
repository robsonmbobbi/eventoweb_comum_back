using EventoWeb.Comum.Negocio.Entidades.Notificacoes;

namespace EventoWeb.Comum.Negocio.Repositorios
{
    public interface IModelosMensagemNotificacao : IPersistencia<ModeloMensagemNotificacao>
    {
        public IList<ModeloMensagemNotificacao> ListarPorTipo(int idEvento, EnumTipoNotificacao tipo);
    }
}
