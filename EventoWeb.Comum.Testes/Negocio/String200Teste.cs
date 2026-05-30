using System;
using EventoWeb.Comum.Negocio;
using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.ObjetosValor;
using Xunit;

namespace EventoWeb.Comum.Testes.Negocio
{
    public class String200Teste
    {
        [Fact]
        public void CriarString200_ComTextoValido_DeveCriarInstancia()
        {
            // Arrange
            var texto = "Criando com texto menor que 200 caracteres";

            // Act
            var string200 = new String200(texto);

            // Assert
            Assert.NotNull(string200);
            Assert.Equal(texto, string200.Valor);
        }

        [Fact]
        public void CriarString200_ComLimiteExato200_DeveCriarInstancia()
        {
            // Arrange
            var texto = new string('a', 200);

            // Act
            var string200 = new String200(texto);

            // Assert
            Assert.NotNull(string200);
            Assert.Equal(200, string200.Valor.Length);
        }

        [Fact]
        public void CriarString200_ComTextoUnico_DeveCriarInstancia()
        {
            // Arrange
            var texto = "A";

            // Act
            var string200 = new String200(texto);

            // Assert
            Assert.NotNull(string200);
            Assert.Equal(texto, string200.Valor);
        }

        [Fact]
        public void CriarString200_ComMaisDe200Caracteres_DeveLancarArgumentException()
        {
            // Arrange
            var texto = new string('a', 201);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new String200(texto));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("\t\n")]
        public void CriarString200_ComTextoNuloOuEmBranco_DeveLancarArgumentException(string textoInvalido)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new String200(textoInvalido));
        }

        [Fact]
        public void CriarString200_ComTextoApenasEspacos_DeveLancarArgumentException()
        {
            // Arrange
            var texto = "     ";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new String200(texto));
        }

        [Fact]
        public void CriarString200_ComEquivalenciaDeDados_DeveSerIgual()
        {
            // Arrange
            var texto = "Texto com até 200 caracteres";
            var string201 = new String200(texto);
            var string202 = new String200(texto);

            // Act
            var saoIguais = string201.Valor == string202.Valor;

            // Assert
            Assert.True(saoIguais);
        }
    }
}
