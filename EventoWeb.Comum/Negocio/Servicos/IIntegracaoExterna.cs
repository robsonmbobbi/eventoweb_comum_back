using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Entidades.IntegracaoFinanceira;

namespace EventoWeb.Comum.Negocio.Servicos
{
    public interface IIntegracaoExterna
    {
        Task<DadosRetornoIntegracaoExterna> Enviar(IntegracaoFinanceiraPorFormaPag integrador, Pedido pedido, DadosCartaoCredito? dadosCartaoCredito);
    }
}