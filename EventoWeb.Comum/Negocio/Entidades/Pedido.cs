using System.Reflection.Emit;

namespace EventoWeb.Comum.Negocio.Entidades;

public enum EnumFormaPagamento { DebitoAplicado, SolicitacaoDesconto, SolicitacaoIsencao, DescontoAplicado, IsencaoAplicada }

public class Pedido : Entidade
{
    private IList<Inscricao> m_Inscricoes;

    public Pedido(IEnumerable<Inscricao> inscricoes, double valor, EnumFormaPagamento formaPagamento)
    {
        if (!inscricoes.Any())
            throw new Exception($"{nameof(inscricoes)} não pode ser vazio.");

        if (valor < 0)
            throw new Exception($"{nameof(valor)} não pode ser negativo.");

        m_Inscricoes = new List<Inscricao>(inscricoes);
        Valor = valor;
        FormaPagamento = formaPagamento;

        Pagamento = new Pagamento(this, valor);
    }

    protected Pedido()
    {
    }

    public virtual IEnumerable<Inscricao> Inscricoes => m_Inscricoes;
    public virtual double Valor { get; }
    public virtual EnumFormaPagamento FormaPagamento { get; protected set; }
    public virtual Pagamento Pagamento { get; }

    public virtual void AplicarDesconto(double valorDesconto)
    {
        FormaPagamento = EnumFormaPagamento.DescontoAplicado;
        Pagamento.Desconto = valorDesconto;
    }

    public virtual void AplicarIsencao()
    {
        FormaPagamento = EnumFormaPagamento.IsencaoAplicada;
        Pagamento.Desconto = Pagamento.Valor;
        Pagamento.Concluir(0, DateTime.Now, EnumMeioPagamento.Dinheiro);
    }
}