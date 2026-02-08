using EventoWeb.Comum.Negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventoWeb.Comum.Aplicacao.Pedidos
{
    public class DTOResultadoPedido
    {
        public required int IdPedido { get; set; }
        public decimal Valor { get; set; }
        public EnumTipoPedido Tipo { get; set; }
        public int? IdFormaPagamento { get; set; }
        public DTODebitoPedido? Debito { get; set; }
    }
}
