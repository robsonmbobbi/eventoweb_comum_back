using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using EventoWeb.Comum.Negocio.Entidades.Financeiro;

namespace EventoWeb.Comum.Infraestrutura.Mapeamentos.Financeiro
{
    public class TransacaoContaMapping : ClassMapping<TransacaoConta>
    {
        public TransacaoContaMapping()
        {
            Table("transacoes_conta");

            Id(x => x.Id, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("id");
                m.Generator(Generators.Native, g =>
                {
                    g.Params(new { sequence = "gen_transacao_conta" });
                });
            });

            ManyToOne(x => x.Conta, m =>
            {
                m.Column("id_conta");
                m.NotNullable(true);
                m.Cascade(Cascade.None);
            });

            ManyToOne(x => x.Transacao, m =>
            {
                m.Column("id_transacao");
                m.NotNullable(false);
                m.Cascade(Cascade.None);
            });

            Property(x => x.Data, m =>
            {
                m.Column("data");
                m.NotNullable(true);
            });

            Component(x => x.ValorTransacao, m =>
            {
                m.Access(Accessor.Property);

                m.Property(c => c.Valor, y =>
                {
                    y.Access(Accessor.Property);
                    y.Column("valor_transacao");
                    y.NotNullable(true);
                });
            });

            Component(x => x.Multa, m =>
            {
                m.Access(Accessor.Property);

                m.Property(c => c.Valor, y =>
                {
                    y.Access(Accessor.Property);
                    y.Column("valor_multa");
                    y.NotNullable(true);
                });
            });

            Component(x => x.Juros, m =>
            {
                m.Access(Accessor.Property);

                m.Property(c => c.Valor, y =>
                {
                    y.Access(Accessor.Property);
                    y.Column("valor_juros");
                    y.NotNullable(true);
                });
            });

            Component(x => x.Desconto, m =>
            {
                m.Access(Accessor.Property);

                m.Property(c => c.Valor, y =>
                {
                    y.Access(Accessor.Property);
                    y.Column("valor_desconto");
                    y.NotNullable(true);
                });
            });
        }
    }
}