using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Repositorios;
using NHibernate;

namespace EventoWeb.Comum.Persistencia.Repositorios;

public class PrecosInscricaoNH(ISession sessao) : PersistenciaNH<PrecoInscricao>(sessao), IPrecosInscricao
{
    public PrecoInscricao? ObterPelaIdade(int idEvento, int idade)
    {
        return Sessao
            .QueryOver<PrecoInscricao>()
            .Where(preco => preco.Evento.Id == idEvento && preco.IdadeMax!.Valor >= idade)
            .OrderBy(preco => preco.IdadeMax!.Valor).Asc
            .Take(1)
            .SingleOrDefault();
    }
}