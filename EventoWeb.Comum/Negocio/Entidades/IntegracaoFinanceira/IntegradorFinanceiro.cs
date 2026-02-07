using EventoWeb.Comum.Negocio.Entidades.Financeiro;

namespace EventoWeb.Comum.Negocio.Entidades.IntegracaoFinanceira
{
    public class IntegradorFinanceiro : Entidade
    {
        private ContaBancaria m_ContaBancaria;
        private string m_TokenAcesso;

        public IntegradorFinanceiro(ContaBancaria contaBancaria, string tokenAcesso, EnumIntegracaoExterna integracaoExterna)
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
        
        public virtual string TokenAcesso 
        { 
            get=> m_TokenAcesso;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Token de acesso não pode ser nulo ou vazio.", nameof(TokenAcesso));
                
                m_TokenAcesso = value;
            }
        }
    
        public virtual EnumIntegracaoExterna IntegracaoExterna { get; set; }
    }
}
