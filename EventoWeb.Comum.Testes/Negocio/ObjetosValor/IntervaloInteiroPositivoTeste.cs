using EventoWeb.Comum.Negocio.ObjetosValor;
using Xunit;

namespace EventoWeb.Comum.Testes.Negocio.ObjetosValor
{
    public class IntervaloInteiroPositivoTeste
    {
        [Fact]
        public void CriarIntervaloInteiroPositivo_ComValoresValidos_DeveCriarInstancia()
        {
            // Arrange
            var minimo = 1;
            var maximo = 12;

            // Act
            var intervalo = new IntervaloInteiroPositivo(minimo, maximo);

            // Assert
            Assert.NotNull(intervalo);
            Assert.Equal(minimo, intervalo.Minimo);
            Assert.Equal(maximo, intervalo.Maximo);
        }

        [Fact]
        public void CriarIntervaloInteiroPositivo_ComValoresIguais_DeveCriarInstancia()
        {
            // Arrange
            var minimo = 5;
            var maximo = 5;

            // Act
            var intervalo = new IntervaloInteiroPositivo(minimo, maximo);

            // Assert
            Assert.NotNull(intervalo);
            Assert.Equal(minimo, intervalo.Minimo);
            Assert.Equal(maximo, intervalo.Maximo);
        }

        [Fact]
        public void CriarIntervaloInteiroPositivo_ComZero_DeveLancarArgumentException()
        {
            // Arrange
            var minimo = 0;
            var maximo = 0;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new IntervaloInteiroPositivo(minimo, maximo));
        }

        [Fact]
        public void CriarIntervaloInteiroPositivo_ComMinimoZero_DeveLancarArgumentException()
        {
            // Arrange
            var minimo = 0;
            var maximo = 10;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new IntervaloInteiroPositivo(minimo, maximo));
        }

        [Fact]
        public void CriarIntervaloInteiroPositivo_ComMaximoMenorQueMinimo_DeveLancarArgumentException()
        {
            // Arrange
            var minimo = 10;
            var maximo = 5;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new IntervaloInteiroPositivo(minimo, maximo));
        }

        [Fact]
        public void CriarIntervaloInteiroPositivo_ComMinimoZero_EMaximoPositivo_DeveLancarArgumentException()
        {
            // Arrange
            var minimo = 0;
            var maximo = 10;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new IntervaloInteiroPositivo(minimo, maximo));
        }

        [Fact]
        public void CriarIntervaloInteiroPositivo_ComMinimoNegativo_DeveLancarArgumentException()
        {
            // Arrange
            var minimo = -1;
            var maximo = 10;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new IntervaloInteiroPositivo(minimo, maximo));
        }

        [Fact]
        public void CriarIntervaloInteiroPositivo_ComMaximoNegativo_DeveLancarArgumentException()
        {
            // Arrange
            var minimo = 1;
            var maximo = -5;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new IntervaloInteiroPositivo(minimo, maximo));
        }

        [Fact]
        public void CriarIntervaloInteiroPositivo_ComAmbosNegativo_DeveLancarArgumentException()
        {
            // Arrange
            var minimo = -10;
            var maximo = -5;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new IntervaloInteiroPositivo(minimo, maximo));
        }

        [Fact]
        public void CriarIntervaloInteiroPositivo_ComEquivalenciaDeDados_DeveSerIgual()
        {
            // Arrange
            var minimo = 1;
            var maximo = 12;
            var intervalo1 = new IntervaloInteiroPositivo(minimo, maximo);
            var intervalo2 = new IntervaloInteiroPositivo(minimo, maximo);

            // Act
            var saoIguais = intervalo1.Minimo == intervalo2.Minimo && intervalo1.Maximo == intervalo2.Maximo;

            // Assert
            Assert.True(saoIguais);
        }

        [Fact]
        public void CriarIntervaloInteiroPositivo_ComValoresMuitoGrandes_DeveCriarInstancia()
        {
            // Arrange
            var minimo = int.MaxValue - 1;
            var maximo = int.MaxValue;

            // Act
            var intervalo = new IntervaloInteiroPositivo(minimo, maximo);

            // Assert
            Assert.NotNull(intervalo);
            Assert.Equal(minimo, intervalo.Minimo);
            Assert.Equal(maximo, intervalo.Maximo);
        }
    }
}
