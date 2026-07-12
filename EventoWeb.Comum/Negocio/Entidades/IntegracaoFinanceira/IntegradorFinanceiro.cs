using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Entidades.Financeiro;
using EventoWeb.Comum.Negocio.ObjetosValor;

namespace EventoWeb.Comum.Negocio.Entidades.IntegracaoFinanceira
{
    public class IntegradorFinanceiro : Entidade
    {
        private ContaBancaria m_ContaBancaria;
        private String1000 m_TokenAcesso;

        public IntegradorFinanceiro(Evento evento, ContaBancaria contaBancaria, String1000 tokenAcesso, EnumIntegracaoExterna integracaoExterna)
        {
            Evento = evento ?? throw new ArgumentNullException(nameof(evento));
            ContaBancaria = contaBancaria ?? throw new ArgumentNullException(nameof(contaBancaria));
            TokenAcesso = tokenAcesso ?? throw new ArgumentNullException(nameof(tokenAcesso));
            IntegracaoExterna = integracaoExterna;
        }

        protected IntegradorFinanceiro() { }

        public virtual Evento Evento { get; protected set; }

        public virtual ContaBancaria ContaBancaria 
        { 
            get => m_ContaBancaria;
            set
            {
                if (value != null && Evento != null && value.Evento != Evento)
                    throw new ArgumentException("A conta bancária deve ser do mesmo evento do integrador.", nameof(ContaBancaria));

                m_ContaBancaria = value ?? throw new ArgumentNullException(nameof(ContaBancaria));
            }
        }

        public virtual String1000 TokenAcesso 
        { 
            get => m_TokenAcesso;
            set => m_TokenAcesso = value ?? throw new ArgumentNullException(nameof(TokenAcesso));
        }

        public virtual EnumIntegracaoExterna IntegracaoExterna { get; set; }
    }
}
