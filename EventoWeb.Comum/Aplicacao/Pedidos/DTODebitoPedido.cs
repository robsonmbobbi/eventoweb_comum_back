using EventoWeb.Comum.Negocio.Entidades.IntegracaoFinanceira;
using EventoWeb.Comum.Negocio.Servicos;

namespace EventoWeb.Comum.Aplicacao.Pedidos
{
    public class DTODebitoPedido
    {
        public required EnumTipoIntegracao TipoTransacao { get; set; }
        public required EnumStatusTransacao Status { get; set; }
        public string? ImagemQRCodePixBase64 { get; set; }
        public string? PixCopiaECola { get; set; }
    }
}
