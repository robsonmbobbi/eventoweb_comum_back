using EventoWeb.Comum.Negocio.Entidades.IntegracaoFinanceira;
using EventoWeb.Comum.Negocio.Entidades.Financeiro;

namespace EventoWeb.Comum.Negocio.Repositorios
{
    public interface IRegistrosIntegracoesFinanceiras : IPersistencia<RegistroIntegracaoFinanceira>
    {
        IList<RegistroIntegracaoFinanceira> ListarPendentes();
        RegistroIntegracaoFinanceira? ObterPorConta(int idConta);
    }
}
