using EventoWeb.Comum.Negocio.Entidades.Financeiro;
using EventoWeb.Comum.Negocio.ObjetosValor;

namespace EventoWeb.Comum.Negocio.Entidades.IntegracaoFinanceira
{
    public class IntegradorFinanceiro : Entidade
    {
        private ContaBancaria m_ContaBancaria;
        private String1000? m_TokenAcesso;

        public IntegradorFinanceiro(ContaBancaria contaBancaria, String1000 tokenAcesso, EnumIntegracaoExterna integracaoExterna)
        {
            ContaBancaria = contaBancaria ?? throw new ArgumentNullException(nameof(contaBancaria));
            TokenAcesso = tokenAcesso ?? throw new ArgumentNullException(nameof(tokenAcesso));
            IntegracaoExterna = integracaoExterna;
        }

        protected IntegradorFinanceiro() { }

        public virtual ContaBancaria ContaBancaria 
        { 
            get => m_ContaBancaria;
            set
            {
                m_ContaBancaria = value ?? throw new ArgumentNullException(nameof(ContaBancaria));
            }
        }

        public virtual String1000? TokenAcesso 
        { 
            get => m_TokenAcesso;
            set => m_TokenAcesso = value;
        }

        public virtual EnumIntegracaoExterna IntegracaoExterna { get; set; }
    }
}
