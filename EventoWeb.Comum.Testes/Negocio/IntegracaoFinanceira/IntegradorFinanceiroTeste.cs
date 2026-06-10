using EventoWeb.Comum.Negocio.Entidades.Financeiro;
using EventoWeb.Comum.Negocio.Entidades.IntegracaoFinanceira;
using EventoWeb.Comum.Negocio.ObjetosValor;
using EventoWeb.Comum.Testes.Negocio.Fixtures;
using static EventoWeb.Comum.Testes.Negocio.Fixtures.FinanceiroFixtures;

namespace EventoWeb.Comum.Testes.Negocio.IntegracaoFinanceira
{
    public class IntegradorFinanceiroTeste
    {
        [Fact]
        public void CriarComDadosValidos_DeveDefinirPropriedadesCorretamente()
        {
            // Arrange
            var contaBancaria = CriarContaBancariaValida();
            var tokenAcesso = new String1000("token_teste_123");
            var integracaoExterna = EnumIntegracaoExterna.Asaas;

            // Act
            var integrador = new IntegradorFinanceiro(contaBancaria, tokenAcesso, integracaoExterna);

            // Assert
            Assert.NotNull(integrador);
            Assert.Equal(contaBancaria, integrador.ContaBancaria);
            Assert.Equal("token_teste_123", integrador.TokenAcesso.Valor);
            Assert.Equal(EnumIntegracaoExterna.Asaas, integrador.IntegracaoExterna);
        }

        [Fact]
        public void CriarComContaBancariaNula_DeveLancarExcecao()
        {
            // Arrange
            var tokenAcesso = new String1000("token_teste_123");

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new IntegradorFinanceiro(null, tokenAcesso, EnumIntegracaoExterna.Asaas)
            );
        }

        [Fact]
        public void CriarComTokenAcessoNulo_DeveLancarExcecao()
        {
            // Arrange
            var contaBancaria = CriarContaBancariaValida();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new IntegradorFinanceiro(contaBancaria, null, EnumIntegracaoExterna.Asaas)
            );
        }

        [Fact]
        public void AlterarContaBancariaValida_DeveAtualizarPropriedade()
        {
            // Arrange
            var integrador = CriarIntegradorFinanceiroValido();
            var novaContaBancaria = new ContaBancaria(new String200("Banco Itaú - Corrente"));

            // Act
            integrador.ContaBancaria = novaContaBancaria;

            // Assert
            Assert.Equal(novaContaBancaria, integrador.ContaBancaria);
        }

        [Fact]
        public void AlterarContaBancariaNula_DeveLancarExcecao()
        {
            // Arrange
            var integrador = CriarIntegradorFinanceiroValido();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                integrador.ContaBancaria = null
            );
        }

        [Fact]
        public void AlterarTokenAcessoValido_DeveAtualizarPropriedade()
        {
            // Arrange
            var integrador = CriarIntegradorFinanceiroValido();
            var novoToken = new String1000("novo_token_456");

            // Act
            integrador.TokenAcesso = novoToken;

            // Assert
            Assert.Equal("novo_token_456", integrador.TokenAcesso.Valor);
        }

        [Fact]
        public void AlterarTokenAcessoNulo_DeveLancarExcecao()
        {
            // Arrange
            var integrador = CriarIntegradorFinanceiroValido();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                integrador.TokenAcesso = null
            );
        }
    }
}
