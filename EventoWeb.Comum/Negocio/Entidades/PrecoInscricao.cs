using EventoWeb.Comum.Negocio.Entidades.Financeiro;
using EventoWeb.Comum.Negocio.ObjetosValor;

namespace EventoWeb.Comum.Negocio.Entidades;

public class PrecoInscricao : Entidade
{
    private InteiroPositivo m_IdadeMax;
    private IList<PrecoInscricaoValor> m_Valores = [];

    public PrecoInscricao(Evento evento, InteiroPositivo idadeMax)
    {
        Evento = evento ?? throw new ArgumentNullException(nameof(evento));
        IdadeMax = idadeMax;
    }

    protected PrecoInscricao()
    {
    }

    public virtual Evento Evento { get; protected set; }

    public virtual InteiroPositivo IdadeMax
    {
        get => m_IdadeMax;
        set => m_IdadeMax = value ?? throw new ArgumentNullException(nameof(IdadeMax));
    }

    public virtual IEnumerable<PrecoInscricaoValor> Valores => m_Valores;

    public virtual void AdicionarValor(FormaPagamento forma, decimal valor)
    {
        if (m_Valores.Any(x => x.Forma == forma))
            throw new Exception("Já existe um valor com essa forma de pagamento.");

        m_Valores.Add(new PrecoInscricaoValor(this, forma, new ValorMonetario(valor)));
    }

    public virtual void RemoverValor(PrecoInscricaoValor preco)
    {
        if (!m_Valores.Remove(preco))
            throw new Exception($"Preço com o id {preco.Id} não encontrado na lista para exclusão");
    }
}