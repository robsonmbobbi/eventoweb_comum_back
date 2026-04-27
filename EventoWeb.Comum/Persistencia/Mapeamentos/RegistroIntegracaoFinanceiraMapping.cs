using EventoWeb.Comum.Negocio.Entidades.Financeiro;
using EventoWeb.Comum.Negocio.Entidades.IntegracaoFinanceira;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace EventoWeb.Comum.Persistencia.Mapeamentos
{
    public class RegistroIntegracaoFinanceiraMapping : ClassMapping<RegistroIntegracaoFinanceira>
    {
        public RegistroIntegracaoFinanceiraMapping()
        {
            Table("registros_integracao_financeira");

            Id(x => x.Id, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("id");
                m.Generator(Generators.Native, g =>
                {
                    g.Params(new { sequence = "gen_registro_integracao_financeira" });
                });
            });

            ManyToOne(x => x.Integrador, m =>
            {
                m.Access(Accessor.Property);
                m.Column("id_integrador_financeiro");
                m.NotNullable(true);
            });

            ManyToOne(x => x.Conta, m =>
            {
                m.Access(Accessor.Property);
                m.Column("id_conta");
                m.NotNullable(true);               
            });

            Component(x => x.Valor, m =>
            {
                m.Access(Accessor.Property);

                m.Property(c => c.Valor, y =>
                {
                    y.Access(Accessor.Property);
                    y.Column("valor");
                    y.NotNullable(true);
                });
            });

            Property(x => x.DataRegistro, m =>
            {
                m.Access(Accessor.Property);
                m.Column("data_registro");
                m.NotNullable(true);
            });

            Property(x => x.Tipo, m =>
            {
                m.Access(Accessor.Property);
                m.Column("tipo");
                m.Type<EnumGeneric<EnumTipoPagamento>>();
                m.NotNullable(true);
            });

            Property(x => x.Situacao, m =>
            {
                m.Access(Accessor.Property);
                m.Column("situacao");
                m.NotNullable(true);
                m.Type<EnumGeneric<EnumSituacaoIntegracao>>();
            });

            Property(x => x.NumeroParcelas, m =>
            {
                m.Access(Accessor.Property);
                m.Column("numero_parcelas");
                m.NotNullable(false);
            });

            Property(x => x.DataConcluidoAbortado, m =>
            {
                m.Access(Accessor.Property);
                m.Column("data_concluido_abortado");
            });

            Property(x => x.IdentificacaoNoIntegrador, m =>
            {
                m.Access(Accessor.Property);
                m.Column("id_no_integrador");
                m.Length(1000);
                m.NotNullable(true);
            });

            ManyToOne(x => x.Transacao, m =>
            {
                m.Access(Accessor.Property);
                m.Column(("id_transacao"));
                m.NotNullable(false);
            });


            Bag(x => x.Logs, m =>
            {
                m.Access(Accessor.Field);
                m.Key(k => k.Column("id_registro_integracao")); 
                m.Cascade(Cascade.All);
            }, r => r.OneToMany());
        }
    }
}
