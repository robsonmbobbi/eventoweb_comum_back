using EventoWeb.Comum.Negocio.Entidades.Financeiro;
using EventoWeb.Comum.Negocio.ObjetosValor;

namespace EventoWeb.Comum.Negocio.Entidades.IntegracaoFinanceira;

public class RegistroIntegracaoFinanceira : Entidade
{
    private IList<RegistroIntegracaoLog> m_Logs = [];

    public RegistroIntegracaoFinanceira(IntegradorFinanceiro integrador, Conta conta, ValorMonetario valor, EnumTipoIntegracao tipo, int? numeroParcelas = null)
    {
        Integrador = integrador ??  throw new ArgumentNullException(nameof(integrador));
        Conta = conta ??  throw new ArgumentNullException(nameof(conta));
        Valor = valor ?? throw new ArgumentNullException(nameof(valor));
        Situacao = EnumSituacaoIntegracao.Pendente;
        Tipo = tipo;
        DataRegistro = DateTime.Now;

        if (tipo == EnumTipoIntegracao.CreditoParcelado && (!numeroParcelas.HasValue || numeroParcelas <= 1))
            throw new ArgumentException("Número de parcelas deve ser informado e maior que 1 para crédito parcelado.", nameof(numeroParcelas));
        NumeroParcelas = numeroParcelas;
    }
    
    protected RegistroIntegracaoFinanceira(){}
    
    public virtual IntegradorFinanceiro Integrador { get; protected set; }

    public virtual Conta Conta { get; protected set; }

    public virtual ValorMonetario Valor { get; protected set; }

    public virtual DateTime DataRegistro { get; protected set;}

    public virtual EnumTipoIntegracao Tipo { get; protected set; }

    public virtual EnumSituacaoIntegracao Situacao { get; protected set;}

    public virtual int? NumeroParcelas { get; protected set; }

    public virtual DateTime? DataConcluidoAbortado { get; protected set; }

    public virtual string? IdentificacaoNoIntegrador { get; protected set; }

    public virtual Transacao? Transacao { get; protected set; }

    public virtual IEnumerable<RegistroIntegracaoLog> Logs => m_Logs;

   
    public virtual void Concluir(Transacao transacao, string IdentificacaoNoIntegrador)
    {
        ValidarSeConcluidoAbortado();

        Transacao = transacao ?? throw new ArgumentNullException(nameof(transacao));
        IdentificacaoNoIntegrador = IdentificacaoNoIntegrador ?? throw new ArgumentNullException(nameof(IdentificacaoNoIntegrador));
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
        ValidarSeConcluidoAbortado();
        m_Logs.Add(new RegistroIntegracaoLog(this, mensagem, tipo, dados));
    }
}