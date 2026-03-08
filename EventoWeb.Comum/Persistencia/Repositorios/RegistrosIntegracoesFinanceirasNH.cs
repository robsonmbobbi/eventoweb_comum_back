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

        public IList<RegistroIntegracaoFinanceira> ListarPendentes()
        {
            return Sessao.Query<RegistroIntegracaoFinanceira>()
                .Where(r => r.Situacao == EnumSituacaoIntegracao.Pendente)
                .ToList();
        }
    }
}
