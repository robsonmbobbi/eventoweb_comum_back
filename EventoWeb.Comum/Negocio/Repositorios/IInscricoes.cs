using EventoWeb.Comum.Negocio.Entidades;

namespace EventoWeb.Comum.Negocio.Repositorios;

public interface IInscricoes : IPersistencia<Inscricao>
{
    Inscricao? ObterPorCPF(int idEvento, string cpf);

    IList<Inscricao> ListarPorSituacao(int idEvento, EnumSituacaoInscricao situacao);
    IList<Inscricao> ListarTodasInscricoesAceitasComPessoasDormemEvento(int id);
}