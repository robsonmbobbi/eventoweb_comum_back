namespace EventoWeb.Comum.Negocio.Entidades.Notificacoes
{
    public class MensagemNotificacao : Entidade
    {
        public MensagemNotificacao(ModeloMensagemNotificacao modelo, string destinatario, string variaveisJson)
        {
            Modelo = modelo;
            Destinatario = destinatario;
            VariaveisJson = variaveisJson;
            Situacao = EnumSituacaoEnvioNotificacao.Pendente;
        }

        protected MensagemNotificacao() { }

        public virtual ModeloMensagemNotificacao Modelo { get; protected set; }
        public virtual string Destinatario { get; protected set; }
        public virtual DateTime? DataSituacao { get; protected set; }
        public virtual EnumSituacaoEnvioNotificacao Situacao { get; protected set; }
        public virtual string VariaveisJson { get; protected set; }
        public virtual string? Erro { get; protected set; }

        public virtual void RegistrarEnvio()
        {
            DataSituacao = DateTime.Now;
            Situacao = EnumSituacaoEnvioNotificacao.Enviada;
        }

        public virtual void RegistrarErro(string erro)
        {
            DataSituacao = DateTime.Now;
            Situacao = EnumSituacaoEnvioNotificacao.Error;
            Erro = erro;
        }
    }
}
