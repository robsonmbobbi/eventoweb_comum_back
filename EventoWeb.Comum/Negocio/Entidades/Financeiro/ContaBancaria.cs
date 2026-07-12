using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.ObjetosValor;

namespace EventoWeb.Comum.Negocio.Entidades.Financeiro
{
    public class ContaBancaria: Entidade
    {
        private String200 m_NomeConta;

        public ContaBancaria(Evento evento, String200 nomeConta)
        {
            Evento = evento ?? throw new Exception($"{nameof(Evento)} não pode ser nulo");
            NomeConta = nomeConta;
        }

        protected ContaBancaria() { }

        public virtual Evento Evento { get; protected set; }

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
