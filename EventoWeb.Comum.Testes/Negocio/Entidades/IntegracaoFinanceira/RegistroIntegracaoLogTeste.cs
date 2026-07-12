using EventoWeb.Comum.Negocio.Entidades.IntegracaoFinanceira;
using EventoWeb.Comum.Negocio.ObjetosValor;
using EventoWeb.Comum.Testes.Negocio.Fixtures;
using static EventoWeb.Comum.Testes.Negocio.Fixtures.FinanceiroFixtures;

namespace EventoWeb.Comum.Testes.Negocio.IntegracaoFinanceira
{
    public class RegistroIntegracaoLogTeste
    {
        [Fact]
        public void CriarComDadosValidos_DeveDefinirPropriedadesCorretamente()
        {
            // Arrange
            var registro = CriarRegistroIntegracaoFinanceiraValida();
            var mensagem = new String500("Log de teste");
            var tipo = EnumTipoLog.Info;

            // Act
            var log = new RegistroIntegracaoLog(registro, mensagem, tipo);

            // Assert
            Assert.NotNull(log);
            Assert.Equal(registro, log.Registro);
            Assert.Equal("Log de teste", log.Mensagem.Valor);
            Assert.Equal(EnumTipoLog.Info, log.Tipo);
            Assert.Null(log.Dados);
        }

        [Fact]
        public void CriarComRegistroNulo_DeveLancarExcecao()
        {
            // Arrange
            var mensagem = new String500("Log de teste");

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new RegistroIntegracaoLog(null, mensagem, EnumTipoLog.Info)
            );
        }

        [Fact]
        public void CriarComMensagemNula_DeveLancarExcecao()
        {
            // Arrange
            var registro = CriarRegistroIntegracaoFinanceiraValida();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new RegistroIntegracaoLog(registro, null, EnumTipoLog.Info)
            );
        }

        [Fact]
        public void CriarComDadosOpcionais_DevePermitirDadosNulos()
        {
            // Arrange
            var registro = CriarRegistroIntegracaoFinanceiraValida();
            var mensagem = new String500("Log sem dados");

            // Act
            var log = new RegistroIntegracaoLog(registro, mensagem, EnumTipoLog.Erro, null);

            // Assert
            Assert.NotNull(log);
            Assert.Null(log.Dados);
            Assert.Equal(EnumTipoLog.Erro, log.Tipo);
        }

        [Fact]
        public void DataDeveSuperiorOuIgualAoMomentoAtual()
        {
            // Arrange
            var dataBefore = DateTime.Now;
            var registro = CriarRegistroIntegracaoFinanceiraValida();
            var mensagem = new String500("Log com data");

            // Act
            var log = new RegistroIntegracaoLog(registro, mensagem, EnumTipoLog.Info);
            var dataAfter = DateTime.Now.AddSeconds(1);

            // Assert
            Assert.True(log.Data >= dataBefore && log.Data <= dataAfter);
        }
    }
}
