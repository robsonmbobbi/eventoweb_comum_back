using EventoWeb.Comum.Negocio.Entidades.Financeiro;
using EventoWeb.Comum.Negocio.ObjetosValor;

namespace EventoWeb.Comum.Negocio.Entidades.IntegracaoFinanceira;

public class RegistroIntegracaoFinanceira : Entidade
{
    private IList<RegistroIntegracaoLog> m_Logs = [];
    private String1000? m_IdentificacaoNoIntegrador;
    private InteiroPositivo? m_NumeroParcelas;

    public RegistroIntegracaoFinanceira(IntegradorFinanceiro integrador, Conta conta, ValorMonetario valor, EnumTipoPagamento tipo, String1000 identificacaoNoIntegrador, InteiroPositivo? numeroParcelas = null)
    {
        Integrador = integrador ??  throw new ArgumentNullException(nameof(integrador));
        Conta = conta ??  throw new ArgumentNullException(nameof(conta));
        Valor = valor ?? throw new ArgumentNullException(nameof(valor));
        IdentificacaoNoIntegrador = identificacaoNoIntegrador ?? throw new ArgumentNullException(nameof(identificacaoNoIntegrador));
        Situacao = EnumSituacaoIntegracao.Pendente;
        Tipo = tipo;
        DataRegistro = DateTime.Now;
        NumeroParcelas = numeroParcelas;
    }

    protected RegistroIntegracaoFinanceira(){}

    public virtual IntegradorFinanceiro Integrador { get; protected set; }

    public virtual Conta Conta { get; protected set; }

    public virtual ValorMonetario Valor { get; protected set; }

    public virtual DateTime DataRegistro { get; protected set;}

    public virtual EnumTipoPagamento Tipo { get; protected set; }

    public virtual EnumSituacaoIntegracao Situacao { get; protected set;}

    public virtual InteiroPositivo? NumeroParcelas
    {
        get => m_NumeroParcelas;
        protected set => m_NumeroParcelas = value;
    }

    public virtual DateTime? DataConcluidoAbortado { get; protected set; }

    public virtual String1000? IdentificacaoNoIntegrador
    {
        get => m_IdentificacaoNoIntegrador;
        protected set => m_IdentificacaoNoIntegrador = value;
    }

    public virtual Transacao Transacao { get; protected set; }

    public virtual IEnumerable<RegistroIntegracaoLog> Logs => m_Logs;


    public virtual void Concluir(Transacao transacao)
    {
        ValidarSeConcluidoAbortado();

        Transacao = transacao ?? throw new ArgumentNullException(nameof(transacao));        
        DataConcluidoAbortado = DateTime.Now;
        Situacao = EnumSituacaoIntegracao.Concluido;

        AdicionarLog(EnumTipoLog.Info, "Conclusão");
    }

    public virtual void NotificarErro(string descricaoErro, string? dados = null)
    {
        ValidarSeConcluidoAbortado();

        Situacao = EnumSituacaoIntegracao.Erro;
        AdicionarLog(EnumTipoLog.Erro, descricaoErro, dados);
    }

    public virtual void Abortar(string descricaoMotivo, string? dados = null)
    {
        ValidarSeConcluidoAbortado();

        DataConcluidoAbortado = DateTime.Now;
        Situacao = EnumSituacaoIntegracao.Abortado;
        AdicionarLog(EnumTipoLog.Info, descricaoMotivo, dados);
    }

    private void ValidarSeConcluidoAbortado()
    {
        if (Situacao == EnumSituacaoIntegracao.Concluido)
            throw new Exception("Integração já concluída");

        if (Situacao == EnumSituacaoIntegracao.Abortado)
            throw new Exception("Integração já abortada");
    }

    public virtual void AdicionarLog(EnumTipoLog tipo, string mensagem, string? dados = null)
    {
        var msg = new String500(mensagem);
        var d = dados != null ? new String4000(dados) : null;
        m_Logs.Add(new RegistroIntegracaoLog(this, msg, tipo, d));
    }
}