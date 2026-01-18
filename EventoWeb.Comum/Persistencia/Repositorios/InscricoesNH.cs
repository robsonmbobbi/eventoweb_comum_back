using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Repositorios;
using NHibernate;

namespace EventoWeb.Comum.Persistencia.Repositorios;

public class InscricoesNH(ISession sessao) : PersistenciaNH<Inscricao>(sessao), IInscricoes
{
    public Inscricao? ObterPorCPF(int idEvento, string cpf)
    {
        cpf = cpf.Where(char.IsDigit)?.ToString() ?? "";
        
        return Sessao
            .QueryOver<Inscricao>()
            .Where(inscricao => inscricao.Evento.Id == idEvento)
            .JoinQueryOver(inscricao => inscricao.Pessoa)
            .Where(pessoa => pessoa.CPF.Numero == cpf)
            .SingleOrDefault();
    }
}