using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Entidades.Financeiro;
using EventoWeb.Comum.Negocio.Entidades.IntegracaoFinanceira;
using EventoWeb.Comum.Negocio.Repositorios;
using NHibernate;

namespace EventoWeb.Comum.Persistencia.Repositorios
{
    public class IntegracaoFinanceiraPorFormasPagamentosNH(ISession sessao) : PersistenciaNH<IntegracaoFinanceiraPorFormaPag>(sessao), IIntegracaoFinanceiraPorFormasPagamentos
    {
        public IntegracaoFinanceiraPorFormaPag ObterPorFormaPagamento(Evento evento, FormaPagamento forma)
        {
            return Sessao
                .QueryOver<IntegracaoFinanceiraPorFormaPag>()
                .Where(x => x.Evento.Id == evento.Id && x.FormaPagamento.Id == forma.Id)
                .SingleOrDefault();
        }
    }
}