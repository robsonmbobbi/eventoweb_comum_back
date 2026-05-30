using EventoWeb.Comum.Negocio.ObjetosValor;

namespace EventoWeb.Comum.Negocio.Entidades.Financeiro
{
    public class ContaBancaria: Entidade
    {
        private String200 m_NomeConta;

        public ContaBancaria(String200 nomeConta)
        {
            NomeConta = nomeConta;
        }

        protected ContaBancaria() { }

        public virtual String200 NomeConta 
        {
            get => m_NomeConta;
            set
            {
                m_NomeConta = value ?? throw new Exception($"{nameof(NomeConta)} não pode ser nulo");
            }
        }
    }
}
