using EventoWeb.Comum.Negocio.Entidades;

namespace EventoWeb.Comum.Aplicacao.Precos;

public static class Conversao
{
    public static DTOPrecoInscricao Converter(this PrecoInscricao precoInscricao)
    {
        return new DTOPrecoInscricao
        {
            Id = precoInscricao.Id,
            IdadeMax = precoInscricao.IdadeMax,
            Preco = precoInscricao.Preco
        };
    }
    
}