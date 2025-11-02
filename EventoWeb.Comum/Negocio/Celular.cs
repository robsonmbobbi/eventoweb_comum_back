namespace EventoWeb.Comum.Negocio
{
    public class Celular
    {
        public string Numero { get; }

        public Celular(string numero)
        {
            // Validação simples de celular
            if (string.IsNullOrWhiteSpace(numero) || numero.Length < 10 || !numero.All(char.IsDigit))
            {
                throw new ArgumentException("Número de celular inválido.", nameof(numero));
            }

            Numero = numero;
        }
    }
}