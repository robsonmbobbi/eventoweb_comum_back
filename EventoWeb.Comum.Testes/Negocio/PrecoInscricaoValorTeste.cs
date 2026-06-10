using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.ObjetosValor;

using EventoWeb.Comum.Testes.Negocio.Fixtures;
using static EventoWeb.Comum.Testes.Negocio.Fixtures.PrecosFixtures;

namespace EventoWeb.Comum.Testes.Negocio
{
    public class PrecoInscricaoValorTeste
    {
        [Fact]
        public void CriarPrecoInscricaoValorComDadosValidos_DeveDefinirPropriedadesCorretas()
        {
            // Arrange
            var precoInscricao = CriarPrecoInscricaoValido();
            var forma = CriarFormaPagamentoValida();
            var valor = new ValorMonetario(200.00m);

            // Act
            var precoValor = new PrecoInscricaoValor(precoInscricao, forma, valor);

            // Assert
            Assert.NotNull(precoValor);
            Assert.Equal(precoInscricao, precoValor.Preco);
            Assert.Equal(forma, precoValor.Forma);
            Assert.Equal(200.00m, precoValor.Valor.Valor);
        }

        [Fact]
        public void CriarPrecoInscricaoValorComPrecoNulo_DeveLancarExcecao()
        {
            // Arrange
            var forma = CriarFormaPagamentoValida();
            var valor = new ValorMonetario(200.00m);

            // Act & Assert
            Assert.Throws<Exception>(() => 
                new PrecoInscricaoValor(null, forma, valor)
            );
        }

        [Fact]
        public void CriarPrecoInscricaoValorComFormaNula_DeveLancarExcecao()
        {
            // Arrange
            var precoInscricao = CriarPrecoInscricaoValido();
            var valor = new ValorMonetario(200.00m);

            // Act & Assert
            Assert.Throws<Exception>(() => 
                new PrecoInscricaoValor(precoInscricao, null, valor)
            );
        }

        [Fact]
        public void CriarPrecoInscricaoValorComValorNulo_DeveLancarExcecao()
        {
            // Arrange
            var precoInscricao = CriarPrecoInscricaoValido();
            var forma = CriarFormaPagamentoValida();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                new PrecoInscricaoValor(precoInscricao, forma, null)
            );
        }

        [Fact]
        public void AlterarValorMonetarioValido_DeveAlterarPropriedade()
        {
            // Arrange
            var precoInscricao = CriarPrecoInscricaoValido();
            var forma = CriarFormaPagamentoValida();
            var precoValor = new PrecoInscricaoValor(precoInscricao, forma, new ValorMonetario(200.00m));

            // Act
            precoValor.Valor = new ValorMonetario(250.00m);

            // Assert
            Assert.Equal(250.00m, precoValor.Valor.Valor);
        }

        [Fact]
        public void AtribuirValorNulo_DeveLancarExcecao()
        {
            // Arrange
            var precoInscricao = CriarPrecoInscricaoValido();
            var forma = CriarFormaPagamentoValida();
            var precoValor = new PrecoInscricaoValor(precoInscricao, forma, new ValorMonetario(200.00m));

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                precoValor.Valor = null
            );
        }
    }
}

