using EventoWeb.Comum.Negocio.Entidades.Financeiro;

namespace EventoWeb.Comum.Aplicacao.FormasPagamento;

public static class Conversao
{
    public static DTOFormaPagamento Converter(this FormaPagamento forma)
    {
        return new DTOFormaPagamento
        {
            Id = forma.Id,
            Nome = forma.Nome.Nome,
            NrParcelasMinima = forma.NrParcelasMinima,
            NrParcelasMaxima = forma.NrParcelasMaxima,
            Tipo = forma.Tipo,
        };
    }
    
}