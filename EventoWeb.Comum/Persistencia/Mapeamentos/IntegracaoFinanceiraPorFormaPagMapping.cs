using EventoWeb.Comum.Negocio.Entidades.IntegracaoFinanceira;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace EventoWeb.Comum.Persistencia.Mapeamentos
{
    public class IntegracaoFinanceiraPorFormaPagMapping : ClassMapping<IntegracaoFinanceiraPorFormaPag>
    {
        public IntegracaoFinanceiraPorFormaPagMapping()
        {
            Table("integracao_financeira_formas_pags");

            Id(x => x.Id, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("id");
                m.Generator(Generators.Native, g =>
                {
                    g.Params(new { sequence = "gen_integracao_financeira_forma_pag" });
                });
            });

            ManyToOne(x => x.Integrador, m =>
            {
                m.Column("id_integrador_financeiro");
                m.NotNullable(true);
            });
            ManyToOne(x => x.FormaPagamento, m =>
            {
                m.Column("id_forma_pagamento");
                m.NotNullable(true);
            });
            Property(x => x.IntegracaoExterna, m =>
            {
                m.Column("tipo");
                m.NotNullable(true);
                m.Type<EnumGeneric<EnumIntegracaoExterna>>();
            });
        }
    }
}
