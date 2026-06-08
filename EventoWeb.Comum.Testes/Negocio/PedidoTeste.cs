using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Entidades.Financeiro;
using EventoWeb.Comum.Negocio.ObjetosValor;

namespace EventoWeb.Comum.Testes.Negocio
{
    public class PedidoTeste
    {
        [Fact]
        public void CriarPedidoDeDebitoComDadosValidos_DeveDefinirPropriedadesCorretas()
        {
            // Arrange
            var pagador = TestFixtures.Pessoas.CriarPessoaValida();
            var evento = TestFixtures.Eventos.CriarEventoValido();
            var pessoaInscricao = TestFixtures.Pessoas.CriarAdulto();
            var inscricao = new InscricaoParticipante(evento, pessoaInscricao, DateTime.Now);
            var forma = TestFixtures.Precos.CriarFormaPagamentoValida();

            // Act
            var pedido = new Pedido(
                pagador,
                new[] { inscricao },
                new ValorMonetario(100.00m),
                EnumTipoPedido.Debito,
                forma,
                null
            );

            // Assert
            Assert.NotNull(pedido);
            Assert.Equal(pagador, pedido.Pagador);
            Assert.Equal(EnumTipoPedido.Debito, pedido.Tipo);
            Assert.Equal(forma, pedido.FormaPagamento);
            Assert.Equal(100.00m, pedido.Valor.Valor);
            Assert.Single(pedido.Inscricoes);
            Assert.NotNull(pedido.Conta);
            Assert.Null(pedido.Motivo);
        }

        [Fact]
        public void CriarPedidoDeDescontoComDadosValidos_DeveDefinirPropriedadesCorretas()
        {
            // Arrange
            var pagador = TestFixtures.Pessoas.CriarPessoaValida();
            var evento = TestFixtures.Eventos.CriarEventoValido();
            var pessoaInscricao = TestFixtures.Pessoas.CriarAdulto();
            var inscricao = new InscricaoParticipante(evento, pessoaInscricao, DateTime.Now);

            // Act
            var pedido = new Pedido(
                pagador,
                new[] { inscricao },
                new ValorMonetario(50.00m),
                EnumTipoPedido.Desconto,
                null,
                null
            );

            // Assert
            Assert.NotNull(pedido);
            Assert.Equal(EnumTipoPedido.Desconto, pedido.Tipo);
            Assert.Null(pedido.FormaPagamento);
        }

        [Fact]
        public void CriarPedidoDeIsencaoComDadosValidos_DeveDefinirPropriedadesCorretas()
        {
            // Arrange
            var pagador = TestFixtures.Pessoas.CriarPessoaValida();
            var evento = TestFixtures.Eventos.CriarEventoValido();
            var pessoaInscricao = TestFixtures.Pessoas.CriarAdulto();
            var inscricao = new InscricaoParticipante(evento, pessoaInscricao, DateTime.Now);

            // Act
            var pedido = new Pedido(
                pagador,
                new[] { inscricao },
                new ValorMonetario(0.00m),
                EnumTipoPedido.Isencao,
                null,
                null
            );

            // Assert
            Assert.NotNull(pedido);
            Assert.Equal(EnumTipoPedido.Isencao, pedido.Tipo);
            Assert.Equal(0.00m, pedido.Valor.Valor);
        }

        [Fact]
        public void CriarPedidoComInscricoesVazia_DeveLancarExcecao()
        {
            // Arrange
            var pagador = TestFixtures.Pessoas.CriarPessoaValida();
            var forma = TestFixtures.Precos.CriarFormaPagamentoValida();

            // Act & Assert
            Assert.Throws<Exception>(() => 
                new Pedido(
                    pagador,
                    new InscricaoParticipante[] { },
                    new ValorMonetario(100.00m),
                    EnumTipoPedido.Debito,
                    forma,
                    null
                )
            );
        }

        [Fact]
        public void CriarPedidoComInscricaoEmEstadoNaoLimbo_DeveLancarExcecao()
        {
            // Arrange
            var pagador = TestFixtures.Pessoas.CriarPessoaValida();
            var inscricao = TestFixtures.Inscricoes.CriarInscricaoParticipanteEmPendente();
            var forma = TestFixtures.Precos.CriarFormaPagamentoValida();

            // Act & Assert
            Assert.Throws<Exception>(() => 
                new Pedido(
                    pagador,
                    new[] { inscricao },
                    new ValorMonetario(100.00m),
                    EnumTipoPedido.Debito,
                    forma,
                    null
                )
            );
        }

        [Fact]
        public void CriarPedidoDeDebitoSemFormaPagamento_DeveLancarExcecao()
        {
            // Arrange
            var pagador = TestFixtures.Pessoas.CriarPessoaValida();
            var evento = TestFixtures.Eventos.CriarEventoValido();
            var pessoaInscricao = TestFixtures.Pessoas.CriarAdulto();
            var inscricao = new InscricaoParticipante(evento, pessoaInscricao, DateTime.Now);

            // Act & Assert
            Assert.Throws<Exception>(() => 
                new Pedido(
                    pagador,
                    new[] { inscricao },
                    new ValorMonetario(100.00m),
                    EnumTipoPedido.Debito,
                    null,
                    null
                )
            );
        }

        [Fact]
        public void CriarPedidoComPagadorNulo_DeveLancarExcecao()
        {
            // Arrange
            var evento = TestFixtures.Eventos.CriarEventoValido();
            var pessoaInscricao = TestFixtures.Pessoas.CriarAdulto();
            var inscricao = new InscricaoParticipante(evento, pessoaInscricao, DateTime.Now);
            var forma = TestFixtures.Precos.CriarFormaPagamentoValida();

            // Act & Assert
            Assert.Throws<Exception>(() => 
                new Pedido(
                    null,
                    new[] { inscricao },
                    new ValorMonetario(100.00m),
                    EnumTipoPedido.Debito,
                    forma,
                    null
                )
            );
        }

        [Fact]
        public void CriarPedidoComValorNulo_DeveLancarExcecao()
        {
            // Arrange
            var pagador = TestFixtures.Pessoas.CriarPessoaValida();
            var evento = TestFixtures.Eventos.CriarEventoValido();
            var pessoaInscricao = TestFixtures.Pessoas.CriarAdulto();
            var inscricao = new InscricaoParticipante(evento, pessoaInscricao, DateTime.Now);
            var forma = TestFixtures.Precos.CriarFormaPagamentoValida();

            // Act & Assert
            Assert.Throws<Exception>(() => 
                new Pedido(
                    pagador,
                    new[] { inscricao },
                    null,
                    EnumTipoPedido.Debito,
                    forma,
                    null
                )
            );
        }

        [Fact]
        public void CriarPedidoComMultiplasInscricoes_DeveContermTodas()
        {
            // Arrange
            var pagador = TestFixtures.Pessoas.CriarPessoaValida();
            var evento = TestFixtures.Eventos.CriarEventoValido();
            var inscricao1 = new InscricaoParticipante(evento, TestFixtures.Pessoas.CriarAdulto(), DateTime.Now);
            var inscricao2 = new InscricaoParticipante(evento, TestFixtures.Pessoas.CriarAdulto(), DateTime.Now);
            var inscricao3 = new InscricaoParticipante(evento, TestFixtures.Pessoas.CriarAdulto(), DateTime.Now);
            var forma = TestFixtures.Precos.CriarFormaPagamentoValida();

            // Act
            var pedido = new Pedido(
                pagador,
                new[] { inscricao1, inscricao2, inscricao3 },
                new ValorMonetario(300.00m),
                EnumTipoPedido.Debito,
                forma,
                null
            );

            // Assert
            Assert.Equal(3, pedido.Inscricoes.Count());
        }

        [Fact]
        public void PedidoAlocaConta()
        {
            // Arrange
            var pagador = TestFixtures.Pessoas.CriarPessoaValida();
            var evento = TestFixtures.Eventos.CriarEventoValido();
            var pessoaInscricao = TestFixtures.Pessoas.CriarAdulto();
            var inscricao = new InscricaoParticipante(evento, pessoaInscricao, DateTime.Now);
            var valor = new ValorMonetario(500.00m);
            var forma = TestFixtures.Precos.CriarFormaPagamentoValida();

            // Act
            var pedido = new Pedido(
                pagador,
                new[] { inscricao },
                valor,
                EnumTipoPedido.Debito,
                forma,
                null
            );

            // Assert
            Assert.NotNull(pedido.Conta);
            Assert.Equal(pagador, pedido.Conta.Pessoa);
            Assert.Equal(EnumTipoTransacao.Receita, pedido.Conta.Tipo);
            Assert.Equal(valor.Valor, pedido.Conta.Valor.Valor);
        }
    }
}
