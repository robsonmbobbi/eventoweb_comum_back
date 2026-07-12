using EventoWeb.Comum.Negocio.Entidades;
using static EventoWeb.Comum.Testes.Negocio.Fixtures.EventosFixtures;
using static EventoWeb.Comum.Testes.Negocio.Fixtures.PessoasFixtures;

namespace EventoWeb.Comum.Testes.Negocio.Fixtures
{
    /// <summary>
    /// Fixtures para criação de dados de teste reutilizáveis relacionados a Inscrição
    /// </summary>
    public static class InscricoesFixtures
    {
        public static InscricaoParticipante CriarInscricaoParticipanteValida(
            Evento? evento = null,
            Pessoa? pessoa = null)
        {
            evento ??= CriarEventoValido();
            pessoa ??= CriarAdulto();

            return new InscricaoParticipante(
                evento,
                pessoa,
                DateTime.Now
            );
        }

        public static InscricaoParticipante CriarInscricaoParticipanteEmPendente(
            Evento? evento = null,
            Pessoa? pessoa = null)
        {
            var inscricao = CriarInscricaoParticipanteValida(evento, pessoa);
            inscricao.TornarPendente();
            return inscricao;
        }

        public static InscricaoParticipante CriarInscricaoParticipanteAceita(
            Evento? evento = null,
            Pessoa? pessoa = null)
        {
            var inscricao = CriarInscricaoParticipanteEmPendente(evento, pessoa);
            inscricao.Tipo = EnumTipoParticipante.Participante;
            inscricao.Aceitar();
            return inscricao;
        }
    }
}
