using EventoWeb.Comum.Negocio.Entidades.Financeiro;
using EventoWeb.Comum.Negocio.ObjetosValor;
using EventoWeb.Comum.Testes.Negocio.Fixtures;
using static EventoWeb.Comum.Testes.Negocio.Fixtures.PrecosFixtures;

namespace EventoWeb.Comum.Testes.Negocio.Financeiro
{
    public class FormaPagamentoTeste
    {
        [Fact]
        public void CriarFormaPagamentoComDadosValidos_DeveDefinirPropriedadesCorretas()
        {
            // Arrange
            var nome = new String200("Cartão de Crédito");
            var tipo = EnumTipoPagamento.CartaoCredito;

            // Act
            var formaPagamento = new FormaPagamento(nome, tipo);

            // Assert
            Assert.NotNull(formaPagamento);
            Assert.Equal("Cartão de Crédito", formaPagamento.Nome.Valor);
            Assert.Equal(EnumTipoPagamento.CartaoCredito, formaPagamento.Tipo);
            Assert.NotNull(formaPagamento.Parcelas);
            Assert.Equal(1, formaPagamento.Parcelas.Minimo);
            Assert.Equal(1, formaPagamento.Parcelas.Maximo);
        }

        [Fact]
        public void CriarFormaPagamentoComNomeNulo_DeveLancarExcecao()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                new FormaPagamento(null, EnumTipoPagamento.CartaoCredito)
            );
        }

        [Fact]
        public void AlterarNomeValido_DeveAlterarPropriedade()
        {
            // Arrange
            var formaPagamento = CriarFormaPagamentoValida();
            var novoNome = new String200("PIX");

            // Act
            formaPagamento.Nome = novoNome;

            // Assert
            Assert.Equal("PIX", formaPagamento.Nome.Valor);
        }

        [Fact]
        public void AtribuirNomeNulo_DeveLancarExcecao()
        {
            // Arrange
            var formaPagamento = CriarFormaPagamentoValida();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                formaPagamento.Nome = null
            );
        }

        [Fact]
        public void AlterarTipoPagamento_DeveAlterarPropriedade()
        {
            // Arrange
            var formaPagamento = CriarFormaPagamentoValida(tipoPagamento: EnumTipoPagamento.CartaoCredito);

            // Act
            formaPagamento.Tipo = EnumTipoPagamento.PIX;

            // Assert
            Assert.Equal(EnumTipoPagamento.PIX, formaPagamento.Tipo);
        }

        [Fact]
        public void DefinirParcelasValidas_DeveAlterarPropriedade()
        {
            // Arrange
            var formaPagamento = CriarFormaPagamentoValida();
            var novasParcelas = new IntervaloInteiroPositivo(1, 12);

            // Act
            formaPagamento.DefinirParcelas(novasParcelas);

            // Assert
            Assert.NotNull(formaPagamento.Parcelas);
            Assert.Equal(1, formaPagamento.Parcelas.Minimo);
            Assert.Equal(12, formaPagamento.Parcelas.Maximo);
        }

        [Fact]
        public void DefinirParcelasNula_DeveLancarExcecao()
        {
            // Arrange
            var formaPagamento = CriarFormaPagamentoValida();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                formaPagamento.DefinirParcelas(null)
            );
        }

        [Fact]
        public void CriarFormaPagamentoComTipoCaraodCredito_DeveDefinirCorreatamente()
        {
            // Arrange & Act
            var formaPagamento = CriarFormaPagamentoValida(tipoPagamento: EnumTipoPagamento.CartaoCredito);

            // Assert
            Assert.Equal(EnumTipoPagamento.CartaoCredito, formaPagamento.Tipo);
        }

        [Fact]
        public void CriarFormaPagamentoComTipoPIX_DeveDefinirCorretamente()
        {
            // Arrange & Act
            var formaPagamento = CriarFormaPagamentoValida(tipoPagamento: EnumTipoPagamento.PIX);

            // Assert
            Assert.Equal(EnumTipoPagamento.PIX, formaPagamento.Tipo);
        }
    }
}


