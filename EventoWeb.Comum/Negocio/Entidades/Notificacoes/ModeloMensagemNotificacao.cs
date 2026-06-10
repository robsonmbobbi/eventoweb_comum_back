using EventoWeb.Comum.Negocio.ObjetosValor;

namespace EventoWeb.Comum.Negocio.Entidades.Notificacoes
{      
    public class ModeloMensagemNotificacao : Entidade
    {
        private StringClob m_Mensagem;
        private String200? m_Assunto;

        public ModeloMensagemNotificacao(Evento evento, EnumMeioNotificacao meio, EnumTipoNotificacao tipo, StringClob mensagem, String200? assunto, String200 nome)
        {
            Evento = evento ?? throw new ArgumentNullException(nameof(evento));
            Meio = meio;
            Tipo = tipo;
            Mensagem = mensagem ?? throw new ArgumentNullException(nameof(mensagem));
            Assunto = assunto;
            Nome = nome ?? throw new ArgumentNullException(nameof(nome));
            Ativo = true;
        }

        protected ModeloMensagemNotificacao() { }

        public virtual Evento Evento { get; protected set; }
        public virtual EnumMeioNotificacao Meio { get; protected set; }
        public virtual EnumTipoNotificacao Tipo { get; protected set; }

        public virtual StringClob Mensagem
        {
            get => m_Mensagem;
            protected set => m_Mensagem = value;
        }

        public virtual String200? Assunto
        {
            get => m_Assunto;
            protected set => m_Assunto = value;
        }

        public virtual String200 Nome { get; protected set; }
        public virtual bool Ativo { get; set; }
    }
}
