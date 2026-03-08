using EventoWeb.Comum.Negocio.Entidades.IntegracaoFinanceira;

namespace EventoWeb.Comum.Negocio.Repositorios
{
    public interface IRegistrosIntegracoesFinanceiras : IPersistencia<RegistroIntegracaoFinanceira>
    {
        IList<RegistroIntegracaoFinanceira> ListarPendentes();
    }
}
