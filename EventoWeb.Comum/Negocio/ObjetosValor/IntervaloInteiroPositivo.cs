namespace EventoWeb.Comum.Negocio.ObjetosValor
{
    public class IntervaloInteiroPositivo
    {
        private int m_Minimo;
        private int m_Maximo;

        public virtual int Minimo => m_Minimo;
        public virtual int Maximo => m_Maximo;

        public IntervaloInteiroPositivo(int minimo, int maximo)
        {
            if (minimo <= 0)
                throw new ArgumentException("O valor mínimo deve ser maior que zero.", nameof(minimo));

            if (maximo <= 0)
                throw new ArgumentException("O valor máximo deve ser maior que zero.", nameof(maximo));

            if (maximo < minimo)
                throw new ArgumentException("O valor máximo deve ser maior ou igual ao valor mínimo.", nameof(maximo));

            m_Minimo = minimo;
            m_Maximo = maximo;
        }

        protected IntervaloInteiroPositivo() { }
    }
}
