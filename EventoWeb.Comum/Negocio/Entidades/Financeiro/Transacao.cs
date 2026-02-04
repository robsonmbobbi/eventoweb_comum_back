using EventoWeb.Comum.Negocio.ObjetosValor;

namespace EventoWeb.Comum.Negocio.Entidades.Financeiro
{
    public class Transacao : Entidade
    {
        private string m_Descricao;
        private ValorMonetario m_Valor;

        public Transacao(EnumTipoTransacao tipo, ContaBancaria conta, DateTime dataHora, ValorMonetario valor, string descricao)
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

        public virtual String? Descricao
        {
            get { return m_Descricao; }
            protected set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new Exception($"{nameof(Descricao)} deve ser informada");

                m_Descricao = value;
            }
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
