using EventoWeb.Comum.Negocio.ObjetosValor;

namespace EventoWeb.Comum.Negocio.Entidades
{    
    public enum EnumSituacaoInscricao { Limbo, Pendente, Aceita, Rejeitada }

    public abstract class Inscricao: Entidade
    {
        private EnumSituacaoInscricao m_Situacao;
        private NomeCompleto? m_NomeCracha;
        private StringClob? m_Observacoes;

        public Inscricao(Evento evento, Pessoa pessoa, DateTime dataRecebimento)
        {
            Pessoa = pessoa ?? throw new ArgumentNullException(nameof(pessoa));
            Evento = evento ?? throw new ArgumentNullException(nameof(evento));
            m_Situacao = EnumSituacaoInscricao.Limbo;
            DataRecebimento = dataRecebimento;
            ConfirmadoNoEvento = false;
            DormeEvento = true;

            if (!EhValidaIdade(pessoa.DataNascimento!.CalcularIdadeEmAnos(evento.PeriodoRealizacaoEvento.DataInicial)))
                throw new ArgumentException("A idade da pessoa é inválida para este tipo de inscrição.");
        }

        protected Inscricao() { }

        public virtual Pessoa Pessoa { get; protected set; }  
        public virtual Evento Evento { get; protected set; }       

        public virtual DateTime DataRecebimento { get; set; }

        public virtual Boolean DormeEvento { get; set; }

        public virtual bool ConfirmadoNoEvento { get; set; }

        public virtual EnumSituacaoInscricao Situacao => m_Situacao; 

        public virtual NomeCompleto? NomeCracha
        {
            get => m_NomeCracha;
            set => m_NomeCracha = value;
        }

        public virtual StringClob? Observacoes
        {
            get => m_Observacoes;
            set => m_Observacoes = value;
        }

        public abstract Boolean EhValidaIdade(int idade);

        public virtual void Aceitar()
        {
            if (m_Situacao != EnumSituacaoInscricao.Pendente)
                throw new Exception("Só se pode tornar uma inscrição aceita se ela estiver na situação Pendente");

            m_Situacao = EnumSituacaoInscricao.Aceita;
        }

        public virtual void Rejeitar()
        {
            if (m_Situacao != EnumSituacaoInscricao.Pendente)
                throw new Exception("Só se pode tornar uma inscrição rejeitada se ela estiver na situação Pendente");

            m_Situacao = EnumSituacaoInscricao.Rejeitada;
        }

        public virtual void TornarPendente()
        {
            if (m_Situacao != EnumSituacaoInscricao.Limbo)
                throw new Exception("Só se pode tornar uma inscrição pendente se ela estiver na situação Limbo");

            m_Situacao = EnumSituacaoInscricao.Pendente;
        }
    }
}
