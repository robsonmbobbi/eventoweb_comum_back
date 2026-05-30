using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Aplicacao.FormasPagamento;

namespace EventoWeb.Comum.Aplicacao.Precos;

public static class Conversao
{
    public static DTOPrecoInscricao Converter(this PrecoInscricao precoInscricao)
    {
        return new DTOPrecoInscricao
        {
            Id = precoInscricao.Id,
            IdadeMax = precoInscricao.IdadeMax.Valor,
            Valores = [.. precoInscricao
                .Valores
                .Select(x => x.Converter())]
        };
    }

    public static DTOPrecosInscricaoForma Converter(this PrecoInscricaoValor valor)
    { 
        return new DTOPrecosInscricaoForma
        {
            Forma = valor.Forma.Converter(),
            Preco = valor.Valor.Valor
        };
    }
}