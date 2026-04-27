using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Repositorios;

namespace EventoWeb.Comum.Negocio.Repositorios
{
    public interface IPedidos : IPersistencia<Pedido>
    {
        Pedido? ObterPorInscricao(int idInscricao);
    }
}
