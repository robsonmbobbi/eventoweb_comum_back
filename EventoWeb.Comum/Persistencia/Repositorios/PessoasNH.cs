using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Repositorios;
using NHibernate;

namespace EventoWeb.Comum.Persistencia.Repositorios;

public class PessoasNH(ISession sessao) : PersistenciaNH<Pessoa>(sessao), IPessoas
{
    public Pessoa? ObterPorCPF(string cpf)
    {
        return Sessao
            .QueryOver<Pessoa>()
            .Where(p => p.CPF.Numero.Equals(cpf))
            .SingleOrDefault();
    }
}