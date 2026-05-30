using EventoWeb.Comum.Negocio.ObjetosValor;

namespace EventoWeb.Comum.Negocio.Entidades.Financeiro
{
    public class Transacao : Entidade
    {
        private String200? m_Descricao;
        private ValorMonetario m_Valor;

        public Transacao(EnumTipoTransacao tipo, ContaBancaria conta, DateTime dataHora, ValorMonetario valor, String200 descricao)
        {
            ContaBancaria = conta ?? throw new Exception($"{nameof(conta)} não pode ser nula.");

            DataHora = dataHora;           
            Valor = valor;
            Tipo = tipo;
            Descricao = descricao;
        }

        protected Transacao() { }

        public virtual ContaBancaria ContaBancaria { get; protected set; }

        public virtual DateTime DataHora { get; protected set; }

        public virtual String200? Descricao
        {
            get => m_Descricao;
            protected set => m_Descricao = value;
        }

        public virtual ValorMonetario Valor
        {
            get { return m_Valor; }
            protected set
            {
                m_Valor = value ?? throw new Exception($"{nameof(Valor)} não pode ser nulo.");
            }
        }

        public virtual EnumTipoTransacao Tipo { get; protected set; }
    }
}
