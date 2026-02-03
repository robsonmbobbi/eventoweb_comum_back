namespace EventoWeb.Comum.Negocio.Entidades;

public class FormaPagamento : Entidade
{
    private NomeCompleto m_Nome;
    private int m_NrParcelasMinima;
    private int m_NrParcelasMaxima;

    public FormaPagamento(NomeCompleto nome)
    {
        Nome = nome;
        m_NrParcelasMaxima = 1;
        m_NrParcelasMinima = 1;
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

    public virtual int NrParcelasMinima => m_NrParcelasMinima;
    public virtual int NrParcelasMaxima => m_NrParcelasMaxima;

    public virtual void DefinirParcelas(int minimas, int maximas)
    {
        if (minimas < 1)
            throw new ArgumentOutOfRangeException(nameof(minimas), "O número mínimo de parcelas deve ser maior ou igual a 1.");
        
        if (maximas < minimas)
            throw new ArgumentOutOfRangeException(nameof(maximas), "O número máximo de parcelas deve ser maior ou igual ao número mínimo de parcelas.");

        m_NrParcelasMinima = minimas;
        m_NrParcelasMaxima = maximas;
    }
}