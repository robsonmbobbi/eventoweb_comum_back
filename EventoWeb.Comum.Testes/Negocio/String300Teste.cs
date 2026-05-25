using EventoWeb.Comum.Negocio.ObjetosValor;
using Xunit;

namespace EventoWeb.Comum.Testes.Negocio
{
    public class String300Teste
    {
        [Fact]
        public void CriarString300_ComTextoValido_DeveCriarInstancia()
        {
            // Arrange
            var texto = "Instituições espíritas de grande relevância";

            // Act
            var string300 = new String300(texto);

            // Assert
            Assert.NotNull(string300);
            Assert.Equal(texto, string300.Valor);
        }

        [Fact]
        public void CriarString300_ComLimiteExato300_DeveCriarInstancia()
        {
            // Arrange
            var texto = new string('a', 300);

            // Act
            var string300 = new String300(texto);

            // Assert
            Assert.NotNull(string300);
            Assert.Equal(300, string300.Valor.Length);
        }

        [Fact]
        public void CriarString300_ComTextoUnico_DeveCriarInstancia()
        {
            // Arrange
            var texto = "A";

            // Act
            var string300 = new String300(texto);

            // Assert
            Assert.NotNull(string300);
            Assert.Equal(texto, string300.Valor);
        }

        [Fact]
        public void CriarString300_ComMaisDe300Caracteres_DeveLancarArgumentException()
        {
            // Arrange
            var texto = new string('a', 301);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new String300(texto));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("\t\n")]
        public void CriarString300_ComTextoNuloOuEmBranco_DeveLancarArgumentException(string textoInvalido)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new String300(textoInvalido));
        }

        [Fact]
        public void CriarString300_ComTextoApenasEspacos_DeveLancarArgumentException()
        {
            // Arrange
            var texto = "     ";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new String300(texto));
        }

        [Fact]
        public void CriarString300_ComEquivalenciaDeDados_DeveSerIgual()
        {
            // Arrange
            var texto = "Texto igual";
            var string3001 = new String300(texto);
            var string3002 = new String300(texto);

            // Act
            var saoIguais = string3001.Valor == string3002.Valor;

            // Assert
            Assert.True(saoIguais);
        }
    }
}
