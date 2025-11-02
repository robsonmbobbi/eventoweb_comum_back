namespace EventoWeb.Comum.Negocio
{
    public class EMail
    {
        public string Endereco { get; }

        public EMail(string endereco)
        {
            // Validação simples de e-mail
            if (string.IsNullOrWhiteSpace(endereco) || !endereco.Contains("@"))
            {
                throw new ArgumentException("Endereço de e-mail inválido.", nameof(endereco));
            }

            Endereco = endereco;
        }
    }
}