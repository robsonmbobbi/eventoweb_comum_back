namespace EventoWeb.Comum.Aplicacao.Precos;

public class DTOPrecoInscricao
{
    public int Id { get; set; }
    public int IdadeMax { get; set; }
    
    public IList<DTOPrecosInscricaoForma> Valores { get; set; }
}

public class DTOPrecosInscricaoForma
{
    public DTOForma Forma { get; set; }
    public decimal Preco { get; set; } 
}

public class DTOForma 
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int NrParcelasMinima { get; set; }
    public int NrParcelasMaxima { get; set; }
}