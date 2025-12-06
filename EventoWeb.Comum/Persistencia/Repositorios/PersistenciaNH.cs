using EventoWeb.Comum.Negocio.Repositorios;
using NHibernate;

namespace EventoWeb.Comum.Persistencia.Repositorios;

public class PersistenciaNH<T>(ISession sessao) : IPersistencia<T>
{
    protected ISession Sessao { get; private set; } = sessao;

    public void Incluir(T objeto)
    {
        Sessao.Save(objeto);
    }

    public void Excluir(T objeto)
    {
        Sessao.Delete(objeto);
    }

    public void Atualizar(T objeto)
    {
        Sessao.Update(objeto);
    }

    public T Obter(int id)
    {
        return Sessao.Get<T>(id);
    }
}