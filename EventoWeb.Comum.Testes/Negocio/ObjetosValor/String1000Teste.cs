using EventoWeb.Comum.Negocio.ObjetosValor;
using Xunit;

namespace EventoWeb.Comum.Testes.Negocio.ObjetosValor
{
    public class String1000Teste
    {
        [Fact]
        public void CriarString1000_ComTextoValido_DeveCriarInstancia()
        {
            // Arrange
            var texto = "Token de acesso ou identificação com até 1000 caracteres para integração";

            // Act
            var string1000 = new String1000(texto);

            // Assert
            Assert.NotNull(string1000);
            Assert.Equal(texto, string1000.Valor);
        }

        [Fact]
        public void CriarString1000_ComLimiteExato1000_DeveCriarInstancia()
        {
            // Arrange
            var texto = new string('a', 1000);

            // Act
            var string1000 = new String1000(texto);

            // Assert
            Assert.NotNull(string1000);
            Assert.Equal(1000, string1000.Valor.Length);
        }

        [Fact]
        public void CriarString1000_ComTextoUnico_DeveCriarInstancia()
        {
            // Arrange
            var texto = "A";

            // Act
            var string1000 = new String1000(texto);

            // Assert
            Assert.NotNull(string1000);
            Assert.Equal(texto, string1000.Valor);
        }

        [Fact]
        public void CriarString1000_ComMaisDe1000Caracteres_DeveLancarArgumentException()
        {
            // Arrange
            var texto = new string('a', 1001);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new String1000(texto));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("\t\n")]
        public void CriarString1000_ComTextoNuloOuEmBranco_DeveLancarArgumentException(string textoInvalido)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new String1000(textoInvalido));
        }

        [Fact]
        public void CriarString1000_ComTextoApenasEspacos_DeveLancarArgumentException()
        {
            // Arrange
            var texto = "     ";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new String1000(texto));
        }

        [Fact]
        public void CriarString1000_ComEquivalenciaDeDados_DeveSerIgual()
        {
            // Arrange
            var texto = "Texto com até 1000 caracteres";
            var string10001 = new String1000(texto);
            var string10002 = new String1000(texto);

            // Act
            var saoIguais = string10001.Valor == string10002.Valor;

            // Assert
            Assert.True(saoIguais);
        }
    }
}
