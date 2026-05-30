using EventoWeb.Comum.Negocio.ObjetosValor;

namespace EventoWeb.Comum.Negocio.Entidades.Financeiro
{
    public class Conta : Entidade
    {
        private ValorMonetario m_Valor;
        private StringClob? m_Descricao;
        private DateTime m_DataVencimento;
        private bool m_Liquidado;
        private IList<TransacaoConta> m_Transacoes;
        private EnumTipoTransacao m_Tipo;

        public Conta(Pessoa pessoa, EnumTipoTransacao tipo, ValorMonetario valor, DateTime dataVencimento)
        {
            Pessoa = pessoa ?? throw new Exception($"{nameof(pessoa)} não pode ser nulo.");
            DataCriado = DateTime.Now;
            Liquidado = false;
            DataVencimento = dataVencimento;
            Valor = valor;
            Tipo = tipo;

            m_Transacoes = new List<TransacaoConta>();

            ValorTotalTransacoes = new (0);
            ValorTotalDesconto = new (0);
            ValorTotalJuros = new (0);
            ValorTotalMulta = new (0);
        }

        protected Conta() { }

        public virtual Pessoa Pessoa { get; protected set; }

        public virtual ValorMonetario Valor
        {
            get => m_Valor;
            set
            {
                ValidarSeContaLiquidada();
                m_Valor = value;
            }
        }

        public virtual bool Liquidado
        {
            get => m_Liquidado;
            set
            {
                ValidarSeContaLiquidada();
                m_Liquidado = value;
            }
        }

        public virtual DateTime DataCriado { get; protected set; }

        public virtual StringClob? Descricao
        {
            get => m_Descricao;
            set
            {
                ValidarSeContaLiquidada();
                m_Descricao = value;
            }
        }

        public virtual DateTime DataVencimento
        {
            get => m_DataVencimento;
            set
            {
                ValidarSeContaLiquidada();
                m_DataVencimento = value;
            }
        }

        public virtual EnumTipoTransacao Tipo
        {
            get => m_Tipo;
            set
            {
                ValidarSeContaLiquidada();
                m_Tipo = value;
            }
        }

        public virtual ValorMonetario ValorTotalTransacoes { get; protected set; }
        public virtual ValorMonetario ValorTotalDesconto { get; protected set; }
        public virtual ValorMonetario ValorTotalJuros { get; protected set; }
        public virtual ValorMonetario ValorTotalMulta { get; protected set; }

        public virtual IEnumerable<TransacaoConta> Transacoes => m_Transacoes;       

        private void ValidarSeContaLiquidada()
        {
            if (Liquidado)
                throw new Exception("A conta já está liquidada");
        }

        public virtual void AdicionarTransacao(ContaBancaria contaBancaria, DateTime data, ValorMonetario valorTransacao,
            ValorMonetario? multa = null, ValorMonetario? juros = null, ValorMonetario? desconto = null)
        {
            ValidarSeContaLiquidada();

            m_Transacoes.Add(new TransacaoConta(contaBancaria, this, data, valorTransacao, multa, juros, desconto));

            ValorTotalTransacoes = new (m_Transacoes.Sum(tc => tc.ValorTransacao.Valor));
            ValorTotalDesconto = new (m_Transacoes.Sum(tc => tc.Desconto.Valor));
            ValorTotalJuros = new (m_Transacoes.Sum(tc => tc.Juros.Valor));
            ValorTotalMulta = new (m_Transacoes.Sum(tc => tc.Multa.Valor));

            if (ValorTotalTransacoes.Valor >= Valor.Valor)
                Liquidado = true;
        }

        public virtual void RemoverTransacao(TransacaoConta transacaoConta)
        {
            ValidarSeContaLiquidada();

            if (!m_Transacoes.Remove(transacaoConta))
                throw new Exception("A transação informada não pertence a esta conta.");

            ValorTotalTransacoes = new (m_Transacoes.Sum(tc => tc.ValorTransacao.Valor));
            ValorTotalDesconto = new (m_Transacoes.Sum(tc => tc.Desconto.Valor));
            ValorTotalJuros = new (m_Transacoes.Sum(tc => tc.Juros.Valor));
            ValorTotalMulta = new (m_Transacoes.Sum(tc => tc.Multa.Valor));
        }

        public virtual void Reabrir()
        {
            if (!Liquidado)
                throw new Exception("A conta não está liquidada.");

            Liquidado = false;
            m_Transacoes.Clear();

            ValorTotalTransacoes = new (0);
            ValorTotalDesconto = new (0);
            ValorTotalJuros = new (0);
            ValorTotalMulta = new (0);
        }
    }
}
