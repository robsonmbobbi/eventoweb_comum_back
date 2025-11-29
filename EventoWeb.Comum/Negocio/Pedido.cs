namespace EventoWeb.Comum.Negocio;

public enum EnumFormaPagamento { Credito, Debito, PIX, Desconto, Isencao }

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
    }

    protected Pedido()
    {
    }

    public virtual IEnumerable<Inscricao> Inscricoes => m_Inscricoes;
    public virtual double Valor { get; }
    public virtual EnumFormaPagamento FormaPagamento { get; }
}