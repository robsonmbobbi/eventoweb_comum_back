using EventoWeb.Comum.Negocio.Entidades;

namespace EventoWeb.Comum.Negocio.Repositorios;

public enum EnumFiltroListagemEventos { Todos, EmPeriodoInscricao }

public interface IEventos : IPersistencia<Evento>
{
    IList<Evento> Listar(EnumFiltroListagemEventos filtro);
}