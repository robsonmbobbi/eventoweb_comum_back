using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Entidades.Financeiro;
using EventoWeb.Comum.Negocio.ObjetosValor;
using EventoWeb.Comum.Testes.Negocio.Fixtures;
using static EventoWeb.Comum.Testes.Negocio.Fixtures.FinanceiroFixtures;
using static EventoWeb.Comum.Testes.Negocio.Fixtures.PessoasFixtures;

namespace EventoWeb.Comum.Testes.Negocio.Financeiro
{
    public class ContaTeste
    {
        [Fact]
        public void CriarContaComDadosValidos_DeveDefinirPropriedadesCorretas()
        {
            // Arrange
            var pessoa = CriarPessoaValida();
            var tipo = EnumTipoTransacao.Receita;
            var valor = new ValorMonetario(500.00m);
            var dataVencimento = DateTime.Now.AddDays(30);

            // Act
            var conta = new Conta(pessoa, tipo, valor, dataVencimento);

            // Assert
            Assert.NotNull(conta);
            Assert.Equal(pessoa, conta.Pessoa);
            Assert.Equal(tipo, conta.Tipo);
            Assert.Equal(500.00m, conta.Valor.Valor);
            Assert.False(conta.Liquidado);
            Assert.Equal(DateTime.Today, conta.DataCriado.Date);
            Assert.Equal(0m, conta.ValorTotalTransacoes.Valor);
            Assert.Equal(0m, conta.ValorTotalDesconto.Valor);
            Assert.Equal(0m, conta.ValorTotalJuros.Valor);
            Assert.Equal(0m, conta.ValorTotalMulta.Valor);
            Assert.Empty(conta.Transacoes);
        }

        [Fact]
        public void CriarContaComPessoaNula_DeveLancarExcecao()
        {
            // Arrange & Act & Assert
            Assert.Throws<Exception>(() => 
                new Conta(null, EnumTipoTransacao.Receita, new ValorMonetario(500.00m), DateTime.Now.AddDays(30))
            );
        }

        [Fact]
        public void CriarContaComValorNulo_DeveLancarExcecao()
        {
            // Arrange
            var pessoa = CriarPessoaValida();

            // Act & Assert
            Assert.Throws<Exception>(() => 
                new Conta(pessoa, EnumTipoTransacao.Receita, null, DateTime.Now.AddDays(30))
            );
        }

        [Fact]
        public void AdicionarTransacaoComValorMenorQueTotalConta_DeveAdicionarSemLiquidar()
        {
            // Arrange
            var conta = CriarContaValida(valor: 500.00m);
            var contaBancaria = CriarContaBancariaValida();
            var valorTransacao = new ValorMonetario(200.00m);

            // Act
            conta.AdicionarTransacao(contaBancaria, DateTime.Now, valorTransacao);

            // Assert
            Assert.Single(conta.Transacoes);
            Assert.Equal(200.00m, conta.ValorTotalTransacoes.Valor);
            Assert.False(conta.Liquidado);
        }

        [Fact]
        public void AdicionarTransacaoQueAtingeValorTotalDaConta_DeveLiquidar()
        {
            // Arrange
            var conta = CriarContaValida(valor: 500.00m);
            var contaBancaria = CriarContaBancariaValida();
            var valorTransacao = new ValorMonetario(500.00m);

            // Act
            conta.AdicionarTransacao(contaBancaria, DateTime.Now, valorTransacao);

            // Assert
            Assert.Single(conta.Transacoes);
            Assert.Equal(500.00m, conta.ValorTotalTransacoes.Valor);
            Assert.True(conta.Liquidado);
        }

        [Fact]
        public void AdicionarTransacaoSuperandoValorTotalDaConta_DeveLiquidar()
        {
            // Arrange
            var conta = CriarContaValida(valor: 500.00m);
            var contaBancaria = CriarContaBancariaValida();

            // Act
            conta.AdicionarTransacao(contaBancaria, DateTime.Now, new ValorMonetario(300.00m));
            conta.AdicionarTransacao(contaBancaria, DateTime.Now, new ValorMonetario(300.00m));

            // Assert
            Assert.Equal(2, conta.Transacoes.Count());
            Assert.Equal(600.00m, conta.ValorTotalTransacoes.Valor);
            Assert.True(conta.Liquidado);
        }

        [Fact]
        public void AdicionarTransacaoComDescontoJurosMulta_DeveCalcularTotais()
        {
            // Arrange
            var conta = CriarContaValida();
            var contaBancaria = CriarContaBancariaValida();

            // Act
            conta.AdicionarTransacao(
                contaBancaria,
                DateTime.Now,
                new ValorMonetario(200.00m),
                new ValorMonetario(5.00m),
                new ValorMonetario(10.00m),
                new ValorMonetario(50.00m)
            );

            // Assert
            Assert.Single(conta.Transacoes);
            Assert.Equal(200.00m, conta.ValorTotalTransacoes.Valor);
            Assert.Equal(50.00m, conta.ValorTotalDesconto.Valor);
            Assert.Equal(10.00m, conta.ValorTotalJuros.Valor);
            Assert.Equal(5.00m, conta.ValorTotalMulta.Valor);
        }

        [Fact]
        public void AdicionarTransacaoEmContaLiquidada_DeveLancarExcecao()
        {
            // Arrange
            var conta = CriarContaValida(valor: 100.00m);
            var contaBancaria = CriarContaBancariaValida();

            conta.AdicionarTransacao(contaBancaria, DateTime.Now, new ValorMonetario(100.00m));
            Assert.True(conta.Liquidado);

            // Act & Assert
            Assert.Throws<Exception>(() => 
                conta.AdicionarTransacao(contaBancaria, DateTime.Now, new ValorMonetario(50.00m))
            );
        }

        [Fact]
        public void RemoverTransacaoExistente_DeveRemoverERecalcularTotais()
        {
            // Arrange
            var conta = CriarContaValida();
            var contaBancaria = CriarContaBancariaValida();

            conta.AdicionarTransacao(contaBancaria, DateTime.Now, new ValorMonetario(150.00m));
            conta.AdicionarTransacao(contaBancaria, DateTime.Now, new ValorMonetario(300.00m));

            var transacao1 = conta.Transacoes.First();

            // Act
            conta.RemoverTransacao(transacao1);

            // Assert
            Assert.Single(conta.Transacoes);
            Assert.Equal(300.00m, conta.ValorTotalTransacoes.Valor);
        }

        [Fact]
        public void RemoverTransacaoInexistente_DeveLancarExcecao()
        {
            // Arrange
            var conta = CriarContaValida();
            var contaBancaria = CriarContaBancariaValida();
            conta.AdicionarTransacao(contaBancaria, DateTime.Now, new ValorMonetario(200.00m));

            var outraConta = CriarContaValida();
            var transacaoOutra = new TransacaoConta(
                contaBancaria,
                outraConta,
                DateTime.Now,
                new ValorMonetario(100.00m)
            );

            // Act & Assert
            Assert.Throws<Exception>(() => 
                conta.RemoverTransacao(transacaoOutra)
            );
        }

        [Fact]
        public void RemoverTransacaoEmContaLiquidada_DeveLancarExcecao()
        {
            // Arrange
            var conta = CriarContaValida(valor: 100.00m);
            var contaBancaria = CriarContaBancariaValida();

            conta.AdicionarTransacao(contaBancaria, DateTime.Now, new ValorMonetario(100.00m));
            var transacao = conta.Transacoes.First();

            // Act & Assert
            Assert.Throws<Exception>(() => 
                conta.RemoverTransacao(transacao)
            );
        }

        [Fact]
        public void RemoverTransacaoRecalculaTotaisCorretamente()
        {
            // Arrange
            var conta = CriarContaValida();
            var contaBancaria = CriarContaBancariaValida();

            conta.AdicionarTransacao(contaBancaria, DateTime.Now, new ValorMonetario(150.00m), desconto: new ValorMonetario(50.00m));
            conta.AdicionarTransacao(contaBancaria, DateTime.Now, new ValorMonetario(300.00m), desconto: new ValorMonetario(100.00m));

            var primeiraTransacao = conta.Transacoes.First();

            // Act
            conta.RemoverTransacao(primeiraTransacao);

            // Assert
            Assert.Single(conta.Transacoes);
            Assert.Equal(300.00m, conta.ValorTotalTransacoes.Valor);
            Assert.Equal(100.00m, conta.ValorTotalDesconto.Valor);
        }

        [Fact]
        public void ReiniciarContaLiquidada_DeveReabrirELimparTransacoes()
        {
            // Arrange
            var conta = CriarContaValida(valor: 100.00m);
            var contaBancaria = CriarContaBancariaValida();

            conta.AdicionarTransacao(contaBancaria, DateTime.Now, new ValorMonetario(100.00m));
            Assert.True(conta.Liquidado);

            // Act
            conta.Reabrir();

            // Assert
            Assert.False(conta.Liquidado);
            Assert.Empty(conta.Transacoes);
            Assert.Equal(0m, conta.ValorTotalTransacoes.Valor);
            Assert.Equal(0m, conta.ValorTotalDesconto.Valor);
            Assert.Equal(0m, conta.ValorTotalJuros.Valor);
            Assert.Equal(0m, conta.ValorTotalMulta.Valor);
        }

        [Fact]
        public void ReiniciarContaNaoLiquidada_DeveLancarExcecao()
        {
            // Arrange
            var conta = CriarContaValida();
            Assert.False(conta.Liquidado);

            // Act & Assert
            Assert.Throws<Exception>(() => 
                conta.Reabrir()
            );
        }

        [Fact]
        public void AlterarValorEmContaNaoLiquidada_DeveAlterarPropriedade()
        {
            // Arrange
            var conta = CriarContaValida();

            // Act
            conta.Valor = new ValorMonetario(1000.00m);

            // Assert
            Assert.Equal(1000.00m, conta.Valor.Valor);
        }

        [Fact]
        public void AlterarValorEmContaLiquidada_DeveLancarExcecao()
        {
            // Arrange
            var conta = CriarContaValida(valor: 100.00m);
            var contaBancaria = CriarContaBancariaValida();

            conta.AdicionarTransacao(contaBancaria, DateTime.Now, new ValorMonetario(100.00m));
            Assert.True(conta.Liquidado);

            // Act & Assert
            Assert.Throws<Exception>(() => 
                conta.Valor = new ValorMonetario(2000.00m)
            );
        }

        [Fact]
        public void AlterarDescricao_DeveAlterarPropriedade()
        {
            // Arrange
            var conta = CriarContaValida();
            var novaDescricao = new StringClob("Nova descrição");

            // Act
            conta.Descricao = novaDescricao;

            // Assert
            Assert.NotNull(conta.Descricao);
        }

        [Fact]
        public void AlterarDataVencimento_DeveAlterarPropriedade()
        {
            // Arrange
            var conta = CriarContaValida();
            var novaData = DateTime.Now.AddDays(60);

            // Act
            conta.DataVencimento = novaData;

            // Assert
            Assert.Equal(novaData, conta.DataVencimento);
        }

        [Fact]
        public void AlterarTipo_DeveAlterarPropriedade()
        {
            // Arrange
            var conta = CriarContaValida(tipo: EnumTipoTransacao.Receita);

            // Act
            conta.Tipo = EnumTipoTransacao.Despesa;

            // Assert
            Assert.Equal(EnumTipoTransacao.Despesa, conta.Tipo);
        }
    }
}


