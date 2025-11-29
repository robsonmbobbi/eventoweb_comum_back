namespace EventoWeb.Comum.Negocio;

public enum EnumMeioPagamento { Credito, Debito, PIX, Dinheiro }
public enum EnumSituacaoPagamento { Pendente, Concluido, Erro }

public class Pagamento
{
    private IList<PagamentoLog> m_Logs = [];
    
    public Pagamento(Pedido pedido, double valor, double desconto, 
        EnumMeioPagamento meioPagamento, int? numeroParcelas = null)
    {
        if (valor < 0)
            throw new Exception($"{nameof(valor)} não pode ser negativo.");
        
        if (desconto < 0)
            throw new Exception($"{nameof(desconto)} não pode ser negativo.");        
        
        Pedido = pedido ??  throw new ArgumentNullException(nameof(pedido));
        Valor = valor;
        Desconto = desconto;
        MeioPagamento = meioPagamento;
        NumeroParcelas = numeroParcelas;
        SituacaoPagamento = EnumSituacaoPagamento.Pendente;
        DataRegistro = DateTime.Now;
    }
    
    protected Pagamento(){}
    
    public virtual Pedido Pedido { get; }
    public virtual double Valor { get; }
    public virtual double Desconto { get; }
    public virtual double ValorPagar => Valor - Desconto;
    public virtual double? ValorPago { get; protected set; }
    public virtual DateTime? DataPago { get; protected set; }
    public virtual DateTime DataRegistro { get; }
    public virtual EnumMeioPagamento  MeioPagamento { get; }
    public virtual EnumSituacaoPagamento SituacaoPagamento { get; protected set;}
    public virtual int? NumeroParcelas { get; }
    public virtual IEnumerable<PagamentoLog> Logs => m_Logs;
    
    public virtual void Concluir(double valorPago, DateTime dataPago)
    {
        if (valorPago < 0)
            throw new Exception($"{nameof(valorPago)} não pode ser negativo.");
        
        ValorPago =  valorPago;
        DataPago = dataPago;
        SituacaoPagamento = EnumSituacaoPagamento.Concluido;
        AdicionarLog(EnumTipoPagamentoLog.Conclusao);
    }

    public virtual void Errar(string descricaoErro)
    {
        SituacaoPagamento = EnumSituacaoPagamento.Erro;
        AdicionarLog(EnumTipoPagamentoLog.Erro, descricaoErro);
    }

    public virtual void AdicionarLog(EnumTipoPagamentoLog tipo, string? dados = null)
    {
        m_Logs.Add(new PagamentoLog(this, tipo, dados));
    }
}