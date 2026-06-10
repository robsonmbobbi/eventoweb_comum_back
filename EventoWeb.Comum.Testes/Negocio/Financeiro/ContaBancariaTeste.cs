using EventoWeb.Comum.Negocio.Entidades.Financeiro;
using EventoWeb.Comum.Negocio.ObjetosValor;
using EventoWeb.Comum.Testes.Negocio.Fixtures;
using static EventoWeb.Comum.Testes.Negocio.Fixtures.FinanceiroFixtures;

namespace EventoWeb.Comum.Testes.Negocio.Financeiro
{
    public class ContaBancariaTeste
    {
        [Fact]
        public void CriarContaBancariaComDadosValidos_DeveDefinirPropriedade()
        {
            // Arrange
            var nomeConta = new String200("Banco Brasil - Corrente");

            // Act
            var contaBancaria = new ContaBancaria(nomeConta);

            // Assert
            Assert.NotNull(contaBancaria);
            Assert.Equal("Banco Brasil - Corrente", contaBancaria.NomeConta.Valor);
        }

        [Fact]
        public void CriarContaBancariaComNomeNulo_DeveLancarExcecao()
        {
            // Act & Assert
            Assert.Throws<Exception>(() => 
                new ContaBancaria(null)
            );
        }

        [Fact]
        public void AlterarNomeContaValido_DeveAlterarPropriedade()
        {
            // Arrange
            var contaBancaria = CriarContaBancariaValida();
            var novoNome = new String200("Novo Banco");

            // Act
            contaBancaria.NomeConta = novoNome;

            // Assert
            Assert.Equal("Novo Banco", contaBancaria.NomeConta.Valor);
        }

        [Fact]
        public void AtribuirNomeNulo_DeveLancarExcecao()
        {
            // Arrange
            var contaBancaria = CriarContaBancariaValida();

            // Act & Assert
            Assert.Throws<Exception>(() => 
                contaBancaria.NomeConta = null
            );
        }
    }
}

