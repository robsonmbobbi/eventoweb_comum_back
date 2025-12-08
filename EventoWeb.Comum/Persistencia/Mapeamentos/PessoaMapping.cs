using EventoWeb.Comum.Negocio.Entidades;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace EventoWeb.Comum.Persistencia.Mapeamentos
{
    public class PessoaMapping: ClassMapping<Pessoa>
    {
        public PessoaMapping()
        {
            this.Table("pessoas");

            this.Id(x => x.Id, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("ID");
                m.Generator(Generators.Native, g =>
                {
                    g.Params(new { sequence = "GEN_PESSOAS" });
                });
            });
            
            Component(x=>x.CPF, c =>
            {
                c.Access(Accessor.Field);
                
                c.Property(y=>y.Numero, m =>
                {
                    m.Access(Accessor.Property);
                    m.Column("CPF");
                    m.Length(11);
                });
            });

            this.Component(x => x.Nome, c =>
              {
                  c.Access(Accessor.NoSetter);
                  c.Property(y=> y.Nome, m =>
                  {
                      m.Column("NOME");
                      m.Length(150);
                      m.NotNullable(true);
                  });
              });

            this.Component(x => x.CelularWP, c =>
            {
                c.Access(Accessor.Property);
                c.Property(y=> y.Numero, m =>
                {
                    m.Column("CELULAR");
                    m.Length(15);
                    m.NotNullable(true);
                });
            });
            this.Component(x => x.Email, c =>
            {
                c.Access(Accessor.Property);
                c.Property(y => y.Endereco, m =>
                {
                    m.Column("EMAIL");
                    m.Length(100);
                    m.NotNullable(true);
                });
            });
            
            this.Property(x => x.AlergiaAlimentos, m =>
            {
                m.Access(Accessor.Property);
                m.Column("ALERGIA_ALIMENTOS");
                m.Length(100);
                m.NotNullable(false);
            });
            this.Component(x => x.DataNascimento, c =>
            {
                c.Access(Accessor.NoSetter);
                c.Property(y=>y.Data, m =>
                {
                    m.Column("DATA_NASCIMENTO");
                    m.NotNullable(true);
                });
            });
            this.Property(x => x.EhDiabetico, m =>
            {
                m.Access(Accessor.Property);
                m.Column("EH_DIABETICO");
                m.NotNullable(true);
            });
            this.Property(x => x.EhVegetariano, m =>
            {
                m.Access(Accessor.Property);
                m.Column("EH_VEGETARIANO");
                m.NotNullable(true);
            });
            
            this.Property(x => x.Sexo, m =>
            {
                m.Access(Accessor.Property);
                m.Column("SEXO");
                m.NotNullable(false);
                m.Type<EnumGeneric<EnumSexo>>();
            });
            this.Property(x => x.UsaAdocanteDiariamente, m =>
            {
                m.Access(Accessor.Property);
                m.Column("USA_ADOCANTE_DIAR");
                m.NotNullable(false);
            });
        }
    }
}
