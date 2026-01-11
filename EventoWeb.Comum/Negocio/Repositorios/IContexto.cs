namespace EventoWeb.Comum.Negocio.Repositorios;

public interface IContexto
{
    void IniciarTransacao();

    void SalvarTransacao();

    void CancelarTransacao();
}