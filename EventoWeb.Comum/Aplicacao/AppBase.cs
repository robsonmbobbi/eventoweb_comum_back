using EventoWeb.Comum.Negocio.Repositorios;

namespace EventoWeb.Comum.Aplicacao;

public abstract class AppBase
{
    public AppBase(IContexto contexto)
    {
        Contexto = contexto;
    }

    public IContexto Contexto { get; }

    protected virtual void ExecutarSeguramente(Action acaoExecutar)
    {
        try
        {
            Contexto.IniciarTransacao();

            acaoExecutar();

            Contexto.SalvarTransacao();
        }
        catch (Exception)
        {
            Contexto.CancelarTransacao();

            throw;
        }
    }
}