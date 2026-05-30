namespace EventoWeb.Comum.Negocio.ObjetosValor
{
    public class Telefone
    {
        private string m_Numero;
        
        public virtual string Numero => m_Numero;

        public Telefone(string numero)
        {
            if (string.IsNullOrWhiteSpace(numero) || !numero.All(char.IsDigit) ||
                numero.Length < 10 || numero.Length > 11)
            {
                throw new ArgumentException(
                    "O número de telefone não pode ser nulo ou vazio, deve conter apenas números e no mínimo 10 e máximo 11 digitos.",
                    nameof(numero));
            }

            m_Numero = numero;
        }
        
        protected Telefone() { }
    }
}