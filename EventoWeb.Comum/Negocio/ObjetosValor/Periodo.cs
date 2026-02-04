namespace EventoWeb.Comum.Negocio.ObjetosValor
{
    public class Periodo
    {
        public Periodo(DateTime dataInicial, DateTime dataFinal)
        {
            if (dataFinal < dataInicial)
                throw new ArgumentException(
                    "Data final deve ser maior ou igual a inicial",
                    nameof(dataFinal));

            DataInicial = dataInicial;
            DataFinal = dataFinal;
        }

        protected Periodo() { }

        public virtual DateTime DataInicial { get; protected set; }
        public virtual DateTime DataFinal { get; protected set; }
    }
}
