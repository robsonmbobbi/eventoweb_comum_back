using EventoWeb.Comum.Negocio.Entidades.Financeiro;
using EventoWeb.Comum.Negocio.Entidades.IntegracaoFinanceira;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventoWeb.Comum.Negocio.Repositorios
{
    public interface IIntegracaoFinanceiraPorFormasPagamentos : IPersistencia<IntegracaoFinanceiraPorFormaPag>
    {
        IntegracaoFinanceiraPorFormaPag ObterPorFormaPagamento(FormaPagamento forma);
    }
}
