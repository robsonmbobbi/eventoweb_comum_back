using EventoWeb.Comum.Negocio.Entidades.Financeiro;
using EventoWeb.Comum.Negocio.ObjetosValor;

namespace EventoWeb.Comum.Negocio.Entidades;

public class PrecoInscricaoValor: Entidade
{
    private ValorMonetario m_Valor;

    public PrecoInscricaoValor(PrecoInscricao preco, FormaPagamento forma, ValorMonetario valor)
    {
        Preco = preco ?? throw new Exception($"{nameof(preco)} não pode ser nulo");
        Forma = forma ?? throw new Exception($"{nameof(forma)} não pode ser nulo");
        Valor = valor; 
    }
    
    protected PrecoInscricaoValor() { }
    
    public virtual PrecoInscricao Preco { get; protected set; }
    public virtual FormaPagamento Forma { get; protected set; }

    public virtual ValorMonetario Valor
    {
        get => m_Valor;
        set => m_Valor = value ?? throw new ArgumentNullException(nameof(Valor));

    }
}