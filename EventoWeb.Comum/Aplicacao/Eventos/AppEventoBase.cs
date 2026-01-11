using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Repositorios;

namespace EventoWeb.Comum.Aplicacao.Eventos;

public abstract class AppEventoBase: AppBase
{
    protected IEventos Eventos { get; private set; }

    public AppEventoBase(IContexto contexto, IEventos eventos) : 
        base(contexto)
    {
        Eventos = eventos ?? throw new ArgumentNullException(nameof(eventos));
    }

    protected Evento ObterOuExcecao(int id)
    {
        return Eventos.Obter(id) ?? throw new Exception($"Evento não encontrado com o id {id}");
    }
}