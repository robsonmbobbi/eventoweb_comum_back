using EventoWeb.Comum.Negocio.ObjetosValor;

namespace EventoWeb.Comum.Negocio.Entidades.Financeiro;

public class FormaPagamento : Entidade
{
    private String200 m_Nome;
    private IntervaloInteiroPositivo? m_Parcelas;

    public FormaPagamento(Evento evento, String200 nome, EnumTipoPagamento tipo)
    {
        Evento = evento ?? throw new Exception($"{nameof(Evento)} não pode ser nulo");
        Nome = nome;
        Tipo = tipo;
        m_Parcelas = new IntervaloInteiroPositivo(1, 1);
    }

    protected FormaPagamento()
    {
    }

    public virtual Evento Evento { get; protected set; }

    public virtual String200 Nome
    {
        get => m_Nome;
        set
        {
            if (value == null)
                throw new ArgumentNullException(nameof(Nome));

            m_Nome = value;
        }
    }

    public virtual EnumTipoPagamento Tipo { get; set; }

    public virtual IntervaloInteiroPositivo? Parcelas
    {
        get => m_Parcelas;
        protected set => m_Parcelas = value;
    }

    public virtual void DefinirParcelas(IntervaloInteiroPositivo parcelas)
    {
        ArgumentNullException.ThrowIfNull(parcelas);

        m_Parcelas = parcelas;
    }
}