using EventoWeb.Comum.Negocio.Entidades;

namespace EventoWeb.Comum.Aplicacao.Eventos;

public static class Conversao
{
    public static DTOEvento Converter(this Evento evento)
    {
        return new DTOEvento
        {
            Id = evento.Id,
            DataFinalInscricao = evento.PeriodoInscricaoOnLine.DataFinal,
            DataInicialInscricao = evento.PeriodoInscricaoOnLine.DataInicial,
            DataFinalRealizacao = evento.PeriodoRealizacaoEvento.DataFinal,
            DataInicialRealizacao = evento.PeriodoRealizacaoEvento.DataInicial,
            IdadeMinimaAdulto = evento.IdadeMinimaInscricaoAdulto,
            Logotipo = evento.Logotipo == null ? null : Convert.ToBase64String(evento.Logotipo.Arquivo),
            Nome = evento.Nome.Nome,
            Regulamento = evento.Regulamento?.Valor
        };
    }

}