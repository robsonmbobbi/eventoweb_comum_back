using EventoWeb.Comum.Negocio.Entidades.IntegracaoFinanceira;
using EventoWeb.Comum.Negocio.Repositorios;
using NHibernate;

namespace EventoWeb.Comum.Persistencia.Repositorios
{
    public class RegistrosIntegracoesFinanceirasNH : PersistenciaNH<RegistroIntegracaoFinanceira>, IRegistrosIntegracoesFinanceiras
    {
        public RegistrosIntegracoesFinanceirasNH(ISession sessao) : base(sessao)
        {
        }

        public IList<RegistroIntegracaoFinanceira> ListarPendentes(int idEvento)
        {
            return Sessao
                .QueryOver<RegistroIntegracaoFinanceira>()
                .Where(r => r.Situacao == EnumSituacaoIntegracao.Pendente && r.Evento.Id == idEvento)
                .List();
        }

        public IList<RegistroIntegracaoFinanceira> ListarPorConta(int idConta)
        {
            return Sessao
                .QueryOver<RegistroIntegracaoFinanceira>()
                .Where(r => r.Conta.Id == idConta)
                .List();
        }
    }
}
