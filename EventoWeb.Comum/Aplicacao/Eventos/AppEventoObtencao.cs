using EventoWeb.Comum.Negocio.Repositorios;

namespace EventoWeb.Comum.Aplicacao.Eventos;

public class AppEventoObtencao : AppEventoBase
{
    public AppEventoObtencao(IContexto contexto, IEventos eventos) : base(contexto, eventos)
    {
    }

    public DTOEvento? Obter(int id)
    {
        DTOEvento? dto = null;
        ExecutarSeguramente(() =>
        {
            var evento = Eventos.Obter(id);
            dto = evento?.Converter();
        });

        return dto;
    }

}