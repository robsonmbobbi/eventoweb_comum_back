using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Entidades.IntegracaoFinanceira;

namespace EventoWeb.Comum.Negocio.Servicos
{
    public interface IIntegracaoExterna
    {
        DadosRetornoIntegracaoExterna Enviar(IntegradorFinanceiro integrador, EnumTipoIntegracao tipoIntegracao, decimal valor, DadosCartaoCredito? dadosCartaoCredito);
    }
}