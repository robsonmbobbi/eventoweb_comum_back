using EventoWeb.Comum.Negocio.Entidades.Financeiro;
using EventoWeb.Comum.Negocio.Entidades.IntegracaoFinanceira;
using EventoWeb.Comum.Testes.Negocio.Fixtures;
using static EventoWeb.Comum.Testes.Negocio.Fixtures.FinanceiroFixtures;

namespace EventoWeb.Comum.Testes.Negocio.IntegracaoFinanceira
{
    public class IntegracaoFinanceiraPorFormaPagTeste
    {
        [Fact]
        public void CriarComDadosValidos_DeveDefinirPropriedadesCorretamente()
        {
            // Arrange
            var integrador = CriarIntegradorFinanceiroValido();
            var formaPagamento = CriarFormaPagamentoValida();

            // Act
            var integracao = new IntegracaoFinanceiraPorFormaPag(integrador, formaPagamento);

            // Assert
            Assert.NotNull(integracao);
            Assert.Equal(integrador, integracao.Integrador);
            Assert.Equal(formaPagamento, integracao.FormaPagamento);
        }

        [Fact]
        public void CriarComIntegradorNulo_DeveLancarExcecao()
        {
            // Arrange
            var formaPagamento = CriarFormaPagamentoValida();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new IntegracaoFinanceiraPorFormaPag(null, formaPagamento)
            );
        }

        [Fact]
        public void CriarComFormaPagamentoNula_DeveLancarExcecao()
        {
            // Arrange
            var integrador = CriarIntegradorFinanceiroValido();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new IntegracaoFinanceiraPorFormaPag(integrador, null)
            );
        }
    }
}
