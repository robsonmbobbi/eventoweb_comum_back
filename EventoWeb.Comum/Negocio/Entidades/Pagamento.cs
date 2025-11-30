namespace EventoWeb.Comum.Negocio.Entidades;

public class Pagamento
{
    private IList<PagamentoLog> m_Logs = [];
    private double m_Desconto = 0;
    private double m_Valor = 0;
    private int? m_NumeroParcelas;
    private EnumMeioPagamento? m_MeioPagamento;

    public Pagamento(Pedido pedido, double valor)
    {
        Pedido = pedido ??  throw new ArgumentNullException(nameof(pedido));
        Valor = valor;
        SituacaoPagamento = EnumSituacaoPagamento.Pendente;
        DataRegistro = DateTime.Now;
    }
    
    protected Pagamento(){}
    
    public virtual Pedido Pedido { get; }

    public virtual double Valor
    {
        get => m_Valor;
        set
        {
            ValidarSeConcluido();
            
            if (value < 0)
                throw new Exception($"{nameof(Valor)} não pode ser negativo.");
            
            if (value < m_Desconto)
                throw new Exception($"{nameof(Desconto)} tem valor maior que o nova valor do pagamento.");

            m_Valor = value;
        }
    }

    public virtual double Desconto
    {
        get => m_Desconto;
        set
        {
            ValidarSeConcluido();
            
            if (value < 0)
                throw new Exception($"{nameof(Desconto)} não pode ser negativo."); 
            
            if (value > Valor)
                throw new Exception($"{nameof(Desconto)} não pode ser maior que o valor do pagamento.");

            m_Desconto = value;
        }
    }
    public virtual double ValorPagar => Valor - Desconto;
    public virtual double? ValorPago { get; protected set; }
    public virtual DateTime? DataPago { get; protected set; }
    public virtual DateTime DataRegistro { get; }

    public virtual EnumMeioPagamento? MeioPagamento { get; protected set; }
    public virtual EnumSituacaoPagamento SituacaoPagamento { get; protected set;}

    public virtual int? NumeroParcelas
    {
        get => m_NumeroParcelas;
        set
        {
            ValidarSeConcluido();

            if (value != null && value <= 0)
                throw new Exception($"{nameof(NumeroParcelas)} deve ser maior que zero.");

            m_NumeroParcelas = value;
        }
    }
    public virtual IEnumerable<PagamentoLog> Logs => m_Logs;

   
    public virtual void Concluir(double valorPago, DateTime dataPago, EnumMeioPagamento meio)
    {
        ValidarSeConcluido();
        
        if (valorPago < 0)
            throw new Exception($"{nameof(valorPago)} não pode ser negativo.");

        MeioPagamento = meio;
        ValorPago =  valorPago;
        DataPago = dataPago;
        SituacaoPagamento = EnumSituacaoPagamento.Concluido;
        AdicionarLog(EnumTipoPagamentoLog.Conclusao);
    }

    public virtual void Errar(string descricaoErro)
    {
        ValidarSeConcluido();
        
        SituacaoPagamento = EnumSituacaoPagamento.Erro;
        AdicionarLog(EnumTipoPagamentoLog.Erro, descricaoErro);
    }
    
    private void ValidarSeConcluido()
    {
        if (SituacaoPagamento == EnumSituacaoPagamento.Concluido)
            throw new Exception("Pagamento já liquidado");
    }

    public virtual void AdicionarLog(EnumTipoPagamentoLog tipo, string? dados = null)
    {
        m_Logs.Add(new PagamentoLog(this, tipo, dados));
    }
}