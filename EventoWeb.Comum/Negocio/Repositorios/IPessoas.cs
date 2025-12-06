using EventoWeb.Comum.Negocio.Entidades;

namespace EventoWeb.Comum.Negocio.Repositorios;

public interface IPessoas : IPersistencia<Pessoa>
{
    Pessoa? ObterPorCPF(string cpf);
}