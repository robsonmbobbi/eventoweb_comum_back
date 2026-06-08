using EventoWeb.Comum.Negocio.Entidades.Financeiro;
using EventoWeb.Comum.Negocio.ObjetosValor;

namespace EventoWeb.Comum.Testes.Negocio
{
    public class TransacaoTeste
    {
        [Fact]
        public void CriarTransacaoComDadosValidos_DeveDefinirPropriedadesCorretas()
        {
            // Arrange
            var tipo = EnumTipoTransacao.Receita;
            var contaBancaria = TestFixtures.Financeiro.CriarContaBancariaValida();
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
            var contaBancaria = TestFixtures.Financeiro.CriarContaBancariaValida();

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

        [Fact]
        public void PropriedadesEhSomenteLeitura()
        {
            // Arrange
            var transacao = TestFixtures.Financeiro.CriarTransacaoValida();

            // Assert - Verifica que não têm setter público
            var contaBancariaProperty = typeof(Transacao).GetProperty("ContaBancaria");
            var dataHoraProperty = typeof(Transacao).GetProperty("DataHora");
            var descricaoProperty = typeof(Transacao).GetProperty("Descricao");
            var valorProperty = typeof(Transacao).GetProperty("Valor");
            var tipoProperty = typeof(Transacao).GetProperty("Tipo");

            Assert.NotNull(contaBancariaProperty);
            Assert.Null(contaBancariaProperty.SetMethod);

            Assert.NotNull(dataHoraProperty);
            Assert.Null(dataHoraProperty.SetMethod);

            Assert.NotNull(descricaoProperty);
            Assert.Null(descricaoProperty.SetMethod);

            Assert.NotNull(valorProperty);
            Assert.Null(valorProperty.SetMethod);

            Assert.NotNull(tipoProperty);
            Assert.Null(tipoProperty.SetMethod);
        }
    }
}
