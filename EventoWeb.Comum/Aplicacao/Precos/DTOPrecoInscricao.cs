using EventoWeb.Comum.Aplicacao.FormasPagamento;

namespace EventoWeb.Comum.Aplicacao.Precos;

public class DTOPrecoInscricao
{
    public int Id { get; set; }
    public int IdadeMax { get; set; }
    
    public IList<DTOPrecosInscricaoForma> Valores { get; set; }
}

public class DTOPrecosInscricaoForma
{
    public DTOFormaPagamento Forma { get; set; }
    public decimal Preco { get; set; } 
}