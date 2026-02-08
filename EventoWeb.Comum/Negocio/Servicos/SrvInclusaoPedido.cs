using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventoWeb.Comum.Negocio.Servicos
{
    public class SrvInclusaoPedido
    {
        private readonly IInscricoes m_Inscricoes;
        private readonly IPersistencia<Pedido> m_Pedidos;

        public SrvInclusaoPedido(IInscricoes inscricoes, IPersistencia<Pedido> pedidos)
        {
            m_Inscricoes = inscricoes;
            m_Pedidos = pedidos;
        }

        public void Incluir(Pedido pedido)
        {
            foreach(var inscricao in pedido.Inscricoes)
            {
                inscricao.TornarPendente();
                m_Inscricoes.Atualizar(inscricao);
            }

            m_Pedidos.Incluir(pedido);
        }

    }
}
