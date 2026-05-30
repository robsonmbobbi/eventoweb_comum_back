using EventoWeb.Comum.Negocio.Entidades.Financeiro;

namespace EventoWeb.Comum.Aplicacao.FormasPagamento;

public static class Conversao
{
    public static DTOFormaPagamento Converter(this FormaPagamento forma)
    {
        return new DTOFormaPagamento
        {
            Id = forma.Id,
            Nome = forma.Nome.Valor,
            NrParcelasMinima = forma.Parcelas?.Minimo ?? 1,
            NrParcelasMaxima = forma.Parcelas?.Maximo ?? 1,
            Tipo = forma.Tipo,
        };
    }

}