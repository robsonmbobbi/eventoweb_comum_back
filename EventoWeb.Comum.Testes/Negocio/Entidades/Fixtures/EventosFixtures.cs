using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.ObjetosValor;

namespace EventoWeb.Comum.Testes.Negocio.Fixtures
{
    /// <summary>
    /// Fixtures para criação de dados de teste reutilizáveis relacionados a Evento
    /// </summary>
    public static class EventosFixtures
    {
        public static Evento CriarEventoValido()
        {
            var periodoInscricao = new Periodo(
                DateTime.Now,
                DateTime.Now.AddDays(30)
            );
            var periodoRealizacao = new Periodo(
                DateTime.Now.AddDays(31),
                DateTime.Now.AddDays(35)
            );

            return new Evento(
                new String200("Congresso de Espiritismo 2024"),
                periodoInscricao,
                periodoRealizacao
            );
        }

        public static Evento CriarEventoComNome(string nome)
        {
            var periodoInscricao = new Periodo(
                DateTime.Now,
                DateTime.Now.AddDays(30)
            );
            var periodoRealizacao = new Periodo(
                DateTime.Now.AddDays(31),
                DateTime.Now.AddDays(35)
            );

            return new Evento(
                new String200(nome),
                periodoInscricao,
                periodoRealizacao
            );
        }

        public static Evento CriarEventoComIdadeMinima(int idadeMinima)
        {
            var evento = CriarEventoValido();
            evento.IdadeMinimaInscricaoAdulto = new InteiroPositivo(idadeMinima);
            return evento;
        }
    }
}
