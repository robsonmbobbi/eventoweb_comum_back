using EventoWeb.Comum.Negocio.Repositorios;

namespace EventoWeb.Comum.Aplicacao.Eventos;

public class AppEventoListagem: AppEventoBase
{
    public AppEventoListagem(IContexto contexto, IEventos eventos) : base(contexto, eventos)
    {
    }

    public IList<DTOEvento> Listar(EnumFiltroListagemEventos filtro)
    {
        List<DTOEvento> lista = [];
        
        ExecutarSeguramente(() =>
        {
            lista.AddRange(
                Eventos
                    .Listar(filtro)
                    .Select( x=> x.Converter())
                    .ToList()
            );
        });

        return lista;
    }
}