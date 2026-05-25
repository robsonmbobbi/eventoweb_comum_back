namespace EventoWeb.Comum.Negocio.ObjetosValor
{
    public class InteiroPositivo
    {
        private int m_Valor;

        public virtual int Valor => m_Valor;

        public InteiroPositivo(int valor)
        {
            if (valor <= 0)
                throw new ArgumentException("O valor deve ser maior que zero.", nameof(valor));

            m_Valor = valor;
        }

        protected InteiroPositivo() { }
    }
}
