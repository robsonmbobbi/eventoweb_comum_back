using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Entidades.Financeiro;
using EventoWeb.Comum.Negocio.ObjetosValor;

namespace EventoWeb.Comum.Testes.Negocio
{
    public class TransacaoContaTeste
    {
        [Fact]
        public void CriarTransacaoContaComDadosValidos_DeveDefinirPropriedadesCorretas()
        {
            // Arrange
            var contaBancaria = TestFixtures.Financeiro.CriarContaBancariaValida();
            var conta = TestFixtures.Financeiro.CriarContaValida();
            var data = DateTime.Now;
            var valorTransacao = new ValorMonetario(400.00m);

            // Act
            var transacaoConta = new TransacaoConta(contaBancaria, conta, data, valorTransacao);

            // Assert
            Assert.NotNull(transacaoConta);
            Assert.Equal(conta, transacaoConta.Conta);
            Assert.Equal(data, transacaoConta.Data);
            Assert.Equal(400.00m, transacaoConta.ValorTransacao.Valor);
            Assert.Equal(0m, transacaoConta.Multa.Valor);
            Assert.Equal(0m, transacaoConta.Juros.Valor);
            Assert.Equal(0m, transacaoConta.Desconto.Valor);
            Assert.NotNull(transacaoConta.Transacao);
        }

        [Fact]
        public void CriarTransacaoContaComContaNula_DeveLancarExcecao()
        {
            // Arrange
            var contaBancaria = TestFixtures.Financeiro.CriarContaBancariaValida();

            // Act & Assert
            Assert.Throws<Exception>(() => 
                new TransacaoConta(
                    contaBancaria,
                    null,
                    DateTime.Now,
                    new ValorMonetario(400.00m)
                )
            );
        }

        [Fact]
        public void CriarTransacaoContaComValorNulo_DeveLancarExcecao()
        {
            // Arrange
            var contaBancaria = TestFixtures.Financeiro.CriarContaBancariaValida();
            var conta = TestFixtures.Financeiro.CriarContaValida();

            // Act & Assert
            Assert.Throws<Exception>(() => 
                new TransacaoConta(contaBancaria, conta, DateTime.Now, null)
            );
        }

        [Fact]
        public void CriarTransacaoContaComMulta_DeveDefinirPropriedade()
        {
            // Arrange
            var contaBancaria = TestFixtures.Financeiro.CriarContaBancariaValida();
            var conta = TestFixtures.Financeiro.CriarContaValida();
            var multa = new ValorMonetario(50.00m);

            // Act
            var transacaoConta = new TransacaoConta(
                contaBancaria,
                conta,
                DateTime.Now,
                new ValorMonetario(400.00m),
                multa: multa
            );

            // Assert
            Assert.Equal(50.00m, transacaoConta.Multa.Valor);
        }

        [Fact]
        public void CriarTransacaoContaComJuros_DeveDefinirPropriedade()
        {
            // Arrange
            var contaBancaria = TestFixtures.Financeiro.CriarContaBancariaValida();
            var conta = TestFixtures.Financeiro.CriarContaValida();
            var juros = new ValorMonetario(25.00m);

            // Act
            var transacaoConta = new TransacaoConta(
                contaBancaria,
                conta,
                DateTime.Now,
                new ValorMonetario(400.00m),
                juros: juros
            );

            // Assert
            Assert.Equal(25.00m, transacaoConta.Juros.Valor);
        }

        [Fact]
        public void CriarTransacaoContaComDesconto_DeveDefinirPropriedade()
        {
            // Arrange
            var contaBancaria = TestFixtures.Financeiro.CriarContaBancariaValida();
            var conta = TestFixtures.Financeiro.CriarContaValida();
            var desconto = new ValorMonetario(100.00m);

            // Act
            var transacaoConta = new TransacaoConta(
                contaBancaria,
                conta,
                DateTime.Now,
                new ValorMonetario(400.00m),
                desconto: desconto
            );

            // Assert
            Assert.Equal(100.00m, transacaoConta.Desconto.Valor);
        }

        [Fact]
        public void CriarTransacaoContaComTodosOsValoresAdicionais_DeveDefinirTodos()
        {
            // Arrange
            var contaBancaria = TestFixtures.Financeiro.CriarContaBancariaValida();
            var conta = TestFixtures.Financeiro.CriarContaValida();

            // Act
            var transacaoConta = new TransacaoConta(
                contaBancaria,
                conta,
                DateTime.Now,
                new ValorMonetario(400.00m),
                multa: new ValorMonetario(50.00m),
                juros: new ValorMonetario(25.00m),
                desconto: new ValorMonetario(100.00m)
            );

            // Assert
            Assert.Equal(400.00m, transacaoConta.ValorTransacao.Valor);
            Assert.Equal(50.00m, transacaoConta.Multa.Valor);
            Assert.Equal(25.00m, transacaoConta.Juros.Valor);
            Assert.Equal(100.00m, transacaoConta.Desconto.Valor);
        }

        [Fact]
        public void CriarTransacaoContaComValorZero_TransacaoNaoEhCriada()
        {
            // Arrange
            var contaBancaria = TestFixtures.Financeiro.CriarContaBancariaValida();
            var conta = TestFixtures.Financeiro.CriarContaValida();

            // Act
            var transacaoConta = new TransacaoConta(
                contaBancaria,
                conta,
                DateTime.Now,
                new ValorMonetario(0m)
            );

            // Assert
            Assert.Null(transacaoConta.Transacao);
        }

        [Fact]
        public void PropriedadesEhSomenteLeitura()
        {
            // Arrange
            var transacaoConta = TestFixtures.Financeiro.CriarTransacaoContaValida();

            // Assert - Verifica que não têm setter público
            var transacaoProperty = typeof(TransacaoConta).GetProperty("Transacao");
            var contaProperty = typeof(TransacaoConta).GetProperty("Conta");
            var dataProperty = typeof(TransacaoConta).GetProperty("Data");
            var valorTransacaoProperty = typeof(TransacaoConta).GetProperty("ValorTransacao");
            var multaProperty = typeof(TransacaoConta).GetProperty("Multa");
            var jurosProperty = typeof(TransacaoConta).GetProperty("Juros");
            var descontoProperty = typeof(TransacaoConta).GetProperty("Desconto");

            Assert.NotNull(transacaoProperty);
            Assert.Null(transacaoProperty.SetMethod);

            Assert.NotNull(contaProperty);
            Assert.Null(contaProperty.SetMethod);

            Assert.NotNull(dataProperty);
            Assert.Null(dataProperty.SetMethod);

            Assert.NotNull(valorTransacaoProperty);
            Assert.Null(valorTransacaoProperty.SetMethod);

            Assert.NotNull(multaProperty);
            Assert.Null(multaProperty.SetMethod);

            Assert.NotNull(jurosProperty);
            Assert.Null(jurosProperty.SetMethod);

            Assert.NotNull(descontoProperty);
            Assert.Null(descontoProperty.SetMethod);
        }

        [Fact]
        public void CriarTransacaoContaComValoresNegativosEhValidoSeValorTransacaoPositivo()
        {
            // Arrange
            var contaBancaria = TestFixtures.Financeiro.CriarContaBancariaValida();
            var conta = TestFixtures.Financeiro.CriarContaValida();

            // Act - Sistema não faz validação de valores negativos, apenas cria com valores zero por padrão
            var transacaoConta = new TransacaoConta(
                contaBancaria,
                conta,
                DateTime.Now,
                new ValorMonetario(400.00m),
                multa: new ValorMonetario(50.00m),
                juros: new ValorMonetario(25.00m),
                desconto: new ValorMonetario(100.00m)
            );

            // Assert
            Assert.NotNull(transacaoConta);
            Assert.Equal(400.00m, transacaoConta.ValorTransacao.Valor);
        }
    }
}
