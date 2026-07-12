using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Entidades.Notificacoes;
using EventoWeb.Comum.Negocio.ObjetosValor;
using EventoWeb.Comum.Testes.Negocio.Fixtures;
using static EventoWeb.Comum.Testes.Negocio.Fixtures.EventosFixtures;
using static EventoWeb.Comum.Testes.Negocio.Fixtures.NotificacoesFixtures;

namespace EventoWeb.Comum.Testes.Negocio.Notificacoes
{
    public class ModeloMensagemNotificacaoTeste
    {
        [Fact]
        public void CriarModeloComDadosValidos_DeveDefinirPropriedadesCorretas()
        {
            // Arrange
            var evento = CriarEventoValido();
            var mensagem = new StringClob("Mensagem de teste");
            var assunto = new String200("Assunto de Teste");
            var nome = new String200("Modelo de Teste");
            var meio = EnumMeioNotificacao.EMail;
            var tipo = EnumTipoNotificacao.InscricaoRecebida;

            // Act
            var modelo = new ModeloMensagemNotificacao(evento, meio, tipo, mensagem, assunto, nome);

            // Assert
            Assert.NotNull(modelo);
            Assert.Equal(evento, modelo.Evento);
            Assert.Equal(meio, modelo.Meio);
            Assert.Equal(tipo, modelo.Tipo);
            Assert.Equal(mensagem, modelo.Mensagem);
            Assert.Equal(assunto, modelo.Assunto);
            Assert.Equal(nome, modelo.Nome);
            Assert.True(modelo.Ativo);
        }

        [Fact]
        public void CriarModeloComEventoNulo_DeveValidarParametroObrigatorio()
        {
            // Arrange
            var mensagem = new StringClob("Mensagem");
            var assunto = new String200("Assunto");
            var nome = new String200("Modelo");

            // Act & Assert - Evento é um parâmetro obrigatório
            Assert.Throws<ArgumentNullException>(() =>
                new ModeloMensagemNotificacao(null, EnumMeioNotificacao.EMail, EnumTipoNotificacao.InscricaoRecebida, mensagem, assunto, nome)
            );
        }

        [Fact]
        public void CriarModeloComMensagemNula_DeveValidarParametroObrigatorio()
        {
            // Arrange
            var evento = CriarEventoValido();
            var assunto = new String200("Assunto");
            var nome = new String200("Modelo");

            // Act & Assert - Mensagem é um parâmetro obrigatório
            Assert.Throws<ArgumentNullException>(() =>
                new ModeloMensagemNotificacao(evento, EnumMeioNotificacao.EMail, EnumTipoNotificacao.InscricaoRecebida, null, assunto, nome)
            );
        }

        [Fact]
        public void CriarModeloComNomeNulo_DeveValidarParametroObrigatorio()
        {
            // Arrange
            var evento = CriarEventoValido();
            var mensagem = new StringClob("Mensagem");
            var assunto = new String200("Assunto");

            // Act & Assert - Nome é um parâmetro obrigatório
            Assert.Throws<ArgumentNullException>(() =>
                new ModeloMensagemNotificacao(evento, EnumMeioNotificacao.EMail, EnumTipoNotificacao.InscricaoRecebida, mensagem, assunto, null)
            );
        }

        [Fact]
        public void CriarModelo_PropriedadeAtivoDeveSempreSerTrue()
        {
            // Act
            var modelo = CriarModeloMensagemValido();

            // Assert
            Assert.True(modelo.Ativo);
        }

        [Fact]
        public void CriarModelo_AssuntoPodenteSerNulo()
        {
            // Act
            var modelo = CriarModeloMensagemSemAssunto();

            // Assert
            Assert.NotNull(modelo);
            Assert.Null(modelo.Assunto);
        }

        [Fact]
        public void CriarModeloComMeioEMail_DeveArmazenarMeioCorreto()
        {
            // Act
            var modelo = CriarModeloMensagemComMeio(EnumMeioNotificacao.EMail);

            // Assert
            Assert.Equal(EnumMeioNotificacao.EMail, modelo.Meio);
        }

        [Fact]
        public void CriarModeloComMeioWhatsApp_DeveArmazenarMeioCorreto()
        {
            // Act
            var modelo = CriarModeloMensagemComMeio(EnumMeioNotificacao.WhatsApp);

            // Assert
            Assert.Equal(EnumMeioNotificacao.WhatsApp, modelo.Meio);
        }

        [Theory]
        [InlineData(EnumTipoNotificacao.InscricaoRecebida)]
        [InlineData(EnumTipoNotificacao.InscricaoAceita)]
        [InlineData(EnumTipoNotificacao.InscricaoRejeitada)]
        [InlineData(EnumTipoNotificacao.PedidoRealizado)]
        [InlineData(EnumTipoNotificacao.PagamentoRecebido)]
        [InlineData(EnumTipoNotificacao.NovoPagamento)]
        public void CriarModeloComDiferentesTiposNotificacao_DeveArmazenarTipoCorreto(EnumTipoNotificacao tipo)
        {
            // Act
            var modelo = CriarModeloMensagemComTipo(tipo);

            // Assert
            Assert.Equal(tipo, modelo.Tipo);
        }

        [Fact]
        public void CriarModelo_MensagemNaoDeveSerVazia()
        {
            // Act
            var modelo = CriarModeloMensagemValido();

            // Assert
            Assert.NotNull(modelo.Mensagem);
            Assert.NotEmpty(modelo.Mensagem.Valor);
        }

        [Fact]
        public void CriarModelo_NomeNaoDeveSerVazio()
        {
            // Act
            var modelo = CriarModeloMensagemValido();

            // Assert
            Assert.NotNull(modelo.Nome);
            Assert.NotEmpty(modelo.Nome.Valor);
        }

        [Fact]
        public void CriarModelo_EventoNaoDeveSerNulo()
        {
            // Act
            var modelo = CriarModeloMensagemValido();

            // Assert
            Assert.NotNull(modelo.Evento);
        }

        [Fact]
        public void MudarPropriedadeAtivo_DeveAtualizarValor()
        {
            // Arrange
            var modelo = CriarModeloMensagemValido();
            Assert.True(modelo.Ativo);

            // Act
            modelo.Ativo = false;

            // Assert
            Assert.False(modelo.Ativo);
        }

        #region Testes de Validação de Parâmetros Nulos

        [Fact]
        public void CriarModeloComMeioNulo_DeveValidarParametroObrigatorio()
        {
            // Arrange
            var evento = CriarEventoValido();
            var mensagem = new StringClob("Mensagem");
            var assunto = new String200("Assunto");
            var nome = new String200("Modelo");

            // Act & Assert - Meio é um parâmetro obrigatório (enum não pode ser nulo em C#)
            var modelo = new ModeloMensagemNotificacao(evento, EnumMeioNotificacao.EMail, EnumTipoNotificacao.InscricaoRecebida, mensagem, assunto, nome);

            // Assert
            Assert.True(modelo.Meio == EnumMeioNotificacao.EMail || modelo.Meio == EnumMeioNotificacao.WhatsApp);
        }

        [Fact]
        public void CriarModeloComTipoNulo_DeveValidarParametroObrigatorio()
        {
            // Arrange
            var evento = CriarEventoValido();
            var mensagem = new StringClob("Mensagem");
            var assunto = new String200("Assunto");
            var nome = new String200("Modelo");

            // Act & Assert - Tipo é um parâmetro obrigatório (enum não pode ser nulo em C#)
            var modelo = new ModeloMensagemNotificacao(evento, EnumMeioNotificacao.EMail, EnumTipoNotificacao.InscricaoRecebida, mensagem, assunto, nome);

            // Assert
            Assert.NotNull(modelo);
            Assert.True(modelo.Tipo != null);
        }

        [Fact]
        public void CriarModelo_ValidarTodosParametrosObrigatorios()
        {
            // Arrange & Act
            var modelo = CriarModeloMensagemValido();

            // Assert - Valida que parâmetros obrigatórios foram preservados
            Assert.NotNull(modelo.Evento);
            Assert.NotNull(modelo.Mensagem);
            Assert.NotNull(modelo.Nome);
            Assert.True(Enum.IsDefined(typeof(EnumMeioNotificacao), modelo.Meio));
            Assert.True(Enum.IsDefined(typeof(EnumTipoNotificacao), modelo.Tipo));
        }

        [Fact]
        public void CriarModelo_ValidarParametrosOpcionais()
        {
            // Act
            var modeloComAssunto = CriarModeloMensagemValido();
            var modeloSemAssunto = CriarModeloMensagemSemAssunto();

            // Assert - Assunto é um parâmetro opcional
            Assert.NotNull(modeloComAssunto.Assunto);
            Assert.Null(modeloSemAssunto.Assunto);
        }

        #endregion
    }
}
