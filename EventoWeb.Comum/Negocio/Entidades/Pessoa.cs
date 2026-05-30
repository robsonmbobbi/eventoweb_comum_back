
using EventoWeb.Comum.Negocio.ObjetosValor;

namespace EventoWeb.Comum.Negocio.Entidades
{
    public enum EnumSexo { Masculino, Feminino }
    public class Pessoa : Entidade
    {
        private CPF m_CPF;
        private DataAniversario? m_DataNascimento;
        private EMail m_EMail;
        private Telefone m_CelularWP;
        private String200 m_Nome;
        private UF? m_UF;
        private String300? m_Cidade;
        private String100? m_AlergiaAlimentos;

        public Pessoa(CPF cpf, String200 nome, EMail email, Telefone celularWP)
        {
            CPF = cpf;
            Nome = nome;
            Email = email;
            CelularWP = celularWP;
        }

        protected Pessoa() { }

        public virtual CPF CPF
        {
            get => m_CPF; 
            set => m_CPF = value ??
                throw new ArgumentNullException(nameof(CPF), $"{nameof(CPF)} não pode ser nulo.");
        }
        public virtual EnumSexo? Sexo { get; set; }
        public virtual DataAniversario? DataNascimento
        {
            get => m_DataNascimento;
            set => m_DataNascimento = value;
        }        
        public virtual String200 Nome
        {
            get => m_Nome;
            set => m_Nome = value ??
                throw new ArgumentNullException(nameof(Nome), $"{nameof(Nome)} não pode ser nulo.");
        }
        public virtual EMail Email
        {
            get => m_EMail; 
            set => m_EMail = value ?? throw new ArgumentNullException(nameof(EMail), $"{nameof(EMail)} não pode ser nulo.");
        }        
        public virtual Telefone CelularWP
        {
            get => m_CelularWP;
            set => m_CelularWP = value ?? throw new ArgumentNullException(nameof(CelularWP), $"{nameof(CelularWP)} não pode ser nulo.");
        }   

        public virtual Boolean EhVegetariano { get; set; }

        public virtual Boolean EhDiabetico { get; set; }

        public virtual Boolean UsaAdocanteDiariamente { get; set; }

        public virtual String100? AlergiaAlimentos
        {
            get => m_AlergiaAlimentos;
            set => m_AlergiaAlimentos = value;
        }

        public virtual UF? UF
        {
            get => m_UF;
            set => m_UF = value;
        }

        public virtual String300? Cidade
        {
            get => m_Cidade;
            set => m_Cidade = value;
        }
    }
}