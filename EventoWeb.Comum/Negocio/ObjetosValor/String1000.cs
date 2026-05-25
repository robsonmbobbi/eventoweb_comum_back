namespace EventoWeb.Comum.Negocio.ObjetosValor
{
    public class String1000
    {
        private string m_Valor;

        public virtual string Valor => m_Valor;

        public String1000(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                throw new ArgumentException("O valor não pode ser nulo, vazio ou conter apenas espaços em branco.", nameof(valor));

            if (valor.Length > 1000)
                throw new ArgumentException("O valor não pode exceder 1000 caracteres.", nameof(valor));

            m_Valor = valor;
        }

        protected String1000() { }
    }
}
