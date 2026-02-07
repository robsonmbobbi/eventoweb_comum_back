namespace EventoWeb.Comum.Negocio.Servicos
{
    public class DadosRetornoIntegracaoExterna
    {
        public required string IdTransacao { get; set; }
        public string? ImagemQRCodePixBase64 { get; set; }
        public string? PixCopiaECola { get; set; }
    }
}
