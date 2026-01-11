namespace EventoWeb.Comum.Negocio.Entidades
{    
    public enum EnumSituacaoInscricao { Limbo, Pendente, Aceita, Rejeitada }

    public abstract class Inscricao: Entidade
    {
        private EnumSituacaoInscricao m_Situacao;
        public Inscricao(Evento evento, Pessoa pessoa, DateTime dataRecebimento)
        {
            Pessoa = pessoa ?? throw new ArgumentNullException(nameof(pessoa));
            Evento = evento ?? throw new ArgumentNullException(nameof(evento));
            m_Situacao = EnumSituacaoInscricao.Limbo;
            DataRecebimento = dataRecebimento;
            ConfirmadoNoEvento = false;
            DormeEvento = true;

            if (!EhValidaIdade(pessoa.DataNascimento.CalcularIdadeEmAnos(evento.PeriodoRealizacaoEvento.DataInicial)))
                throw new ArgumentException("A idade da pessoa é inválida para este tipo de inscrição.");
        }

        protected Inscricao() { }

        public virtual Pessoa Pessoa { get; }  
        public virtual Evento Evento { get; }       

        public virtual DateTime DataRecebimento { get; set; }

        public virtual Boolean DormeEvento { get; set; }

        public virtual bool ConfirmadoNoEvento { get; set; }

        public virtual EnumSituacaoInscricao Situacao => m_Situacao; 
        
        public virtual String? NomeCracha { get; set; }
        public virtual string? Observacoes { get; set; }  

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
