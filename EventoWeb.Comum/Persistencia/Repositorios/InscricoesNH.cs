using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Repositorios;
using NHibernate;

namespace EventoWeb.Comum.Persistencia.Repositorios;

public class InscricoesNH(ISession sessao) : PersistenciaNH<Inscricao>(sessao), IInscricoes
{
    public IList<Inscricao> ListarPorSituacao(int idEvento, EnumSituacaoInscricao situacao)
    {
        return Sessao
            .QueryOver<Inscricao>()
            .Where(inscricao => inscricao.Evento.Id == idEvento && inscricao.Situacao == situacao)
            .List();
    }

    public Inscricao? ObterPorCPF(int idEvento, string cpf)
    {
        cpf = new String(cpf.Where(char.IsDigit)?.ToArray());

        return Sessao
            .QueryOver<Inscricao>()
            .Where(inscricao => inscricao.Evento.Id == idEvento)
            .JoinQueryOver(inscricao => inscricao.Pessoa)
            .Where(pessoa => pessoa.CPF.Numero == cpf)
            .SingleOrDefault();
    }

    public IList<Inscricao> ListarTodasInscricoesAceitasComPessoasDormemEvento(int id)
    {
        return Sessao
            .QueryOver<Inscricao>()
            .Where(inscricao => inscricao.Evento.Id == id && 
                                inscricao.Situacao == EnumSituacaoInscricao.Aceita &&
                                inscricao.DormeEvento == true)
            .List();
    }
}