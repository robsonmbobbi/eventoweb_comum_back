using EventoWeb.Comum.Negocio.Entidades;

namespace EventoWeb.Comum.Negocio.Repositorios;

public interface IInscricoes : IPersistencia<Inscricao>
{
    Inscricao? ObterPorCPF(int idEvento, string cpf);

    List<Inscricao> ListarPorSituacao(int idEvento, EnumSituacaoInscricao situacao);
}