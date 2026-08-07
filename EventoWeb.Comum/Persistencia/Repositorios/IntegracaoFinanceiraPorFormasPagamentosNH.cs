using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Entidades.Financeiro;
using EventoWeb.Comum.Negocio.Entidades.IntegracaoFinanceira;
using EventoWeb.Comum.Negocio.Repositorios;
using NHibernate;

namespace EventoWeb.Comum.Persistencia.Repositorios
{
    public class IntegracaoFinanceiraPorFormasPagamentosNH(ISession sessao) : PersistenciaNH<IntegracaoFinanceiraPorFormaPag>(sessao), IIntegracaoFinanceiraPorFormasPagamentos
    {
        public IntegracaoFinanceiraPorFormaPag ObterPorFormaPagamento(int idEvento, int idForma)
        {
            return Sessao
                .QueryOver<IntegracaoFinanceiraPorFormaPag>()
                .Where(x => x.Evento.Id == idEvento && x.FormaPagamento.Id == idForma)
                .SingleOrDefault();
        }
    }
}