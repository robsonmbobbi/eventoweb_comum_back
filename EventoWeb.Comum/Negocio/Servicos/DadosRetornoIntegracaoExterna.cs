using EventoWeb.Comum.Negocio.Entidades.Financeiro;

namespace EventoWeb.Comum.Negocio.Servicos
{
    public enum EnumStatusTransacao { Pendente, Recebida, Cancelada }

    public class DadosRetornoIntegracaoExterna
    {
        public required string IdTransacao { get; set; }
        public required EnumTipoPagamento TipoTransacao { get; set; }
        public required EnumStatusTransacao Status { get; set; }
        public string? ImagemQRCodePixBase64 { get; set; }
        public string? PixCopiaECola { get; set; }
    }
}
