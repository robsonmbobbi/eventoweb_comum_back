using EventoWeb.Comum.Negocio.ObjetosValor;

namespace EventoWeb.Comum.Negocio.Entidades
{
    public class Evento: Entidade
    {
        private String200 m_Nome;
        private DateTime m_DataRegistro;
        private Periodo m_PeriodoInscricaoOnLine;
        private Periodo m_PeriodoRealizacaoEvento;
        private StringClob? m_Regulamento;

        protected Evento()
        {
        }

        public Evento(String200 nome, Periodo periodoInscricaoOnline, Periodo periodoRealizacaoEvento)
        {
            Nome = nome;
            PeriodoInscricaoOnLine = periodoInscricaoOnline;
            PeriodoRealizacaoEvento = periodoRealizacaoEvento;
            m_DataRegistro = DateTime.Today;
            IdadeMinimaInscricaoAdulto = new InteiroPositivo(13);
        }

        public virtual String200 Nome 
        {
            get => m_Nome; 
            set => m_Nome = value ?? throw new ArgumentNullException(nameof(Nome));
        }

        public virtual Periodo PeriodoRealizacaoEvento
        {
            get => m_PeriodoRealizacaoEvento;
            set => m_PeriodoRealizacaoEvento = value ??
                throw new ArgumentNullException(nameof(PeriodoRealizacaoEvento));
        }        

        public virtual Periodo PeriodoInscricaoOnLine
        {
            get => m_PeriodoInscricaoOnLine;
            set => m_PeriodoInscricaoOnLine = value ??
                throw new ArgumentNullException(nameof(PeriodoInscricaoOnLine));
        }

        public virtual DateTime DataRegistro { get { return m_DataRegistro; } }

        public virtual ArquivoBinario? Logotipo { get; set; }
        public virtual InteiroPositivo IdadeMinimaInscricaoAdulto { get; set; }

        public virtual StringClob? Regulamento
        {
            get => m_Regulamento;
            set => m_Regulamento = value;
        }
    }
}
