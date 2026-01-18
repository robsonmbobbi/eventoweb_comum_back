using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Repositorios;
using NHibernate;

namespace EventoWeb.Comum.Persistencia.Repositorios;

public class EventosNH(ISession sessao) : PersistenciaNH<Evento>(sessao), IEventos
{
    public IList<Evento> Listar(EnumFiltroListagemEventos filtro, DateTime? dataAtual = null)
    {
        var query = Sessao.QueryOver<Evento>();
        if (filtro == EnumFiltroListagemEventos.EmPeriodoInscricao)
        {
            query.Where(evento => evento.PeriodoInscricaoOnLine.DataFinal <= (dataAtual ?? DateTime.Now));
        }

        return query.List();
    }
}