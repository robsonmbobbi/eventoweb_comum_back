namespace EventoWeb.Comum.Negocio.Entidades
{
    public class CPF
    {
        public virtual string Numero { get; }

        public CPF(string numero)
        {
            if (string.IsNullOrWhiteSpace(numero) || numero.Length != 11 || !numero.All(char.IsDigit))
            {
                throw new ArgumentException("Número de CPF não pode ser vazio ou nulo e deve conter apenas números e ter exatamente 11 digitos.", nameof(numero));
            }

            // Verifica se todos os dígitos são iguais
            if (numero.Distinct().Count() == 1)
            {
                throw new ArgumentException("Número de CPF inválido.", nameof(numero));
            }

            // Calcula os dígitos verificadores
            int[] multiplicadores1 = [10, 9, 8, 7, 6, 5, 4, 3, 2];
            int[] multiplicadores2 = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];

            string tempCpf = numero[..9];
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicadores1[i];

            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            tempCpf += digito1;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicadores2[i];

            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            string digitosVerificadores = numero.Substring(9, 2);
            if (digitosVerificadores != $"{digito1}{digito2}")
            {
                throw new ArgumentException("Número de CPF inválido.", nameof(numero));
            }

            Numero = numero;
        }

        protected CPF() { }
    }
}