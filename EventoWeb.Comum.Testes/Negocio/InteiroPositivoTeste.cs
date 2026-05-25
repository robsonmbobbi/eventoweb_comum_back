using EventoWeb.Comum.Negocio.ObjetosValor;
using Xunit;

namespace EventoWeb.Comum.Testes.Negocio
{
    public class InteiroPositivoTeste
    {
        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(999)]
        [InlineData(int.MaxValue)]
        public void CriarInteiroPositivo_ComValorPositivo_DeveCriarInstancia(int valor)
        {
            // Act
            var inteiroPositivo = new InteiroPositivo(valor);

            // Assert
            Assert.NotNull(inteiroPositivo);
            Assert.Equal(valor, inteiroPositivo.Valor);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        [InlineData(int.MinValue)]
        public void CriarInteiroPositivo_ComValorNaoPositivo_DeveLancarArgumentException(int valor)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new InteiroPositivo(valor));
        }

        [Fact]
        public void CriarInteiroPositivo_ComValor1_DeveCriarInstancia()
        {
            // Arrange
            var valor = 1;

            // Act
            var inteiroPositivo = new InteiroPositivo(valor);

            // Assert
            Assert.NotNull(inteiroPositivo);
            Assert.Equal(1, inteiroPositivo.Valor);
        }

        [Fact]
        public void CriarInteiroPositivo_ComValorMuitoGrande_DeveCriarInstancia()
        {
            // Arrange
            var valor = int.MaxValue;

            // Act
            var inteiroPositivo = new InteiroPositivo(valor);

            // Assert
            Assert.NotNull(inteiroPositivo);
            Assert.Equal(int.MaxValue, inteiroPositivo.Valor);
        }

        [Fact]
        public void CriarInteiroPositivo_ComEquivalenciaDeDados_DeveSerIgual()
        {
            // Arrange
            var valor = 42;
            var inteiro1 = new InteiroPositivo(valor);
            var inteiro2 = new InteiroPositivo(valor);

            // Act
            var saoIguais = inteiro1.Valor == inteiro2.Valor;

            // Assert
            Assert.True(saoIguais);
        }

        [Fact]
        public void CriarInteiroPositivo_ComValorZero_DeveLancarArgumentException()
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => new InteiroPositivo(0));
            Assert.Contains("deve ser maior que zero", ex.Message);
        }

        [Fact]
        public void CriarInteiroPositivo_ComValorNegativo_DeveLancarArgumentException()
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => new InteiroPositivo(-5));
            Assert.Contains("deve ser maior que zero", ex.Message);
        }
    }
}
