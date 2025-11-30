namespace EventoWeb.Comum.Negocio.Entidades
{
    public class DataAniversario
    {
        public virtual DateTime Data { get; }

        public DataAniversario(DateTime data)
        {
            if (data > DateTime.Now)
            {
                throw new ArgumentException("Data de aniversário não pode ser no futuro.", nameof(data));
            }

            Data = data;
        }

        protected DataAniversario() { }

        public virtual int CalcularIdadeEmAnos(DateTime dataAtual)
        {
            if (dataAtual < Data)
                throw new ArgumentException("A data de comparação deve ser maior ou igual a do aniversário.", nameof(dataAtual));

            return (dataAtual.Year - Data.Year - 1) +
                    (((dataAtual.Month > Data.Month) ||
                    ((dataAtual.Month == Data.Month) && (dataAtual.Day >= Data.Day))) ? 1 : 0);
        }        
    }    
}