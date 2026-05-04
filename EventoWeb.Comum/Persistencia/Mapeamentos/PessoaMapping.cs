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
                m.Column("id");
                m.Generator(Generators.Native, g =>
                {
                    g.Params(new { sequence = "gen_pessoas" });
                });
            });
            
            Component(x=>x.CPF, c =>
            {
                c.Access(Accessor.Field);
                
                c.Property(y=>y.Numero, m =>
                {
                    m.Access(Accessor.NoSetter);
                    m.Column("cpf");
                    m.Length(11);
                });
            });

            this.Component(x => x.Nome, c =>
              {
                  c.Access(Accessor.NoSetter);
                  c.Property(y=> y.Nome, m =>
                  {
                      m.Access(Accessor.NoSetter);
                      m.Column("nome");
                      m.Length(150);
                      m.NotNullable(true);
                  });
              });

            this.Component(x => x.CelularWP, c =>
            {
                c.Access(Accessor.Property);
                c.Property(y=> y.Numero, m =>
                {
                    m.Access(Accessor.NoSetter);
                    m.Column("celular");
                    m.Length(15);
                    m.NotNullable(true);
                });
            });
            this.Component(x => x.Email, c =>
            {
                c.Access(Accessor.Property);
                c.Property(y => y.Endereco, m =>
                {
                    m.Access(Accessor.NoSetter);
                    m.Column("email");
                    m.Length(100);
                    m.NotNullable(true);
                });
            });

            this.Component(x => x.AlergiaAlimentos, c =>
            {
                c.Access(Accessor.Property);
                c.Property(y => y.Descricao, m =>
                {
                    m.Access(Accessor.NoSetter);
                    m.Column("alergia_alimentos");
                    m.Length(100);
                    m.NotNullable(false);
                });
            });

            this.Component(x => x.DataNascimento, c =>
            {
                c.Access(Accessor.NoSetter);
                c.Property(y=>y.Data, m =>
                {
                    m.Access(Accessor.NoSetter);
                    m.Column("data_nascimento");
                    m.NotNullable(false);
                });
            });

            this.Component(x => x.UF, c =>
            {
                c.Access(Accessor.Property);
                c.Property(y => y.Sigla, m =>
                {
                    m.Access(Accessor.NoSetter);
                    m.Column("uf");
                    m.Length(2);
                    m.NotNullable(false);
                });
            });

            this.Component(x => x.Cidade, c =>
            {
                c.Access(Accessor.Property);
                c.Property(y => y.Nome, m =>
                {
                    m.Access(Accessor.NoSetter);
                    m.Column("cidade");
                    m.Length(300);
                    m.NotNullable(false);
                });
            });
            this.Property(x => x.EhDiabetico, m =>
            {
                m.Access(Accessor.Property);
                m.Column("eh_diabetico");
                m.NotNullable(true);
            });
            this.Property(x => x.EhVegetariano, m =>
            {
                m.Access(Accessor.Property);
                m.Column("eh_vegetariano");
                m.NotNullable(true);
            });

            this.Property(x => x.Sexo, m =>
            {
                m.Access(Accessor.Property);
                m.Column("sexo");
                m.NotNullable(false);
                m.Type<EnumGeneric<EnumSexo>>();
            });
            this.Property(x => x.UsaAdocanteDiariamente, m =>
            {
                m.Access(Accessor.Property);
                m.Column("usa_adocante_diar");
                m.NotNullable(false);
            });
        }
    }
}
