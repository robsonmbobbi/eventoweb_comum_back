using EventoWeb.Comum.Negocio.ObjetosValor;

namespace EventoWeb.Comum.Negocio.Entidades.IntegracaoFinanceira;

public class RegistroIntegracaoLog : Entidade
{
    private String500? m_Mensagem;
    private String4000? m_Dados;

    public RegistroIntegracaoLog(RegistroIntegracaoFinanceira registro, String500 mensagem, EnumTipoLog tipo, 
        String4000? dados = null)
    {
        Registro = registro ??  throw new ArgumentNullException(nameof(registro));
        Mensagem = mensagem ?? throw new ArgumentNullException(nameof(mensagem));
        Tipo = tipo;
        Dados = dados;
        Data = DateTime.Now;
    }

    protected RegistroIntegracaoLog(){}

    public virtual RegistroIntegracaoFinanceira Registro { get; protected set; }

    public virtual String500? Mensagem
    {
        get => m_Mensagem;
        protected set => m_Mensagem = value;
    }

    public virtual EnumTipoLog Tipo { get; protected set; }
    public virtual DateTime Data { get; protected set; }

    public virtual String4000? Dados
    {
        get => m_Dados;
        protected set => m_Dados = value;
    }
}