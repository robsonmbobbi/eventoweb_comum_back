namespace EventoWeb.Comum.Negocio.Repositorios;

public interface IContexto : IDisposable
{
    void IniciarTransacao();

    void SalvarTransacao();

    void CancelarTransacao();
}