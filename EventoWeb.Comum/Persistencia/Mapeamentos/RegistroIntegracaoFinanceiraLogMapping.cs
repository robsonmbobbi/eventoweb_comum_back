using EventoWeb.Comum.Negocio.Entidades.IntegracaoFinanceira;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace EventoWeb.Comum.Persistencia.Mapeamentos
{
    public class RegistroIntegracaoFinanceiraLogMapping : ClassMapping<RegistroIntegracaoLog>
    {
        public RegistroIntegracaoFinanceiraLogMapping()
        {
            Table("registro_integracao_financeira_logs");

            Id(x => x.Id, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("id");
                m.Generator(Generators.Native, g =>
                {
                    g.Params(new { sequence = "gen_registro_integracao_financeira_log" });
                });
            }); 
            
            ManyToOne(x=>x.Registro, m =>
            {
                m.Column("id_registro_integracao");
                m.NotNullable(true);
            });
            
            Property(x => x.Tipo, m =>
            {
                m.Access(Accessor.Property);
                m.Column("tipo");
                m.NotNullable(true);
                m.Type<EnumGeneric<EnumTipoLog>>();
            });

            Property(x => x.Data, m =>
            {
                m.Access(Accessor.Property);
                m.Column("data");
                m.NotNullable(true);
            });
            
            this.Component(x => x.Mensagem, c =>
            {
                c.Access(Accessor.Property);
                c.Property(o => o.Valor, m => {
                    m.Access(Accessor.NoSetter);
                    m.NotNullable(true);
                    m.Column("mensagem");
                    m.Length(500);
                });
            });

            this.Component(x => x.Dados, c =>
            {
                c.Access(Accessor.Property);
                c.Property(o => o.Valor, m => {
                    m.Access(Accessor.NoSetter);
                    m.NotNullable(true);
                    m.Column("dados");
                    m.Length(4000);
                });
            });
        }
    }
}
