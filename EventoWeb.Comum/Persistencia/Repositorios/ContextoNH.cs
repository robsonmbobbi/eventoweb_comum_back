using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Repositorios;
using NHibernate;

namespace EventoWeb.Comum.Persistencia.Repositorios;

public class ContextoNH: IContexto
{
    private readonly ISession m_Sessao;

    public ContextoNH(ISession sessao)
    {
        m_Sessao = sessao;
    }
    
    public void IniciarTransacao()
    {
        m_Sessao.BeginTransaction();
    }

    public void SalvarTransacao()
    {
        var session = m_Sessao;
        if (session.GetCurrentTransaction() != null)
            session.GetCurrentTransaction().Commit();
    }

    public void CancelarTransacao()
    {
        var session = m_Sessao;
        if (session.GetCurrentTransaction() != null)
        {
            session.GetCurrentTransaction().Rollback();
            session.Clear();
        }
    }

    public void Dispose()
    {
        m_Sessao.Close();
        m_Sessao.Dispose();
    }

    public IEventos Eventos => new EventosNH(m_Sessao);
    public IInscricoes Inscricoes => new InscricoesNH(m_Sessao);
    public IPessoas Pessoas => new PessoasNH(m_Sessao);
    public IPrecosInscricao PrecosInscricao => new PrecosInscricaoNH(m_Sessao);
    public IPersistencia<Pedido> Pedidos => new PersistenciaNH<Pedido>(m_Sessao);

}