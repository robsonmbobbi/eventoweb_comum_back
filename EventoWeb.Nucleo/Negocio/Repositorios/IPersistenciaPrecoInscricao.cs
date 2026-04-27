using EventoWeb.Nucleo.Negocio.Entidades;

namespace EventoWeb.Nucleo.Negocio.Repositorios;

public interface IPersistenciaPrecoInscricao : IPersistencia<PrecoInscricao>
{
    PrecoInscricao? ObterParaEssaIdade(Evento novoPrecoEvento, int novoPrecoIdadeMax);
}