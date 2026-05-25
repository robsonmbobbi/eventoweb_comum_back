using EventoWeb.Comum.Negocio.ObjetosValor;

namespace EventoWeb.Comum.Negocio.Entidades.Notificacoes
{      
    public class ModeloMensagemNotificacao : Entidade
    {
        private StringClob? m_Mensagem;
        private StringClob? m_Assunto;

        public ModeloMensagemNotificacao(Evento evento, EnumMeioNotificacao meio, EnumTipoNotificacao tipo, StringClob mensagem, StringClob? assunto, NomeCompleto nome)
        {
            Evento = evento;
            Meio = meio;
            Tipo = tipo;
            Mensagem = mensagem;
            Assunto = assunto;
            Nome = nome;
            Ativo = true;
        }

        protected ModeloMensagemNotificacao() { }

        public virtual Evento Evento { get; protected set; }
        public virtual EnumMeioNotificacao Meio { get; protected set; }
        public virtual EnumTipoNotificacao Tipo { get; protected set; }

        public virtual StringClob? Mensagem
        {
            get => m_Mensagem;
            protected set => m_Mensagem = value;
        }

        public virtual StringClob? Assunto
        {
            get => m_Assunto;
            protected set => m_Assunto = value;
        }

        public virtual NomeCompleto Nome { get; protected set; }
        public virtual bool Ativo { get; set; }
    }
}
