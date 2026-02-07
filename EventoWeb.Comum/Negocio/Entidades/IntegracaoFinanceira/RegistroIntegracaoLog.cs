namespace EventoWeb.Comum.Negocio.Entidades.IntegracaoFinanceira;

public class RegistroIntegracaoLog : Entidade
{
    public RegistroIntegracaoLog(RegistroIntegracaoFinanceira registro, string mensagem, EnumTipoLog tipo, 
        string? dados = null)
    {
        if (string.IsNullOrWhiteSpace(mensagem))
            throw new ArgumentException($"{nameof(mensagem)} não pode estar vazia ou em branco");
        
        Registro = registro ??  throw new ArgumentNullException(nameof(registro));
        Mensagem = mensagem;
        Tipo = tipo;
        Dados = dados;
        Data = DateTime.Now;
    }

    protected RegistroIntegracaoLog(){}
    
    public virtual RegistroIntegracaoFinanceira Registro { get; protected set; }
    public virtual string Mensagem { get; protected set; }
    public virtual EnumTipoLog Tipo { get; protected set; }
    public virtual DateTime Data { get; protected set; }
    public virtual string? Dados { get; protected set; }
}