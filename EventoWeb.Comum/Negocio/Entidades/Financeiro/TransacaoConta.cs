using EventoWeb.Comum.Negocio.ObjetosValor;

namespace EventoWeb.Comum.Negocio.Entidades.Financeiro
{
    public class TransacaoConta: Entidade
    {
        public TransacaoConta(ContaBancaria contaBancaria, Conta conta, DateTime data, ValorMonetario valorTransacao, ValorMonetario? multa = null, 
            ValorMonetario? juros = null, ValorMonetario? desconto = null) 
        {
            Conta = conta ?? throw new Exception($"{nameof(conta)} não pode ser nula");
            ValorTransacao = valorTransacao ?? throw new Exception($"{nameof(valorTransacao)} não pode ser nulo");
            Data = data;
            Multa = multa ?? new ValorMonetario(0);
            Juros = juros ?? new ValorMonetario(0);
            Desconto = desconto ?? new ValorMonetario(0);

            if (valorTransacao.Valor > 0)
            {
                var descricao = new String200($"Referente conta id {conta.Id}");
                Transacao = new Transacao(conta.Tipo, contaBancaria, data, valorTransacao, descricao);
            }
        }

        protected TransacaoConta() { }

        public virtual Transacao? Transacao { get; protected set; }
        public virtual Conta Conta { get; protected set; }
        public virtual DateTime Data { get; protected set; }
        public virtual ValorMonetario ValorTransacao { get; protected set; }
        public virtual ValorMonetario Multa { get; protected set; }
        public virtual ValorMonetario Juros { get; protected set; }
        public virtual ValorMonetario Desconto { get; protected set; }
    }
}
