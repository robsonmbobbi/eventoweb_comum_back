namespace EventoWeb.Comum.Negocio.Entidades
{
    public partial class EMail
    {
        public virtual string Endereco { get; }

        public EMail(string endereco)
        {
            if (string.IsNullOrWhiteSpace(endereco))
                throw new ArgumentException("O endereço de e-mail não pode estar em branco");

            if (endereco.Length > 300)
                throw new ArgumentException("O endereço de e-mail não pode ter mais de 300 caracteres");

            if (!MyRegex().IsMatch(endereco))
                throw new ArgumentException("O endereço de e-mail informado não é válido");                              

            Endereco = endereco;
        }

        protected EMail() { }

        [System.Text.RegularExpressions.GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
        private static partial System.Text.RegularExpressions.Regex MyRegex();
    }
}