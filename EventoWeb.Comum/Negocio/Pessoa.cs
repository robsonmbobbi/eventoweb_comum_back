
namespace EventoWeb.Comum.Negocio
{
    public enum EnumSexo { Masculino, Feminino }
    public class Pessoa : Entidade
    {
        private CPF m_CPF;
        private DataAniversario m_DataNascimento;
        private EMail m_EMail;
        private Celular m_CelularWP;
        private string m_Nome;

        public Pessoa(CPF cpf, EnumSexo sexo, DataAniversario dataNascimento, string nome, EMail email, Celular celularWP)
        {
            CPF = cpf;
            Masculino = sexo;
            DataNascimento = dataNascimento;
            Nome = nome;
            Email = email;
            CelularWP = celularWP;
        }

        protected Pessoa() { }

        public virtual CPF CPF
        {
            get => m_CPF; 
            set => m_CPF = value ?? throw new ArgumentNullException(nameof(CPF), $"{nameof(CPF)} não pode ser nulo.");
        }
        public virtual EnumSexo Masculino { get; set; }
        public virtual DataAniversario DataNascimento
        {
            get => m_DataNascimento; 
            set => m_DataNascimento = value ?? throw new ArgumentNullException(nameof(DataNascimento), $"{nameof(DataNascimento)} não pode ser nulo.");
        }        
        public virtual string Nome
        {
            get => m_Nome;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException($"{nameof(Nome)} não pode ser nulo ou vazio.", nameof(Nome));
                m_Nome = value;
            }
        }
        public virtual EMail Email
        {
            get => m_EMail; 
            set => m_EMail = value ?? throw new ArgumentNullException(nameof(EMail), $"{nameof(EMail)} não pode ser nulo.");
        }        
        public virtual Celular CelularWP
        {
            get => m_CelularWP;
            set => m_CelularWP = value ?? throw new ArgumentNullException(nameof(CelularWP), $"{nameof(CelularWP)} não pode ser nulo.");
        }   

        public virtual Boolean EhVegetariano { get; set; }

        public virtual Boolean EhDiabetico { get; set; }

        public virtual Boolean UsaAdocanteDiariamente { get; set; }

        public virtual String AlergiaAlimentos { get; set; }     
    }
}