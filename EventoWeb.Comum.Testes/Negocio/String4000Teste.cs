using EventoWeb.Comum.Negocio.ObjetosValor;
using Xunit;

namespace EventoWeb.Comum.Testes.Negocio
{
    public class String4000Teste
    {
        [Fact]
        public void CriarString4000_ComTextoValido_DeveCriarInstancia()
        {
            // Arrange
            var texto = "Dados completos ou mensagem com até 4000 caracteres para logs detalhados";

            // Act
            var string4000 = new String4000(texto);

            // Assert
            Assert.NotNull(string4000);
            Assert.Equal(texto, string4000.Valor);
        }

        [Fact]
        public void CriarString4000_ComLimiteExato4000_DeveCriarInstancia()
        {
            // Arrange
            var texto = new string('a', 4000);

            // Act
            var string4000 = new String4000(texto);

            // Assert
            Assert.NotNull(string4000);
            Assert.Equal(4000, string4000.Valor.Length);
        }

        [Fact]
        public void CriarString4000_ComTextoUnico_DeveCriarInstancia()
        {
            // Arrange
            var texto = "A";

            // Act
            var string4000 = new String4000(texto);

            // Assert
            Assert.NotNull(string4000);
            Assert.Equal(texto, string4000.Valor);
        }

        [Fact]
        public void CriarString4000_ComMaisDe4000Caracteres_DeveLancarArgumentException()
        {
            // Arrange
            var texto = new string('a', 4001);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new String4000(texto));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("\t\n")]
        public void CriarString4000_ComTextoNuloOuEmBranco_DeveLancarArgumentException(string textoInvalido)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new String4000(textoInvalido));
        }

        [Fact]
        public void CriarString4000_ComTextoApenasEspacos_DeveLancarArgumentException()
        {
            // Arrange
            var texto = "     ";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new String4000(texto));
        }

        [Fact]
        public void CriarString4000_ComEquivalenciaDeDados_DeveSerIgual()
        {
            // Arrange
            var texto = "Texto com até 4000 caracteres";
            var string40001 = new String4000(texto);
            var string40002 = new String4000(texto);

            // Act
            var saoIguais = string40001.Valor == string40002.Valor;

            // Assert
            Assert.True(saoIguais);
        }
    }
}
