using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Entidades.Financeiro;
using EventoWeb.Comum.Negocio.Entidades.IntegracaoFinanceira;

namespace EventoWeb.Comum.Negocio.Servicos
{
    public interface IIntegracaoExterna
    {
        Task<DadosRetornoIntegracaoExterna> CriarCobranca(IntegracaoFinanceiraPorFormaPag integrador, Pedido pedido, int? numeroParcelas);
        Task<DadosRetornoIntegracaoExterna> CriarCobrancaPorConta(IntegracaoFinanceiraPorFormaPag integrador, Conta conta, decimal valor, EnumTipoPagamento tipoPagamento, int? numeroParcelas);
        Task<DadosRetornoIntegracaoExterna?> ConsultarCobranca(IntegradorFinanceiro integrador, string identificador);
    }
}