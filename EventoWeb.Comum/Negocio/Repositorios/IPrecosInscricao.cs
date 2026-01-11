using EventoWeb.Comum.Negocio.Entidades;

namespace EventoWeb.Comum.Negocio.Repositorios;

public interface IPrecosInscricao: IPersistencia<PrecoInscricao>
{
    PrecoInscricao ObterPelaIdade(int idEvento, int idade);
}