using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Repositorios;
using NHibernate;

namespace EventoWeb.Comum.Persistencia.Repositorios
{
    public class PedidosNH : IPedidos
    {
        private readonly ISession m_Sessao;

        public PedidosNH(ISession sessao)
        {
            m_Sessao = sessao ?? throw new ArgumentNullException(nameof(sessao));
        }

        public void Incluir(Pedido objeto)
        {
            m_Sessao.Save(objeto);
        }

        public void Excluir(Pedido objeto)
        {
            m_Sessao.Delete(objeto);
        }

        public void Atualizar(Pedido objeto)
        {
            m_Sessao.Update(objeto);
        }

        public Pedido? Obter(int id)
        {
            return m_Sessao.Get<Pedido>(id);
        }

        public Pedido? ObterPorInscricao(int idInscricao)
        {
            return m_Sessao.Query<Pedido>()
                .FirstOrDefault(p => p.Inscricoes.Any(i => i.Id == idInscricao));
        }
    }
}
