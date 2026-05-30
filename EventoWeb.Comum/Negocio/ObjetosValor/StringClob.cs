namespace EventoWeb.Comum.Negocio.ObjetosValor
{
    public class StringClob
    {
        private string m_Valor;

        public virtual string Valor => m_Valor;

        public StringClob(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                throw new ArgumentException("O valor não pode ser nulo, vazio ou conter apenas espaços em branco.", nameof(valor));

            m_Valor = valor;
        }

        protected StringClob() { }
    }
}
