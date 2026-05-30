namespace EventoWeb.Comum.Negocio.ObjetosValor
{
    public class String4000
    {
        private string m_Valor;

        public virtual string Valor => m_Valor;

        public String4000(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                throw new ArgumentException("O valor não pode ser nulo, vazio ou conter apenas espaços em branco.", nameof(valor));

            if (valor.Length > 4000)
                throw new ArgumentException("O valor não pode exceder 4000 caracteres.", nameof(valor));

            m_Valor = valor;
        }

        protected String4000() { }
    }
}
