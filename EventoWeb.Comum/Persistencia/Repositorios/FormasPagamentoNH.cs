using EventoWeb.Comum.Negocio.Entidades.Financeiro;
using EventoWeb.Comum.Negocio.Repositorios;
using NHibernate;

namespace EventoWeb.Comum.Persistencia.Repositorios
{
    internal class FormasPagamentoNH(ISession sessao) : PersistenciaNH<FormaPagamento>(sessao), IFormasPagamento
    {
        public IEnumerable<FormaPagamento> ListarTodas(int idEvento)
        {
            return Sessao
                .QueryOver<FormaPagamento>()
                .Where(f => f.Evento.Id == idEvento)
                .List();
        }
    }
}
