using EventoWeb.Comum.Negocio.Entidades.Notificacoes;
using EventoWeb.Comum.Negocio.Repositorios;
using NHibernate;

namespace EventoWeb.Comum.Persistencia.Repositorios
{
    public class ModelosMensagemNotificacaoNH(ISession sessao) : PersistenciaNH<ModeloMensagemNotificacao>(sessao), IModelosMensagemNotificacao
    {
        public IList<ModeloMensagemNotificacao> ListarPorTipo(int idEvento, EnumTipoNotificacao tipo)
        {
            return Sessao
                .QueryOver<ModeloMensagemNotificacao>()
                .Where(modelo => modelo.Evento.Id == idEvento && modelo.Tipo == tipo)
                .List();
        }
    }
}