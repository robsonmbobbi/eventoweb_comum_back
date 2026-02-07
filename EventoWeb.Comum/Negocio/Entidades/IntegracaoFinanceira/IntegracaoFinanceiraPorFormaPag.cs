using EventoWeb.Comum.Negocio.Entidades.Financeiro;

namespace EventoWeb.Comum.Negocio.Entidades.IntegracaoFinanceira
{
    public class IntegracaoFinanceiraPorFormaPag: Entidade
    {
        public IntegracaoFinanceiraPorFormaPag(IntegradorFinanceiro integrador, FormaPagamento formaPagamento, EnumIntegracaoExterna integracaoExterna, EnumTipoIntegracao tipoIntegracao)
        {
            Integrador = integrador ?? throw new ArgumentNullException(nameof(integrador));
            FormaPagamento = formaPagamento ?? throw new ArgumentNullException(nameof(formaPagamento));
            IntegracaoExterna = integracaoExterna;
        }

        protected IntegracaoFinanceiraPorFormaPag() { }

        public virtual IntegradorFinanceiro Integrador { get; protected set; }
        public virtual FormaPagamento FormaPagamento { get; protected set; }
        public virtual EnumIntegracaoExterna IntegracaoExterna { get; protected set; }
        public virtual EnumTipoIntegracao TipoIntegracao { get; protected set; }
    }
}
