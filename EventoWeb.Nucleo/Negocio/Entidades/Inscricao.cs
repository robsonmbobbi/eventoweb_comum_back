using EventoWeb.Nucleo.Negocio.Excecoes;

namespace EventoWeb.Nucleo.Negocio.Entidades
{    
    public enum EnumSituacaoInscricao { Pendente, Aceita, Rejeitada }

    public abstract class Inscricao: Entidade
    {
        private Pessoa m_Pessoa;
        private Evento m_Evento;
        private EnumSituacaoInscricao m_Situacao;

        public Inscricao(Evento evento, Pessoa pessoa, DateTime dataRecebimento)
        {
            m_Pessoa = pessoa ?? throw new ArgumentNullException(nameof(pessoa));
            m_Evento = evento ?? throw new ArgumentNullException(nameof(evento));
            m_Situacao = EnumSituacaoInscricao.Pendente;
            DataRecebimento = dataRecebimento;
            ConfirmadoNoEvento = false;
            DormeEvento = true;

            if (!EhValidaIdade(pessoa.CalcularIdadeEmAnos(evento.PeriodoRealizacaoEvento.DataInicial)))
                throw new ArgumentException("A idade da pessoa é inválida para este tipo de inscrição.");
        }

        protected Inscricao() { }

        public virtual Pessoa Pessoa { get { return m_Pessoa; } }
        public virtual Evento Evento { get { return m_Evento; } }        

        public virtual DateTime DataRecebimento { get; set; }

        public virtual Boolean DormeEvento { get; set; }

        public virtual bool ConfirmadoNoEvento { get; set; }

        public virtual EnumSituacaoInscricao Situacao { get => m_Situacao; }
        public virtual String? NomeCracha { get; set; }
        public virtual string? Observacoes { get; set; }  

        public abstract Boolean EhValidaIdade(int idade);

        public virtual void Aceitar()
        {
            if (m_Situacao != EnumSituacaoInscricao.Pendente)
                throw new ExcecaoNegocio("Inscricao", "Só se pode tornar uma inscrição aceita se ela estiver na situação Pendente");

            m_Situacao = EnumSituacaoInscricao.Aceita;
        }

        public virtual void Rejeitar()
        {
            if (m_Situacao != EnumSituacaoInscricao.Pendente)
                throw new ExcecaoNegocio("Inscricao", "Só se pode tornar uma inscrição rejeitada se ela estiver na situação Pendente");

            m_Situacao = EnumSituacaoInscricao.Rejeitada;
        }
    }
}
