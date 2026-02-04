using EventoWeb.Comum.Negocio.ObjetosValor;

namespace EventoWeb.Comum.Negocio.Entidades.Financeiro
{
    public class ContaBancaria: Entidade
    {
        private NomeCompleto m_NomeConta;

        public ContaBancaria(NomeCompleto nomeConta)
        {
            NomeConta = nomeConta;
        }

        protected ContaBancaria() { }

        public virtual NomeCompleto NomeConta 
        {
            get => m_NomeConta;
            set
            {
                m_NomeConta = value ?? throw new Exception($"{nameof(NomeConta)} não pode ser nulo");
            }
        }
    }
}
