using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Entidades.Notificacoes;
using EventoWeb.Comum.Negocio.ObjetosValor;
using static EventoWeb.Comum.Testes.Negocio.Fixtures.EventosFixtures;

namespace EventoWeb.Comum.Testes.Negocio.Fixtures
{
    /// <summary>
    /// Fixtures para criação de dados de teste reutilizáveis relacionados a Notificações
    /// </summary>
    public static class NotificacoesFixtures
    {
        /// <summary>
        /// Cria um ModeloMensagemNotificacao com dados válidos
        /// </summary>
        public static ModeloMensagemNotificacao CriarModeloMensagemValido()
        {
            var evento = CriarEventoValido();
            var mensagem = new StringClob("Esta é uma mensagem de teste para notificação");
            var assunto = new String200("Assunto de Teste");
            var nome = new String200("Modelo de Teste");

            return new ModeloMensagemNotificacao(
                evento,
                EnumMeioNotificacao.EMail,
                EnumTipoNotificacao.InscricaoRecebida,
                mensagem,
                assunto,
                nome
            );
        }

        /// <summary>
        /// Cria um ModeloMensagemNotificacao com meio específico
        /// </summary>
        public static ModeloMensagemNotificacao CriarModeloMensagemComMeio(EnumMeioNotificacao meio)
        {
            var evento = CriarEventoValido();
            var mensagem = new StringClob("Mensagem de teste");
            var assunto = new String200("Assunto");
            var nome = new String200("Modelo Teste");

            return new ModeloMensagemNotificacao(
                evento,
                meio,
                EnumTipoNotificacao.InscricaoRecebida,
                mensagem,
                assunto,
                nome
            );
        }

        /// <summary>
        /// Cria um ModeloMensagemNotificacao com tipo específico
        /// </summary>
        public static ModeloMensagemNotificacao CriarModeloMensagemComTipo(EnumTipoNotificacao tipo)
        {
            var evento = CriarEventoValido();
            var mensagem = new StringClob("Mensagem de teste");
            var assunto = new String200("Assunto");
            var nome = new String200("Modelo Teste");

            return new ModeloMensagemNotificacao(
                evento,
                EnumMeioNotificacao.EMail,
                tipo,
                mensagem,
                assunto,
                nome
            );
        }

        /// <summary>
        /// Cria um ModeloMensagemNotificacao sem assunto (null)
        /// </summary>
        public static ModeloMensagemNotificacao CriarModeloMensagemSemAssunto()
        {
            var evento = CriarEventoValido();
            var mensagem = new StringClob("Mensagem de teste");
            var nome = new String200("Modelo Teste");

            return new ModeloMensagemNotificacao(
                evento,
                EnumMeioNotificacao.EMail,
                EnumTipoNotificacao.InscricaoRecebida,
                mensagem,
                null,
                nome
            );
        }

        /// <summary>
        /// Cria uma MensagemNotificacao com dados válidos
        /// </summary>
        public static MensagemNotificacao CriarMensagemNotificacaoValida()
        {
            var modelo = CriarModeloMensagemValido();
            var destinatario = new String500("contato@exemplo.com");
            var variaveisJson = new StringClob("{\"nome\": \"João\", \"evento\": \"Congresso 2024\"}");

            return new MensagemNotificacao(modelo, destinatario, variaveisJson);
        }

        /// <summary>
        /// Cria uma MensagemNotificacao sem variáveis JSON
        /// </summary>
        public static MensagemNotificacao CriarMensagemNotificacaoSemVariaveis()
        {
            var modelo = CriarModeloMensagemValido();
            var destinatario = new String500("contato@exemplo.com");

            return new MensagemNotificacao(modelo, destinatario, null);
        }

        /// <summary>
        /// Cria uma MensagemNotificacao com destinatário específico
        /// </summary>
        public static MensagemNotificacao CriarMensagemNotificacaoComDestinatario(string destinatario)
        {
            var modelo = CriarModeloMensagemValido();
            var variaveisJson = new StringClob("{\"nome\": \"Teste\"}");

            return new MensagemNotificacao(modelo, new String500(destinatario), variaveisJson);
        }

        /// <summary>
        /// Cria uma MensagemNotificacao com variáveis JSON específicas
        /// </summary>
        public static MensagemNotificacao CriarMensagemNotificacaoComVariaveis(string variaveisJson)
        {
            var modelo = CriarModeloMensagemValido();
            var destinatario = new String500("contato@exemplo.com");

            return new MensagemNotificacao(
                modelo,
                destinatario,
                string.IsNullOrEmpty(variaveisJson) ? null : new StringClob(variaveisJson)
            );
        }
    }
}
