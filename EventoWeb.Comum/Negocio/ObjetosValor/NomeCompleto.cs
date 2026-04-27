namespace EventoWeb.Comum.Negocio.ObjetosValor
{
    public class NomeCompleto
    {
        private string m_Nome;

        public virtual string Nome => m_Nome;

        public NomeCompleto(string nome)
        {
            // Validação simples de e-mail
            if (string.IsNullOrWhiteSpace(nome) || nome.Length > 200)
            {
                throw new ArgumentException(
                    "Nome não pode ser vazio ou nulo e deve ter no máximo 200 caracteres",
                    nameof(nome));
            }

            m_Nome = nome;
        }

        protected NomeCompleto() { }
    }
}