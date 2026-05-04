namespace EventoWeb.Comum.Negocio.ObjetosValor
{
    public class UF
    {
        private static readonly string[] UfsValidas = 
        {
            "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA",
            "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN",
            "RS", "RO", "RR", "SC", "SP", "SE", "TO"
        };

        private string m_Sigla;

        public virtual string Sigla => m_Sigla;

        public UF(string sigla)
        {
            if (string.IsNullOrWhiteSpace(sigla))
            {
                throw new ArgumentException(
                    "A sigla da UF não pode ser nula ou vazia.",
                    nameof(sigla));
            }

            string siglaNormalizada = sigla.Trim().ToUpper();

            if (siglaNormalizada.Length != 2 || !siglaNormalizada.All(char.IsLetter))
            {
                throw new ArgumentException(
                    "A sigla da UF deve conter exatamente 2 letras.",
                    nameof(sigla));
            }

            if (!UfsValidas.Contains(siglaNormalizada))
            {
                throw new ArgumentException(
                    "A sigla da UF informada não é válida.",
                    nameof(sigla));
            }

            m_Sigla = siglaNormalizada;
        }

        protected UF() { }
    }
}
