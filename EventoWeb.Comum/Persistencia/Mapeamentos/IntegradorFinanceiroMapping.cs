using EventoWeb.Comum.Negocio.Entidades.IntegracaoFinanceira;
using EventoWeb.Comum.Persistencia.Mapeamentos;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace EventoWeb.Comum.Infraestrutura.Mapeamentos.Financeiro
{
    public class IntegradorFinanceiroMapping : ClassMapping<IntegradorFinanceiro>
    {
        public IntegradorFinanceiroMapping()
        {
            Table("integradores_financeiros");

            Id(x => x.Id, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("id");
                m.Generator(Generators.Native, g =>
                {
                    g.Params(new { sequence = "gen_integrador_financeiro" });
                });
            });

            ManyToOne(x => x.ContaBancaria, m =>
            {
                m.Column("id_conta_bancaria");
                m.NotNullable(true);
            });

            this.Component(x => x.TokenAcesso, c =>
            {
                c.Access(Accessor.NoSetter);

                c.Property(o => o.Valor, m => {
                    m.Access(Accessor.NoSetter);
                    m.NotNullable(true);
                    m.Column("token_acesso");
                    m.Length(1000);
                });
            });

            Property(x => x.IntegracaoExterna, m => 
            {
                m.Access(Accessor.Property);
                m.Column("integracao_externa");
                m.NotNullable(true);
                m.Type<EnumGeneric<EnumIntegracaoExterna>>();
            });            
        }
    }
}