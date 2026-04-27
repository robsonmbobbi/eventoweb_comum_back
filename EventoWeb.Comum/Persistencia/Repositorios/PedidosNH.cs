using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Repositorios;
using NHibernate;

namespace EventoWeb.Comum.Persistencia.Repositorios
{
    public class PedidosNH(ISession sessao) : PersistenciaNH<Pedido>(sessao), IPedidos
    {
        public Pedido? ObterPorInscricao(int idInscricao)
        {
            Inscricao inscricaoAlias = null;
            
            return Sessao
                .QueryOver<Pedido>()
                .JoinQueryOver(p => p.Inscricoes, () => inscricaoAlias)
                .Where(() => inscricaoAlias.Id == idInscricao)
                .SingleOrDefault();
        }
    }
}
