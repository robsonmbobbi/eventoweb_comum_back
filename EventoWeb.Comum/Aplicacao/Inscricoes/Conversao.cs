using EventoWeb.Comum.Negocio.Entidades;

namespace EventoWeb.Comum.Aplicacao.Inscricoes;

public static class Conversao
{
    public static DTOPessoa Converter(this Pessoa pessoa)
    {
        return new DTOPessoa
        {
            Nome = pessoa.Nome.Nome,
            DataNascimento = pessoa.DataNascimento.Data,
            CPF = pessoa.CPF.Numero,
            AlergiaAlimentos = pessoa.AlergiaAlimentos,
            Celular = pessoa.CelularWP.Numero,
            EhDiabetico = pessoa.EhDiabetico,
            EhVegetariano = pessoa.EhVegetariano,
            Email = pessoa.Email.Endereco,
            Sexo = pessoa.Sexo,
            UsaAdocanteDiariamente = pessoa.UsaAdocanteDiariamente,
        };
    }

    public static DTOResponsavel ConverterResponsavel(this Inscricao inscricao)
    {
        return new DTOResponsavel
        {
            CPF = inscricao.Pessoa.CPF.Numero,
            Nome = inscricao.Pessoa.Nome.Nome,
            IdInscricao = inscricao.Id
        };
    }

    public static DTOInscricao Converter(this Inscricao inscricao)
    {
        var dto = new DTOInscricao
        {
            DormeEvento = inscricao.DormeEvento,
            Id = inscricao.Id,
            IdEvento = inscricao.Evento.Id,
            NomeCracha = inscricao.NomeCracha,
            Observacoes = inscricao.Observacoes,
            Pessoa = inscricao.Pessoa.Converter(),
            Situacao = inscricao.Situacao
        };

        if (inscricao is InscricaoParticipante participante)
        {
            dto.Tipo = EnumTipoInscricao.Adulto;
            dto.InstituicoesEspiritasFrequenta = participante.InstituicoesEspiritasFrequenta;
        }
        else if (inscricao is InscricaoInfantil infantil)
        {
            dto.Tipo = EnumTipoInscricao.Infantil;
            dto.Responsavel1 = infantil.InscricaoResponsavel1.ConverterResponsavel();
            dto.Responsavel2 = infantil.InscricaoResponsavel2?.ConverterResponsavel();
        }

        return dto;
    }
}