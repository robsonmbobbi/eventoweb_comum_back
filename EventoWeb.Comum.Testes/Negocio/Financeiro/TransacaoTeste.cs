using EventoWeb.Comum.Negocio.Entidades.Financeiro;
using EventoWeb.Comum.Negocio.ObjetosValor;
using EventoWeb.Comum.Testes.Negocio.Fixtures;
using static EventoWeb.Comum.Testes.Negocio.Fixtures.FinanceiroFixtures;

namespace EventoWeb.Comum.Testes.Negocio.Financeiro
{
    public class TransacaoTeste
    {
        [Fact]
        public void CriarTransacaoComDadosValidos_DeveDefinirPropriedadesCorretas()
        {
            // Arrange
            var tipo = EnumTipoTransacao.Receita;
            var contaBancaria = CriarContaBancariaValida();
            var dataHora = DateTime.Now;
            var valor = new ValorMonetario(300.00m);
            var descricao = new String200("Pagamento recebido");

            // Act
            var transacao = new Transacao(tipo, contaBancaria, dataHora, valor, descricao);

            // Assert
            Assert.NotNull(transacao);
            Assert.Equal(EnumTipoTransacao.Receita, transacao.Tipo);
            Assert.Equal(contaBancaria, transacao.ContaBancaria);
            Assert.Equal(dataHora, transacao.DataHora);
            Assert.Equal(300.00m, transacao.Valor.Valor);
            Assert.NotNull(transacao.Descricao);
        }

        [Fact]
        public void CriarTransacaoComContaBancariaNula_DeveLancarExcecao()
        {
            // Arrange & Act & Assert
            Assert.Throws<Exception>(() => 
                new Transacao(
                    EnumTipoTransacao.Receita,
                    null,
                    DateTime.Now,
                    new ValorMonetario(300.00m),
                    new String200("Descrição")
                )
            );
        }

        [Fact]
        public void CriarTransacaoComValorNulo_DeveLancarExcecao()
        {
            // Arrange
            var contaBancaria = CriarContaBancariaValida();

            // Act & Assert
            Assert.Throws<Exception>(() => 
                new Transacao(
                    EnumTipoTransacao.Receita,
                    contaBancaria,
                    DateTime.Now,
                    null,
                    new String200("Descrição")
                )
            );
        }
    }
}

