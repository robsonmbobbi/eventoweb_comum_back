namespace EventoWeb.Comum.Negocio.ObjetosValor
{
    public class String300
    {
        private string m_Valor;

        public virtual string Valor => m_Valor;

        public String300(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                throw new ArgumentException("O valor não pode ser nulo, vazio ou conter apenas espaços em branco.", nameof(valor));

            if (valor.Length > 300)
                throw new ArgumentException("O valor não pode exceder 300 caracteres.", nameof(valor));

            m_Valor = valor;
        }

        protected String300() { }
    }
}
