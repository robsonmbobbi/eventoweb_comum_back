namespace EventoWeb.Comum.Negocio.Entidades
{
    public class Telefone
    {
        public virtual string Numero { get; }

        public Telefone(string numero)
        {
            if (string.IsNullOrWhiteSpace(numero) || !numero.All(char.IsDigit) ||
                numero.Length < 10 || numero.Length > 11)
            {
                throw new ArgumentException(
                    "O número de telefone não pode ser nulo ou vazio, deve conter apenas números e no mínimo 10 e máximo 11 digitos.",
                    nameof(numero));
            }

            Numero = numero;
        }
        
        protected Telefone() { }
    }
}