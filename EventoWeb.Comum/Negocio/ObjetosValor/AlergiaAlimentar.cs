namespace EventoWeb.Comum.Negocio.ObjetosValor
{
    public class AlergiaAlimentar
    {
        private string m_Descricao;

        public virtual string Descricao => m_Descricao;

        public AlergiaAlimentar(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
            {
                throw new ArgumentException(
                    "A descrição da alergia alimentar não pode ser nula ou vazia.",
                    nameof(descricao));
            }

            if (descricao.Length > 100)
            {
                throw new ArgumentException(
                    "A descrição da alergia alimentar não pode ter mais de 100 caracteres.",
                    nameof(descricao));
            }

            m_Descricao = descricao;
        }

        protected AlergiaAlimentar() { }
    }
}
