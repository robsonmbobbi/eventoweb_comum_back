using EventoWeb.Comum.Negocio.Entidades.Financeiro;

namespace EventoWeb.Comum.Negocio.Repositorios
{
    public interface IFormasPagamento : IPersistencia<FormaPagamento>
    {
        IEnumerable<FormaPagamento> ListarTodas();
    }
}
