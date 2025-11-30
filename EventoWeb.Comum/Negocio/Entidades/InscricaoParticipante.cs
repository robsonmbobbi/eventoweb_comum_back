namespace EventoWeb.Comum.Negocio.Entidades
{
    public enum EnumTipoParticipante { Participante, ParticipanteTrabalhador, Trabalhador }    

    public class InscricaoParticipante: Inscricao
    {
        public InscricaoParticipante(Evento evento, Pessoa pessoa, DateTime dataRecebimento) :
            base(evento, pessoa, dataRecebimento)
        {           
        }

        protected InscricaoParticipante() { }

        public virtual EnumTipoParticipante? Tipo { get; set; }

        
        public virtual string? InstituicoesEspiritasFrequenta { get; set; }      

        public override bool EhValidaIdade(int idade)
        {
            return idade >= Evento.IdadeMinimaInscricaoAdulto;
        }

        public override void Aceitar()
        {
            if (Tipo == null) {
                throw new ArgumentException("Tipo de participante deve ser informado antes de aceitar a inscrição.");
            }

            base.Aceitar();
        }
    }
}
