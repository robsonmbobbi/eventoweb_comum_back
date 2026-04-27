namespace EventoWeb.Comum.Negocio.ObjetosValor
{
    public class ValorMonetario
    {
        public ValorMonetario(decimal valor) 
        {
            if (valor < 0)
                throw new Exception($"{nameof(valor)} deve ser maior ou igual a zero");

            Valor = valor;
        }

        protected ValorMonetario() { }

        public virtual decimal Valor
        {
            get;
            protected set;
        }
    }
}
