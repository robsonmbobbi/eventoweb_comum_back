using EventoWeb.Comum.Negocio.ObjetosValor;

namespace EventoWeb.Comum.Negocio.Entidades.Notificacoes
{      
    public class ModeloMensagemNotificacao : Entidade
    {
        public ModeloMensagemNotificacao(Evento evento, EnumMeioNotificacao meio, EnumTipoNotificacao tipo, string mensagem, string? assunto, NomeCompleto nome)
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
        public virtual string Mensagem { get; protected set; }
        public virtual string? Assunto { get; protected set; }
        public virtual NomeCompleto Nome { get; protected set; }
        public virtual bool Ativo { get; set; }
    }
}
