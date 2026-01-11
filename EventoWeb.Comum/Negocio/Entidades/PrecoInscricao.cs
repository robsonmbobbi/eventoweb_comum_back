namespace EventoWeb.Comum.Negocio.Entidades;

public class PrecoInscricao : Entidade
{
    private int m_IdadeMax;
    private double m_Preco;

    public PrecoInscricao(Evento evento, int idadeMax, double preco)
    {
        Evento = evento ?? throw new ArgumentNullException(nameof(evento));
        IdadeMax = idadeMax;
        Preco = preco;
    }

    protected PrecoInscricao()
    {
    }

    public virtual Evento Evento { get; }

    public virtual int IdadeMax
    {
        get => m_IdadeMax;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException($"{nameof(value)} deve ser maior ou igual a zero.");
            m_IdadeMax = value;
        }
    }
    public virtual double Preco
    {
        get => m_Preco;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException($"{nameof(value)} deve ser maior ou igual a zero.");
            m_Preco = value;
        }
    }
}