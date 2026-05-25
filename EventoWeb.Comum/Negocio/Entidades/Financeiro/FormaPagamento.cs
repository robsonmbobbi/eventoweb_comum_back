using EventoWeb.Comum.Negocio.ObjetosValor;

namespace EventoWeb.Comum.Negocio.Entidades.Financeiro;

public class FormaPagamento : Entidade
{
    private NomeCompleto m_Nome;
    private IntervaloInteiroPositivo? m_Parcelas;

    public FormaPagamento(NomeCompleto nome, EnumTipoPagamento tipo)
    {
        Nome = nome;
        Tipo = tipo;
        m_Parcelas = new IntervaloInteiroPositivo(1, 1);
    }

    protected FormaPagamento()
    {
    }

    public virtual NomeCompleto Nome
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
        if (parcelas == null)
            throw new ArgumentNullException(nameof(parcelas));

        m_Parcelas = parcelas;
    }
}