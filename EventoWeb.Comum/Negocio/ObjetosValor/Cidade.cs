namespace EventoWeb.Comum.Negocio.ObjetosValor
{
    public class Cidade
    {
        private string m_Nome;

        public virtual string Nome => m_Nome;

        public Cidade(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentException(
                    "O nome da cidade não pode ser nulo ou vazio.",
                    nameof(nome));
            }

            if (nome.Length > 300)
            {
                throw new ArgumentException(
                    "O nome da cidade não pode ter mais de 300 caracteres.",
                    nameof(nome));
            }

            m_Nome = nome;
        }

        protected Cidade() { }
    }
}
