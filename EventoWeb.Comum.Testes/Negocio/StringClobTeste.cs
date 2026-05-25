using EventoWeb.Comum.Negocio.ObjetosValor;
using Xunit;

namespace EventoWeb.Comum.Testes.Negocio
{
    public class StringClobTeste
    {
        [Fact]
        public void CriarStringClob_ComTextoValido_DeveCriarInstancia()
        {
            // Arrange
            var texto = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.";

            // Act
            var stringClob = new StringClob(texto);

            // Assert
            Assert.NotNull(stringClob);
            Assert.Equal(texto, stringClob.Valor);
        }

        [Fact]
        public void CriarStringClob_ComTextoMuitoGrande_DeveCriarInstancia()
        {
            // Arrange
            var texto = new string('a', 10000); // Sem limite de caracteres

            // Act
            var stringClob = new StringClob(texto);

            // Assert
            Assert.NotNull(stringClob);
            Assert.Equal(texto, stringClob.Valor);
            Assert.Equal(10000, stringClob.Valor.Length);
        }

        [Fact]
        public void CriarStringClob_ComTextoUnico_DeveCriarInstancia()
        {
            // Arrange
            var texto = "a";

            // Act
            var stringClob = new StringClob(texto);

            // Assert
            Assert.NotNull(stringClob);
            Assert.Equal(texto, stringClob.Valor);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("\t\n")]
        public void CriarStringClob_ComTextoNuloOuEmBranco_DeveLancarArgumentException(string textoInvalido)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new StringClob(textoInvalido));
        }

        [Fact]
        public void CriarStringClob_ComTextoApenasEspacos_DeveLancarArgumentException()
        {
            // Arrange
            var texto = "     ";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new StringClob(texto));
        }

        [Fact]
        public void CriarStringClob_ComEquivalenciaDeDados_DeveSerIgual()
        {
            // Arrange
            var texto = "Mesmo texto";
            var stringClob1 = new StringClob(texto);
            var stringClob2 = new StringClob(texto);

            // Act
            var saoIguais = stringClob1.Valor == stringClob2.Valor;

            // Assert
            Assert.True(saoIguais);
        }
    }
}
