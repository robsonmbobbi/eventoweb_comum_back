using EventoWeb.Comum.Negocio.Entidades.Financeiro;

namespace EventoWeb.Comum.Negocio.Entidades;

public class PrecoInscricao : Entidade
{
    private int m_IdadeMax;
    private IList<PrecoInscricaoValor> m_Valores = [];

    public PrecoInscricao(Evento evento, int idadeMax)
    {
        Evento = evento ?? throw new ArgumentNullException(nameof(evento));
        IdadeMax = idadeMax;
    }

    protected PrecoInscricao()
    {
    }

    public virtual Evento Evento { get; protected set; }

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
    
    public virtual IEnumerable<PrecoInscricaoValor> Valores => m_Valores;

    public virtual void AdicionarValor(FormaPagamento forma, decimal valor)
    {
        if (m_Valores.Any(x => x.Forma.Id == forma.Id))
            throw new Exception("Já existe um valor com aessa forma de pagamento.");
        
        m_Valores.Add(new PrecoInscricaoValor(this, forma, valor));
    }

    public virtual void RemoverValor(PrecoInscricaoValor preco)
    {
        if (!m_Valores.Remove(preco))
            throw new Exception($"Preço com o id {preco.Id} não encontrado na lista para exclusão");
    }
}