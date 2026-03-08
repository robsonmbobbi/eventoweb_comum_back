using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Entidades.IntegracaoFinanceira;

namespace EventoWeb.Comum.Negocio.Servicos
{
    public interface IIntegracaoExterna
    {
        Task<DadosRetornoIntegracaoExterna> CriarCobranca(IntegracaoFinanceiraPorFormaPag integrador, Pedido pedido, DadosCartaoCredito? dadosCartaoCredito);
        Task<DadosRetornoIntegracaoExterna?> ConsultarCobranca(IntegradorFinanceiro integrador, string identificador);

    }
}