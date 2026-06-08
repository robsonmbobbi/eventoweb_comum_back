using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.ObjetosValor;

namespace EventoWeb.Comum.Testes.Negocio
{
    public class EventoTeste
    {
        [Fact]
        public void CriarEventoComDadosValidos_DeveDefinirPropriedadesCorretas()
        {
            // Arrange
            var nome = new String200("Congresso de Espiritismo 2024");
            var periodoInscricao = new Periodo(
                new DateTime(2024, 1, 1),
                new DateTime(2024, 6, 30)
            );
            var periodoRealizacao = new Periodo(
                new DateTime(2024, 7, 15),
                new DateTime(2024, 7, 20)
            );

            // Act
            var evento = new Evento(nome, periodoInscricao, periodoRealizacao);

            // Assert
            Assert.NotNull(evento);
            Assert.Equal("Congresso de Espiritismo 2024", evento.Nome.Valor);
            Assert.Equal(periodoInscricao, evento.PeriodoInscricaoOnLine);
            Assert.Equal(periodoRealizacao, evento.PeriodoRealizacaoEvento);
            Assert.Equal(DateTime.Today, evento.DataRegistro);
            Assert.Equal(13, evento.IdadeMinimaInscricaoAdulto.Valor);
            Assert.Null(evento.Logotipo);
            Assert.Null(evento.Regulamento);
        }

        [Fact]
        public void CriarEventoComNomeNulo_DeveLancarExcecao()
        {
            // Arrange
            var periodoInscricao = new Periodo(DateTime.Now, DateTime.Now.AddDays(30));
            var periodoRealizacao = new Periodo(DateTime.Now.AddDays(31), DateTime.Now.AddDays(35));

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                new Evento(null, periodoInscricao, periodoRealizacao)
            );
        }

        [Fact]
        public void CriarEventoComPeriodoInscricaoNulo_DeveLancarExcecao()
        {
            // Arrange
            var nome = new String200("Evento Teste");
            var periodoRealizacao = new Periodo(DateTime.Now.AddDays(31), DateTime.Now.AddDays(35));

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                new Evento(nome, null, periodoRealizacao)
            );
        }

        [Fact]
        public void CriarEventoComPeriodoRealizacaoNulo_DeveLancarExcecao()
        {
            // Arrange
            var nome = new String200("Evento Teste");
            var periodoInscricao = new Periodo(DateTime.Now, DateTime.Now.AddDays(30));

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                new Evento(nome, periodoInscricao, null)
            );
        }

        [Fact]
        public void AlterarNomeValido_DeveAlterarPropriedade()
        {
            // Arrange
            var evento = TestFixtures.Eventos.CriarEventoValido();
            var novoNome = new String200("Novo Nome");

            // Act
            evento.Nome = novoNome;

            // Assert
            Assert.Equal("Novo Nome", evento.Nome.Valor);
        }

        [Fact]
        public void AtribuirNomeNuloAoSetter_DeveLancarExcecao()
        {
            // Arrange
            var evento = TestFixtures.Eventos.CriarEventoValido();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => evento.Nome = null);
        }

        [Fact]
        public void AlterarPeriodoInscricaoValido_DeveAlterarPropriedade()
        {
            // Arrange
            var evento = TestFixtures.Eventos.CriarEventoValido();
            var novoPeriodo = new Periodo(
                DateTime.Now.AddDays(10),
                DateTime.Now.AddDays(40)
            );

            // Act
            evento.PeriodoInscricaoOnLine = novoPeriodo;

            // Assert
            Assert.Equal(novoPeriodo, evento.PeriodoInscricaoOnLine);
        }

        [Fact]
        public void AtribuirPeriodoInscricaoNuloAoSetter_DeveLancarExcecao()
        {
            // Arrange
            var evento = TestFixtures.Eventos.CriarEventoValido();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => evento.PeriodoInscricaoOnLine = null);
        }

        [Fact]
        public void AlterarPeriodoRealizacaoValido_DeveAlterarPropriedade()
        {
            // Arrange
            var evento = TestFixtures.Eventos.CriarEventoValido();
            var novoPeriodo = new Periodo(
                DateTime.Now.AddDays(50),
                DateTime.Now.AddDays(60)
            );

            // Act
            evento.PeriodoRealizacaoEvento = novoPeriodo;

            // Assert
            Assert.Equal(novoPeriodo, evento.PeriodoRealizacaoEvento);
        }

        [Fact]
        public void AtribuirPeriodoRealizacaoNuloAoSetter_DeveLancarExcecao()
        {
            // Arrange
            var evento = TestFixtures.Eventos.CriarEventoValido();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => evento.PeriodoRealizacaoEvento = null);
        }

        [Fact]
        public void AtribuirLogotipo_DeveAlterarPropriedade()
        {
            // Arrange
            var evento = TestFixtures.Eventos.CriarEventoValido();
            var logotipo = TestFixtures.Arquivos.CriarImagemPNG();

            // Act
            evento.Logotipo = logotipo;

            // Assert
            Assert.Equal(logotipo, evento.Logotipo);
        }

        [Fact]
        public void LogotipoNuloEhValido()
        {
            // Arrange
            var evento = TestFixtures.Eventos.CriarEventoValido();
            evento.Logotipo = TestFixtures.Arquivos.CriarImagemPNG();

            // Act
            evento.Logotipo = null;

            // Assert
            Assert.Null(evento.Logotipo);
        }

        [Fact]
        public void AtribuirRegulamento_DeveAlterarPropriedade()
        {
            // Arrange
            var evento = TestFixtures.Eventos.CriarEventoValido();
            var regulamento = new StringClob("Texto do regulamento do evento");

            // Act
            evento.Regulamento = regulamento;

            // Assert
            Assert.NotNull(evento.Regulamento);
        }

        [Fact]
        public void RegulamentoNuloEhValido()
        {
            // Arrange
            var evento = TestFixtures.Eventos.CriarEventoValido();
            evento.Regulamento = new StringClob("Texto do regulamento");

            // Act
            evento.Regulamento = null;

            // Assert
            Assert.Null(evento.Regulamento);
        }

        [Fact]
        public void AlterarIdadeMinimaInscricao_DeveAlterarPropriedade()
        {
            // Arrange
            var evento = TestFixtures.Eventos.CriarEventoValido();

            // Act
            evento.IdadeMinimaInscricaoAdulto = new InteiroPositivo(18);

            // Assert
            Assert.Equal(18, evento.IdadeMinimaInscricaoAdulto.Valor);
        }

        [Fact]
        public void DataRegistroEhDefinidaAutomaticamente()
        {
            // Arrange
            var periodoInscricao = new Periodo(DateTime.Now, DateTime.Now.AddDays(30));
            var periodoRealizacao = new Periodo(DateTime.Now.AddDays(31), DateTime.Now.AddDays(35));

            // Act
            var evento = new Evento(
                new String200("Evento Teste"),
                periodoInscricao,
                periodoRealizacao
            );

            // Assert
            Assert.Equal(DateTime.Today, evento.DataRegistro);
        }

        [Fact]
        public void DataRegistroEhSomenteLeitura()
        {
            // Arrange
            var evento = TestFixtures.Eventos.CriarEventoValido();
            var propriedade = typeof(Evento).GetProperty("DataRegistro");

            // Assert - Verifica que não tem setter público
            Assert.NotNull(propriedade);
            Assert.Null(propriedade.SetMethod);
        }
    }
}
