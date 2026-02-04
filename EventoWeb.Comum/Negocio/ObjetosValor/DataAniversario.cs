namespace EventoWeb.Comum.Negocio.ObjetosValor
{
    public class DataAniversario
    {
        private DateTime m_Data;
        
        public virtual DateTime Data => m_Data;

        public DataAniversario(DateTime data)
        {
            if (data > DateTime.Now)
            {
                throw new ArgumentException("Data de aniversário não pode ser no futuro.", nameof(data));
            }

            m_Data = data;
        }

        protected DataAniversario() { }

        public virtual int CalcularIdadeEmAnos(DateTime dataAtual)
        {
            return ObterIdadeAnos(Data, dataAtual);
        }

        public static int ObterIdadeAnos(DateTime dataAniversario, DateTime dataAtual)
        {
            if (dataAtual < dataAniversario)
                throw new ArgumentException("A data de comparação deve ser maior ou igual a do aniversário.", nameof(dataAtual));

            return (dataAtual.Year - dataAniversario.Year - 1) +
                   (((dataAtual.Month > dataAniversario.Month) ||
                     ((dataAtual.Month == dataAniversario.Month) && (dataAtual.Day >= dataAniversario.Day))) ? 1 : 0);
            
        }
    }    
}