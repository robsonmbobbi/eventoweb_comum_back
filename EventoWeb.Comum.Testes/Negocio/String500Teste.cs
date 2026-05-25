using EventoWeb.Comum.Negocio.ObjetosValor;
using Xunit;

namespace EventoWeb.Comum.Testes.Negocio
{
    public class String500Teste
    {
        [Fact]
        public void CriarString500_ComTextoValido_DeveCriarInstancia()
        {
            // Arrange
            var texto = "Este é um texto destinatário ou mensagem válida com até 500 caracteres";

            // Act
            var string500 = new String500(texto);

            // Assert
            Assert.NotNull(string500);
            Assert.Equal(texto, string500.Valor);
        }

        [Fact]
        public void CriarString500_ComLimiteExato500_DeveCriarInstancia()
        {
            // Arrange
            var texto = new string('a', 500);

            // Act
            var string500 = new String500(texto);

            // Assert
            Assert.NotNull(string500);
            Assert.Equal(500, string500.Valor.Length);
        }

        [Fact]
        public void CriarString500_ComTextoUnico_DeveCriarInstancia()
        {
            // Arrange
            var texto = "A";

            // Act
            var string500 = new String500(texto);

            // Assert
            Assert.NotNull(string500);
            Assert.Equal(texto, string500.Valor);
        }

        [Fact]
        public void CriarString500_ComMaisDe500Caracteres_DeveLancarArgumentException()
        {
            // Arrange
            var texto = new string('a', 501);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new String500(texto));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("\t\n")]
        public void CriarString500_ComTextoNuloOuEmBranco_DeveLancarArgumentException(string textoInvalido)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new String500(textoInvalido));
        }

        [Fact]
        public void CriarString500_ComTextoApenasEspacos_DeveLancarArgumentException()
        {
            // Arrange
            var texto = "     ";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new String500(texto));
        }

        [Fact]
        public void CriarString500_ComEquivalenciaDeDados_DeveSerIgual()
        {
            // Arrange
            var texto = "Texto com 500 chars máximo";
            var string5001 = new String500(texto);
            var string5002 = new String500(texto);

            // Act
            var saoIguais = string5001.Valor == string5002.Valor;

            // Assert
            Assert.True(saoIguais);
        }
    }
}
