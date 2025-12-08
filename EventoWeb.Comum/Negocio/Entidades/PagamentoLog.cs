namespace EventoWeb.Comum.Negocio.Entidades;

public enum EnumTipoPagamentoLog { Info, Erro, Conclusao }

public class PagamentoLog : Entidade
{
    public PagamentoLog(Pagamento pagamento, string mensagem, EnumTipoPagamentoLog tipo, 
        string? dados = null)
    {
        if (string.IsNullOrWhiteSpace(mensagem))
            throw new ArgumentException($"{nameof(mensagem)} não pode estar vazia ou em branco");
        
        Pagamento = pagamento ??  throw new ArgumentNullException(nameof(pagamento));
        Mensagem = mensagem;
        Tipo = tipo;
        Dados = dados;
        Data = DateTime.Now;
    }

    protected PagamentoLog(){}
    
    public virtual Pagamento Pagamento { get; }
    public virtual string Mensagem { get; }
    public virtual EnumTipoPagamentoLog Tipo { get; }
    public virtual DateTime Data { get; }
    public virtual string? Dados { get; }
}