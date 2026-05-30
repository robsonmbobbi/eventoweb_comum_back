namespace EventoWeb.Comum.Negocio.Entidades
{
    public class InscricaoInfantil : Inscricao
    {
        private const String MSG_ERRO_INSCRICAO_OUTRO_EVENTO = "A inscrição do responsável deve ser do mesmo evento da inscrição infantil.";

        private InscricaoParticipante m_InscricaoResponsavel1;
        private InscricaoParticipante? m_InscricaoResponsavel2;

        protected InscricaoInfantil() { }

        public InscricaoInfantil(Pessoa pessoa, Evento evento, InscricaoParticipante inscricaoResponsavel,
            InscricaoParticipante? inscricaoResponsavel2, DateTime dataRecebimento, bool dormeEvento)
            : base(evento, pessoa, dataRecebimento)
        {
            DormeEvento = dormeEvento;
            AtribuirResponsaveis(inscricaoResponsavel, inscricaoResponsavel2);
        }

        public virtual InscricaoParticipante InscricaoResponsavel1 
        { 
            get { return m_InscricaoResponsavel1; }
        }
        public virtual InscricaoParticipante? InscricaoResponsavel2 
        { 
            get { return m_InscricaoResponsavel2; }
        }

        public virtual void AtribuirResponsaveis(InscricaoParticipante responsavel1, InscricaoParticipante? responsavel2)
        {
            if (Evento != responsavel1.Evento)
                throw new ArgumentException(MSG_ERRO_INSCRICAO_OUTRO_EVENTO, nameof(responsavel1));

            if (responsavel2 != null)
            {
                if (Evento != responsavel2.Evento)
                    throw new ArgumentException(MSG_ERRO_INSCRICAO_OUTRO_EVENTO, nameof(responsavel2));

                if (responsavel1 == responsavel2)
                    throw new ArgumentException("Os responsáveis devem ser diferentes.", $"{nameof(responsavel1)}/{nameof(responsavel2)}");
            }

            if (DormeEvento &&
                (!responsavel1.DormeEvento && !(responsavel2?.DormeEvento ?? false)))
                throw new ArgumentException("Para uma criança que dormirá no evento, pelo um dos responsáveis deve dormir no evento.", $"{nameof(responsavel1)}/{nameof(responsavel2)}");

            m_InscricaoResponsavel1 = responsavel1;
            m_InscricaoResponsavel2 = responsavel2;
        }
        
        public override bool EhValidaIdade(int idade)
        {
            return idade < Evento.IdadeMinimaInscricaoAdulto.Valor;
        }

        public override void Aceitar()
        {
            if (InscricaoResponsavel1.Situacao != EnumSituacaoInscricao.Aceita &&
                InscricaoResponsavel2?.Situacao != EnumSituacaoInscricao.Aceita)
                throw new Exception("Não é possível aceitar uma inscrição infantil sem que pelo menos um dos responsáveis por ela esteja aceito também.");

            base.Aceitar();
        }
    }
}
