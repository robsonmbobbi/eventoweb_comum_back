using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.ObjetosValor;

namespace EventoWeb.Comum.Testes.Negocio
{
    public class PrecoInscricaoTeste
    {
        [Fact]
        public void CriarPrecoInscricaoComDadosValidos_DeveDefinirPropriedadesCorretas()
        {
            // Arrange
            var evento = TestFixtures.Eventos.CriarEventoValido();
            var idadeMax = new InteiroPositivo(17);

            // Act
            var precoInscricao = new PrecoInscricao(evento, idadeMax);

            // Assert
            Assert.NotNull(precoInscricao);
            Assert.Equal(evento, precoInscricao.Evento);
            Assert.Equal(17, precoInscricao.IdadeMax.Valor);
            Assert.Empty(precoInscricao.Valores);
        }

        [Fact]
        public void CriarPrecoInscricaoComEventoNulo_DeveLancarExcecao()
        {
            // Arrange
            var idadeMax = new InteiroPositivo(17);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                new PrecoInscricao(null, idadeMax)
            );
        }

        [Fact]
        public void CriarPrecoInscricaoComIdadeMaxNula_DeveLancarExcecao()
        {
            // Arrange
            var evento = TestFixtures.Eventos.CriarEventoValido();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                new PrecoInscricao(evento, null)
            );
        }

        [Fact]
        public void AdicionarValorComDadosValidos_DeveAdicionarALista()
        {
            // Arrange
            var precoInscricao = TestFixtures.Precos.CriarPrecoInscricaoValido();
            var forma = TestFixtures.Precos.CriarFormaPagamentoValida();

            // Act
            precoInscricao.AdicionarValor(forma, 150.00m);

            // Assert
            Assert.Single(precoInscricao.Valores);
            Assert.Equal(forma.Id, precoInscricao.Valores.First().Forma.Id);
            Assert.Equal(150.00m, precoInscricao.Valores.First().Valor.Valor);
        }

        [Fact]
        public void AdicionarMultiplosValoresComFormasPagementoDiferentes_DeveAdicionarTodos()
        {
            // Arrange
            var precoInscricao = TestFixtures.Precos.CriarPrecoInscricaoValido();
            var forma1 = TestFixtures.Precos.CriarFormaPagamentoValida("Cartão de Crédito");
            var forma2 = TestFixtures.Precos.CriarFormaPagamentoValida("Boleto");
            var forma3 = TestFixtures.Precos.CriarFormaPagamentoValida("PIX");

            // Act
            precoInscricao.AdicionarValor(forma1, 100.00m);
            precoInscricao.AdicionarValor(forma2, 105.00m);
            precoInscricao.AdicionarValor(forma3, 110.00m);

            // Assert
            Assert.Equal(3, precoInscricao.Valores.Count());
        }

        [Fact]
        public void AdicionarValorParaMesmaFormaPagamentoDuasVezes_DeveLancarExcecao()
        {
            // Arrange
            var precoInscricao = TestFixtures.Precos.CriarPrecoInscricaoValido();
            var forma = TestFixtures.Precos.CriarFormaPagamentoValida();

            precoInscricao.AdicionarValor(forma, 150.00m);

            // Act & Assert
            Assert.Throws<Exception>(() => 
                precoInscricao.AdicionarValor(forma, 160.00m)
            );
        }

        [Fact]
        public void RemoverValorExistente_DeveRemoverDaLista()
        {
            // Arrange
            var precoInscricao = TestFixtures.Precos.CriarPrecoInscricaoValido();
            var forma1 = TestFixtures.Precos.CriarFormaPagamentoValida("Cartão de Crédito");
            var forma2 = TestFixtures.Precos.CriarFormaPagamentoValida("Boleto");

            precoInscricao.AdicionarValor(forma1, 100.00m);
            precoInscricao.AdicionarValor(forma2, 105.00m);

            var preco1 = precoInscricao.Valores.First();

            // Act
            precoInscricao.RemoverValor(preco1);

            // Assert
            Assert.Single(precoInscricao.Valores);
            Assert.Equal(forma2.Id, precoInscricao.Valores.First().Forma.Id);
        }

        [Fact]
        public void RemoverValorInexistente_DeveLancarExcecao()
        {
            // Arrange
            var precoInscricao = TestFixtures.Precos.CriarPrecoInscricaoValido();
            var forma = TestFixtures.Precos.CriarFormaPagamentoValida();
            precoInscricao.AdicionarValor(forma, 150.00m);

            var outroPreco = new PrecoInscricaoValor(
                precoInscricao,
                TestFixtures.Precos.CriarFormaPagamentoValida("Outra Forma"),
                new ValorMonetario(200.00m)
            );

            // Act & Assert
            Assert.Throws<Exception>(() => 
                precoInscricao.RemoverValor(outroPreco)
            );
        }

        [Fact]
        public void RemoverTodosOsValores_DeveEsvaziarLista()
        {
            // Arrange
            var precoInscricao = TestFixtures.Precos.CriarPrecoInscricaoValido();
            var forma1 = TestFixtures.Precos.CriarFormaPagamentoValida("Forma 1");
            var forma2 = TestFixtures.Precos.CriarFormaPagamentoValida("Forma 2");
            var forma3 = TestFixtures.Precos.CriarFormaPagamentoValida("Forma 3");

            precoInscricao.AdicionarValor(forma1, 100.00m);
            precoInscricao.AdicionarValor(forma2, 105.00m);
            precoInscricao.AdicionarValor(forma3, 110.00m);

            var valores = precoInscricao.Valores.ToList();

            // Act
            foreach (var valor in valores)
            {
                precoInscricao.RemoverValor(valor);
            }

            // Assert
            Assert.Empty(precoInscricao.Valores);
        }

        [Fact]
        public void AlterarIdadeMaxValida_DeveAlterarPropriedade()
        {
            // Arrange
            var precoInscricao = TestFixtures.Precos.CriarPrecoInscricaoValido();

            // Act
            precoInscricao.IdadeMax = new InteiroPositivo(25);

            // Assert
            Assert.Equal(25, precoInscricao.IdadeMax.Valor);
        }

        [Fact]
        public void AtribuirIdadeMaxNula_DeveLancarExcecao()
        {
            // Arrange
            var precoInscricao = TestFixtures.Precos.CriarPrecoInscricaoValido();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                precoInscricao.IdadeMax = null
            );
        }
    }
}
