
namespace EventoWeb.Comum.Negocio.Repositorios;

public interface IPersistencia<T>
{
    void Incluir(T objeto);
    void Excluir(T objeto);
    void Atualizar(T objeto);
    T? Obter(int id);
}