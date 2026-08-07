using EventoWeb.Comum.Negocio.Entidades.Notificacoes;
using EventoWeb.Comum.Negocio.Repositorios;
using EventoWeb.Comum.Negocio.Servicos.Notificacoes;
using static EventoWeb.Comum.Testes.Negocio.Fixtures.NotificacoesFixtures;

namespace EventoWeb.Comum.Testes.Negocio.Servicos.Notificacoes
{
    public class EnvioNotificacaoExtensionsTeste
    {
        private class EnvioNotificacaoFalho(Exception excecao) : IEnvioNotificacao
        {
            public Task Enviar(MensagemNotificacao mensagem) => throw excecao;
        }

        private class EnvioNotificacaoSucesso : IEnvioNotificacao
        {
            public Task Enviar(MensagemNotificacao mensagem) => Task.CompletedTask;
        }

        private class PersistenciaMensagensFake : IPersistencia<MensagemNotificacao>
        {
            public bool AtualizarFoiChamado { get; private set; }
            public void Incluir(MensagemNotificacao objeto) { }
            public void Excluir(MensagemNotificacao objeto) { }
            public void Atualizar(MensagemNotificacao objeto) => AtualizarFoiChamado = true;
            public MensagemNotificacao? Obter(int id) => null;
        }

        [Fact]
        public void EnviarERegistrar_QuandoEnviarLancaExcecao_DeveRegistrarErroFila()
        {
            var mensagem = CriarMensagemNotificacaoValida();
            var envio = new EnvioNotificacaoFalho(new InvalidOperationException("Falha ao conectar na fila"));
            var mensagens = new PersistenciaMensagensFake();

            envio.EnviarERegistrar(mensagens, mensagem);

            Assert.Equal(EnumSituacaoEnvioNotificacao.FilaErro, mensagem.Situacao);
        }

        [Fact]
        public void EnviarERegistrar_QuandoEnviarLancaExcecao_DeveDefinirMensagemErro()
        {
            var mensagem = CriarMensagemNotificacaoValida();
            var envio = new EnvioNotificacaoFalho(new InvalidOperationException("Falha ao conectar na fila"));
            var mensagens = new PersistenciaMensagensFake();

            envio.EnviarERegistrar(mensagens, mensagem);

            Assert.NotNull(mensagem.Erro);
            Assert.Equal("Falha ao conectar na fila", mensagem.Erro!.Valor);
        }

        [Fact]
        public void EnviarERegistrar_QuandoEnviarLancaExcecao_DeveAtualizarMensagem()
        {
            var mensagem = CriarMensagemNotificacaoValida();
            var envio = new EnvioNotificacaoFalho(new InvalidOperationException("Falha ao conectar na fila"));
            var mensagens = new PersistenciaMensagensFake();

            envio.EnviarERegistrar(mensagens, mensagem);

            Assert.True(mensagens.AtualizarFoiChamado);
        }

        [Fact]
        public void EnviarERegistrar_QuandoEnviarNaoLancaExcecao_SituacaoPermaneceEmFila()
        {
            var mensagem = CriarMensagemNotificacaoValida();
            var envio = new EnvioNotificacaoSucesso();
            var mensagens = new PersistenciaMensagensFake();

            envio.EnviarERegistrar(mensagens, mensagem);

            Assert.Equal(EnumSituacaoEnvioNotificacao.EmFila, mensagem.Situacao);
        }

        [Fact]
        public void EnviarERegistrar_QuandoEnviarNaoLancaExcecao_NaoDeveAtualizarMensagem()
        {
            var mensagem = CriarMensagemNotificacaoValida();
            var envio = new EnvioNotificacaoSucesso();
            var mensagens = new PersistenciaMensagensFake();

            envio.EnviarERegistrar(mensagens, mensagem);

            Assert.False(mensagens.AtualizarFoiChamado);
        }
    }
}
