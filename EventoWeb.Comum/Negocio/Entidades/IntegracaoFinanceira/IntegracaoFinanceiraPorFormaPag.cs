using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Entidades.Financeiro;

namespace EventoWeb.Comum.Negocio.Entidades.IntegracaoFinanceira
{
    public class IntegracaoFinanceiraPorFormaPag: Entidade
    {
        public IntegracaoFinanceiraPorFormaPag(Evento evento, IntegradorFinanceiro integrador, FormaPagamento formaPagamento)
        {
            Evento = evento ?? throw new ArgumentNullException(nameof(evento));
            Integrador = integrador ?? throw new ArgumentNullException(nameof(integrador));
            FormaPagamento = formaPagamento ?? throw new ArgumentNullException(nameof(formaPagamento));

            if (Integrador.Evento != Evento)
                throw new ArgumentException("O integrador deve ser do mesmo evento da integração.", nameof(integrador));
        }

        protected IntegracaoFinanceiraPorFormaPag() { }

        public virtual Evento Evento { get; protected set; }
        public virtual IntegradorFinanceiro Integrador { get; protected set; }
        public virtual FormaPagamento FormaPagamento { get; protected set; }
    }
}
