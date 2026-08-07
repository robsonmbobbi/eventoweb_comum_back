using EventoWeb.Comum.Negocio.Entidades.Notificacoes;
using EventoWeb.Comum.Negocio.ObjetosValor;
using EventoWeb.Comum.Testes.Negocio.Fixtures;
using static EventoWeb.Comum.Testes.Negocio.Fixtures.NotificacoesFixtures;

namespace EventoWeb.Comum.Testes.Negocio.Notificacoes
{
    public class MensagemNotificacaoTeste
    {
        [Fact]
        public void CriarMensagemComDadosValidos_DeveDefinirPropriedadesCorretas()
        {
            // Arrange
            var modelo = CriarModeloMensagemValido();
            var destinatario = new String500("contato@exemplo.com");
            var variaveisJson = new StringClob("{\"nome\": \"João\"}");

            // Act
            var mensagem = new MensagemNotificacao(modelo, destinatario, variaveisJson);

            // Assert
            Assert.NotNull(mensagem);
            Assert.Equal(modelo, mensagem.Modelo);
            Assert.Equal(destinatario, mensagem.Destinatario);
            Assert.Equal(variaveisJson, mensagem.VariaveisJson);
            Assert.Equal(EnumSituacaoEnvioNotificacao.EmFila, mensagem.Situacao);
            Assert.Null(mensagem.DataSituacao);
            Assert.Null(mensagem.Erro);
        }

        [Fact]
        public void CriarMensagemComModeloNulo_DeveLancarExcecao()
        {
            // Arrange
            var destinatario = new String500("contato@exemplo.com");
            var variaveisJson = new StringClob("{\"nome\": \"João\"}");

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new MensagemNotificacao(null, destinatario, variaveisJson)
            );
        }

        [Fact]
        public void CriarMensagemComDestinatarioNulo_DeveLancarExcecao()
        {
            // Arrange
            var modelo = CriarModeloMensagemValido();
            var variaveisJson = new StringClob("{\"nome\": \"João\"}");

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new MensagemNotificacao(modelo, null, variaveisJson)
            );
        }

        [Fact]
        public void CriarMensagem_SituacaoInicialeDeveSarPendente()
        {
            // Act
            var mensagem = CriarMensagemNotificacaoValida();

            // Assert
            Assert.Equal(EnumSituacaoEnvioNotificacao.EmFila, mensagem.Situacao);
        }

        [Fact]
        public void CriarMensagem_DataSituacaoNaoDeveSerDefinidaNoInicio()
        {
            // Act
            var mensagem = CriarMensagemNotificacaoValida();

            // Assert
            Assert.Null(mensagem.DataSituacao);
        }

        [Fact]
        public void CriarMensagemComVariaveisJson_DeveArmazenarVariaveisJson()
        {
            // Arrange
            var variaveisJson = "{\"nome\": \"Maria\", \"id\": 123}";

            // Act
            var mensagem = CriarMensagemNotificacaoComVariaveis(variaveisJson);

            // Assert
            Assert.NotNull(mensagem.VariaveisJson);
            Assert.Equal(variaveisJson, mensagem.VariaveisJson.Valor);
        }

        [Fact]
        public void CriarMensagemSemVariaveisJson_DeveArmazenarNulo()
        {
            // Act
            var mensagem = CriarMensagemNotificacaoSemVariaveis();

            // Assert
            Assert.Null(mensagem.VariaveisJson);
        }

        [Fact]
        public void RegistrarEnvio_DeveAtualizarSituacaoParaEnviada()
        {
            // Arrange
            var mensagem = CriarMensagemNotificacaoValida();
            Assert.Equal(EnumSituacaoEnvioNotificacao.EmFila, mensagem.Situacao);

            // Act
            mensagem.RegistrarEnvio();

            // Assert
            Assert.Equal(EnumSituacaoEnvioNotificacao.Enviado, mensagem.Situacao);
        }

        [Fact]
        public void RegistrarEnvio_DeveDefinirDataSituacao()
        {
            // Arrange
            var mensagem = CriarMensagemNotificacaoValida();
            var dataAntes = DateTime.Now;

            // Act
            mensagem.RegistrarEnvio();

            // Assert
            Assert.NotNull(mensagem.DataSituacao);
            Assert.True(mensagem.DataSituacao >= dataAntes);
            Assert.True(mensagem.DataSituacao <= DateTime.Now);
        }

        [Fact]
        public void RegistrarEnvio_DataSituacaoDeveSerAgoraOuAnte()
        {
            // Arrange
            var mensagem = CriarMensagemNotificacaoValida();

            // Act
            var dataAntes = DateTime.Now;
            mensagem.RegistrarEnvio();
            var dataDepois = DateTime.Now;

            // Assert
            Assert.NotNull(mensagem.DataSituacao);
            Assert.True(mensagem.DataSituacao >= dataAntes);
            Assert.True(mensagem.DataSituacao <= dataDepois);
        }

        [Fact]
        public void RegistrarErroEnvio_DeveAtualizarSituacaoParaEnvioErro()
        {
            // Arrange
            var mensagem = CriarMensagemNotificacaoValida();
            var erro = new StringClob("Erro ao enviar a notificação");

            // Act
            mensagem.RegistrarErroEnvio(erro);

            // Assert
            Assert.Equal(EnumSituacaoEnvioNotificacao.EnvioErro, mensagem.Situacao);
        }

        [Fact]
        public void RegistrarErroEnvio_DeveDefinirMensagemErro()
        {
            // Arrange
            var mensagem = CriarMensagemNotificacaoValida();
            var erro = new StringClob("Erro de conexão com o servidor SMTP");

            // Act
            mensagem.RegistrarErroEnvio(erro);

            // Assert
            Assert.Equal(erro, mensagem.Erro);
        }

        [Fact]
        public void RegistrarErroEnvio_DeveDefinirDataSituacao()
        {
            // Arrange
            var mensagem = CriarMensagemNotificacaoValida();
            var erro = new StringClob("Erro ao processar");
            var dataAntes = DateTime.Now;

            // Act
            mensagem.RegistrarErroEnvio(erro);

            // Assert
            Assert.NotNull(mensagem.DataSituacao);
            Assert.True(mensagem.DataSituacao >= dataAntes);
            Assert.True(mensagem.DataSituacao <= DateTime.Now);
        }

        [Fact]
        public void RegistrarErroEnvio_DevePreservarModeloEDestinatario()
        {
            // Arrange
            var mensagem = CriarMensagemNotificacaoValida();
            var modeloOriginal = mensagem.Modelo;
            var destinatarioOriginal = mensagem.Destinatario;
            var erro = new StringClob("Erro ao enviar");

            // Act
            mensagem.RegistrarErroEnvio(erro);

            // Assert
            Assert.Equal(modeloOriginal, mensagem.Modelo);
            Assert.Equal(destinatarioOriginal, mensagem.Destinatario);
        }

        [Fact]
        public void RegistrarEnvioAposErroEnvio_DeveAtualizarSituacaoParaEnviado()
        {
            // Arrange
            var mensagem = CriarMensagemNotificacaoValida();
            var erro = new StringClob("Erro inicial");
            mensagem.RegistrarErroEnvio(erro);
            Assert.Equal(EnumSituacaoEnvioNotificacao.EnvioErro, mensagem.Situacao);

            // Act
            mensagem.RegistrarEnvio();

            // Assert
            Assert.Equal(EnumSituacaoEnvioNotificacao.Enviado, mensagem.Situacao);
        }

        [Fact]
        public void RegistrarErroFila_DeveAtualizarSituacaoParaFilaErro()
        {
            // Arrange
            var mensagem = CriarMensagemNotificacaoValida();
            var erro = new StringClob("Erro ao conectar na fila");

            // Act
            mensagem.RegistrarErroFila(erro);

            // Assert
            Assert.Equal(EnumSituacaoEnvioNotificacao.FilaErro, mensagem.Situacao);
        }

        [Fact]
        public void RegistrarErroFila_DeveDefinirMensagemErro()
        {
            // Arrange
            var mensagem = CriarMensagemNotificacaoValida();
            var erro = new StringClob("Erro de conexão com o RabbitMQ");

            // Act
            mensagem.RegistrarErroFila(erro);

            // Assert
            Assert.Equal(erro, mensagem.Erro);
        }

        [Fact]
        public void RegistrarErroFila_DeveDefinirDataSituacao()
        {
            // Arrange
            var mensagem = CriarMensagemNotificacaoValida();
            var erro = new StringClob("Erro ao processar");
            var dataAntes = DateTime.Now;

            // Act
            mensagem.RegistrarErroFila(erro);

            // Assert
            Assert.NotNull(mensagem.DataSituacao);
            Assert.True(mensagem.DataSituacao >= dataAntes);
            Assert.True(mensagem.DataSituacao <= DateTime.Now);
        }

        [Fact]
        public void RegistrarErroFila_DevePreservarModeloEDestinatario()
        {
            // Arrange
            var mensagem = CriarMensagemNotificacaoValida();
            var modeloOriginal = mensagem.Modelo;
            var destinatarioOriginal = mensagem.Destinatario;
            var erro = new StringClob("Erro ao enfileirar");

            // Act
            mensagem.RegistrarErroFila(erro);

            // Assert
            Assert.Equal(modeloOriginal, mensagem.Modelo);
            Assert.Equal(destinatarioOriginal, mensagem.Destinatario);
        }

        [Fact]
        public void CriarMensagem_ErroNaoDeveSerDefinidoNoInicio()
        {
            // Act
            var mensagem = CriarMensagemNotificacaoValida();

            // Assert
            Assert.Null(mensagem.Erro);
        }

        [Theory]
        [InlineData("usuario@dominio.com.br")]
        [InlineData("contato@empresa.com")]
        [InlineData("email.com.teste@provedor.net")]
        public void CriarMensagemComDiferentesDestinatarios_DeveArmazenarDestinatarioCorreto(string destinatario)
        {
            // Act
            var mensagem = CriarMensagemNotificacaoComDestinatario(destinatario);

            // Assert
            Assert.Equal(destinatario, mensagem.Destinatario.Valor);
        }
    }
}
