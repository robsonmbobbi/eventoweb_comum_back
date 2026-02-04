using EventoWeb.Comum.Negocio.Entidades.Financeiro;

namespace EventoWeb.Comum.Negocio.Entidades;

public class PrecoInscricaoValor: Entidade
{
    private decimal m_Valor;

    public PrecoInscricaoValor(PrecoInscricao preco, FormaPagamento forma, decimal valor)
    {
        Preco = preco ?? throw new Exception($"{nameof(preco)} não pode ser nulo");
        Forma = forma ?? throw new Exception($"{nameof(forma)} não pode ser nulo");
        Valor = valor; 
    }
    
    protected PrecoInscricaoValor() { }
    
    public virtual PrecoInscricao Preco { get; protected set; }
    public virtual FormaPagamento Forma { get; protected set; }

    public virtual decimal Valor
    {
        get => m_Valor;
        set
        {
            if (value < 0)
                throw new Exception($"{nameof(Valor)} deve ser maior ou igual a zero.");

            m_Valor = value;
        }
        
    }
}