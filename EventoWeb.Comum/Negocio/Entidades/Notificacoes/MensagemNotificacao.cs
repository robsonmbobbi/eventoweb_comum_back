using EventoWeb.Comum.Negocio.ObjetosValor;

namespace EventoWeb.Comum.Negocio.Entidades.Notificacoes
{
    public class MensagemNotificacao : Entidade
    {
        private String500? m_Destinatario;
        private StringClob? m_VariaveisJson;
        private StringClob? m_Erro;

        public MensagemNotificacao(ModeloMensagemNotificacao modelo, String500 destinatario, StringClob? variaveisJson)
        {
            Modelo = modelo;
            Destinatario = destinatario;
            VariaveisJson = variaveisJson;
            Situacao = EnumSituacaoEnvioNotificacao.Pendente;
        }

        protected MensagemNotificacao() { }

        public virtual ModeloMensagemNotificacao Modelo { get; protected set; }

        public virtual String500? Destinatario
        {
            get => m_Destinatario;
            protected set => m_Destinatario = value;
        }

        public virtual DateTime? DataSituacao { get; protected set; }
        public virtual EnumSituacaoEnvioNotificacao Situacao { get; protected set; }

        public virtual StringClob? VariaveisJson
        {
            get => m_VariaveisJson;
            protected set => m_VariaveisJson = value;
        }

        public virtual StringClob? Erro
        {
            get => m_Erro;
            protected set => m_Erro = value;
        }

        public virtual void RegistrarEnvio()
        {
            DataSituacao = DateTime.Now;
            Situacao = EnumSituacaoEnvioNotificacao.Enviada;
        }

        public virtual void RegistrarErro(StringClob erro)
        {
            DataSituacao = DateTime.Now;
            Situacao = EnumSituacaoEnvioNotificacao.Error;
            Erro = erro;
        }
    }
}
