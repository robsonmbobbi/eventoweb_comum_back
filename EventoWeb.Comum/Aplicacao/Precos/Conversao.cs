using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Entidades.Financeiro;

namespace EventoWeb.Comum.Aplicacao.Precos;

public static class Conversao
{
    public static DTOPrecoInscricao Converter(this PrecoInscricao precoInscricao)
    {
        return new DTOPrecoInscricao
        {
            Id = precoInscricao.Id,
            IdadeMax = precoInscricao.IdadeMax,
            Valores = precoInscricao
                .Valores
                .Select(x => x.Converter())
                .ToList()
        };
    }

    public static DTOPrecosInscricaoForma Converter(this PrecoInscricaoValor valor)
    {
        return new DTOPrecosInscricaoForma
        {
            Forma = valor.Forma.Converter(),
            Preco = valor.Valor
        };
    }

    public static DTOForma Converter(this FormaPagamento forma)
    {
        return new DTOForma
        {
            Id = forma.Id,
            Nome = forma.Nome.Nome,
            NrParcelasMaxima = forma.NrParcelasMaxima,
            NrParcelasMinima = forma.NrParcelasMinima
        };
    }
}