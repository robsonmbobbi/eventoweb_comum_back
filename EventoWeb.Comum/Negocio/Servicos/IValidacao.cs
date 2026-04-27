using EventoWeb.Comum.Negocio.Entidades;

namespace EventoWeb.Comum.Negocio.Servicos;

public interface IValidacao<T> where T: Entidade
{
    void Validar(T entidade);
}