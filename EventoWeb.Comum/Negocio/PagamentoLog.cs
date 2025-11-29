namespace EventoWeb.Comum.Negocio;

public enum EnumTipoPagamentoLog { Info, Erro, Conclusao }

public class PagamentoLog
{
    public PagamentoLog(Pagamento pagamento, EnumTipoPagamentoLog tipo, string? dados = null)
    {
        Pagamento = pagamento ??  throw new ArgumentNullException(nameof(pagamento));
        Tipo = tipo;
        Dados = dados;
        Data = DateTime.Now;
    }

    protected PagamentoLog(){}
    
    public virtual Pagamento Pagamento { get; }
    public virtual EnumTipoPagamentoLog Tipo { get; }
    public virtual DateTime Data { get; }
    public virtual string? Dados { get; }
}