using EventoWeb.Comum.Negocio.Entidades.Financeiro;
using EventoWeb.Comum.Negocio.Repositorios;
using NHibernate;

namespace EventoWeb.Comum.Persistencia.Repositorios
{
    internal class FormasPagamentoNH(ISession sessao) : PersistenciaNH<FormaPagamento>(sessao), IFormasPagamento
    {
        public IEnumerable<FormaPagamento> ListarTodas()
        {
            return Sessao.Query<FormaPagamento>().ToList();
        }
    }
}
