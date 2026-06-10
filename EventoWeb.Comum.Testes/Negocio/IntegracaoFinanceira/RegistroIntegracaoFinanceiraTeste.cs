using EventoWeb.Comum.Negocio.Entidades.Financeiro;
using EventoWeb.Comum.Negocio.Entidades.IntegracaoFinanceira;
using EventoWeb.Comum.Negocio.ObjetosValor;
using EventoWeb.Comum.Testes.Negocio.Fixtures;
using static EventoWeb.Comum.Testes.Negocio.Fixtures.FinanceiroFixtures;

namespace EventoWeb.Comum.Testes.Negocio.IntegracaoFinanceira
{
    public class RegistroIntegracaoFinanceiraTeste
    {
        [Fact]
        public void CriarComDadosValidos_DeveDefinirPropriedadesCorretamente()
        {
            // Arrange
            var integrador = CriarIntegradorFinanceiroValido();
            var conta = CriarContaValida();
            var valor = new ValorMonetario(150.00m);
            var tipo = EnumTipoPagamento.CartaoCredito;
            var identificacao = new String1000("INTEG_001");

            // Act
            var registro = new RegistroIntegracaoFinanceira(integrador, conta, valor, tipo, identificacao);

            // Assert
            Assert.NotNull(registro);
            Assert.Equal(integrador, registro.Integrador);
            Assert.Equal(conta, registro.Conta);
            Assert.Equal(150.00m, registro.Valor.Valor);
            Assert.Equal(EnumTipoPagamento.CartaoCredito, registro.Tipo);
            Assert.Equal("INTEG_001", registro.IdentificacaoNoIntegrador.Valor);
        }

        [Fact]
        public void CriarComIntegradorNulo_DeveLancarExcecao()
        {
            // Arrange
            var conta = CriarContaValida();
            var valor = new ValorMonetario(150.00m);
            var identificacao = new String1000("INTEG_001");

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new RegistroIntegracaoFinanceira(null, conta, valor, EnumTipoPagamento.CartaoCredito, identificacao)
            );
        }

        [Fact]
        public void CriarComContaNula_DeveLancarExcecao()
        {
            // Arrange
            var integrador = CriarIntegradorFinanceiroValido();
            var valor = new ValorMonetario(150.00m);
            var identificacao = new String1000("INTEG_001");

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new RegistroIntegracaoFinanceira(integrador, null, valor, EnumTipoPagamento.CartaoCredito, identificacao)
            );
        }

        [Fact]
        public void CriarComValorNulo_DeveLancarExcecao()
        {
            // Arrange
            var integrador = CriarIntegradorFinanceiroValido();
            var conta = CriarContaValida();
            var identificacao = new String1000("INTEG_001");

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new RegistroIntegracaoFinanceira(integrador, conta, null, EnumTipoPagamento.CartaoCredito, identificacao)
            );
        }

        [Fact]
        public void CriarComIdentificacaoNula_DeveLancarExcecao()
        {
            // Arrange
            var integrador = CriarIntegradorFinanceiroValido();
            var conta = CriarContaValida();
            var valor = new ValorMonetario(150.00m);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new RegistroIntegracaoFinanceira(integrador, conta, valor, EnumTipoPagamento.CartaoCredito, null)
            );
        }

        [Fact]
        public void SituacaoInicialDeveSerpPendente()
        {
            // Act
            var registro = CriarRegistroIntegracaoFinanceiraValida();

            // Assert
            Assert.Equal(EnumSituacaoIntegracao.Pendente, registro.Situacao);
        }

        [Fact]
        public void ConcluirComTransacaoValida_DeveDefinirSituacaoComoConcluido()
        {
            // Arrange
            var registro = CriarRegistroIntegracaoFinanceiraValida();
            var transacao = CriarTransacaoValida();

            // Act
            registro.Concluir(transacao);

            // Assert
            Assert.Equal(EnumSituacaoIntegracao.Concluido, registro.Situacao);
            Assert.Equal(transacao, registro.Transacao);
            Assert.NotNull(registro.DataConcluidoAbortado);
        }

        [Fact]
        public void NotificarErro_DeveDefinirSituacaoComoErro()
        {
            // Arrange
            var registro = CriarRegistroIntegracaoFinanceiraValida();
            var descricaoErro = "Erro de conexão com a API";

            // Act
            registro.NotificarErro(descricaoErro);

            // Assert
            Assert.Equal(EnumSituacaoIntegracao.Erro, registro.Situacao);
            var logs = registro.Logs.ToList();
            Assert.NotEmpty(logs);
            Assert.Equal(EnumTipoLog.Erro, logs.Last().Tipo);
            Assert.Equal(descricaoErro, logs.Last().Mensagem.Valor);
        }

        [Fact]
        public void AbortarIntegracao_DeveDefinirSituacaoComoAbortado()
        {
            // Arrange
            var registro = CriarRegistroIntegracaoFinanceiraValida();
            var motivo = "Usuário cancelou a operação";

            // Act
            registro.Abortar(motivo);

            // Assert
            Assert.Equal(EnumSituacaoIntegracao.Abortado, registro.Situacao);
            Assert.NotNull(registro.DataConcluidoAbortado);
            var logs = registro.Logs.ToList();
            Assert.NotEmpty(logs);
        }

        [Fact]
        public void AdicionarLog_DeveAdicionarLogAColecao()
        {
            // Arrange
            var registro = CriarRegistroIntegracaoFinanceiraValida();
            var mensagem = "Log de teste";

            // Act
            registro.AdicionarLog(EnumTipoLog.Info, mensagem);

            // Assert
            var logs = registro.Logs.ToList();
            Assert.NotEmpty(logs);
            Assert.Contains(logs, l => l.Mensagem.Valor == mensagem);
        }

        [Fact]
        public void ConcluirIntegracaoJaConcluida_DeveLancarExcecao()
        {
            // Arrange
            var registro = CriarRegistroIntegracaoFinanceiraValida();
            var transacao = CriarTransacaoValida();
            registro.Concluir(transacao);

            // Act & Assert
            Assert.Throws<Exception>(() =>
                registro.Concluir(CriarTransacaoValida())
            );
        }

        [Fact]
        public void AbortarIntegracaoJaConcluida_DeveLancarExcecao()
        {
            // Arrange
            var registro = CriarRegistroIntegracaoFinanceiraValida();
            var transacao = CriarTransacaoValida();
            registro.Concluir(transacao);

            // Act & Assert
            Assert.Throws<Exception>(() =>
                registro.Abortar("Motivo")
            );
        }
    }
}
