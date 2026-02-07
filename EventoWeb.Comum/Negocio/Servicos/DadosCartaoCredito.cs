namespace EventoWeb.Comum.Negocio.Servicos
{
    public class DadosCartaoCredito
    {
        public required string NumeroCartao { get; set; }
        public required string NomeImpressoCartao { get; set; }
        public required string MesExpiracao { get; set; }
        public required string AnoExpiracao { get; set; }
        public required string CodigoSeguranca { get; set; }

        public required string NomeTitular { get; set; }
        public required string EmailTitular { get; set; }
        public required string CPFouCNPJTitular { get; set; }
        public required string CEPTitular { get; set; }
        public required string NumeroEnderecoTitular { get; set; }
        public required string TelefoneTitular { get; set; }

        public int? NumeroParcelas { get; set; }
    }
}
