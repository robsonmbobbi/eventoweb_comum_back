using EventoWeb.Comum.Negocio.Entidades.Financeiro;
using EventoWeb.Comum.Persistencia.Mapeamentos;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace EventoWeb.Comum.Infraestrutura.Mapeamentos.Financeiro
{
    public class ContaMapping : ClassMapping<Conta>
    {
        public ContaMapping()
        {
            Table("contas");

            Id(x => x.Id, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("id");
                m.Generator(Generators.Native, g =>
                {
                    g.Params(new { sequence = "gen_conta" });
                });
            });

            ManyToOne(x => x.Pessoa, m =>
            {
                m.Column("id_pessoa");
                m.NotNullable(true);
            });

            Component(x => x.Valor, c =>
            {
                c.Access(Accessor.NoSetter);
                c.Property(v => v.Valor, pm => 
                {
                    pm.Column("valor");
                    pm.NotNullable(true);
                });
            });

            Property(x => x.Liquidado, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("liquidado"); 
                m.NotNullable(true);
            });

            Property(x => x.DataCriado, m =>
            {
                m.Column("data_criado");
                m.NotNullable(true);
            });
            
            this.Component(x => x.Descricao, c =>
            {
                c.Access(Accessor.NoSetter);
                c.Property(o => o.Valor, m => {
                    m.Access(Accessor.NoSetter);
                    m.NotNullable(false);
                    m.Column("descricao");
                    m.Type(NHibernateUtil.StringClob);
                });
            });

            Property(x => x.DataVencimento, m => 
            {
                m.Access(Accessor.NoSetter);
                m.Column("data_vencimento"); 
                m.NotNullable(true);
            });
            
            Property(x => x.Tipo, m => 
            {
                m.Access(Accessor.NoSetter);
                m.Column("tipo");
                m.NotNullable(true);
                m.Type<EnumGeneric<EnumTipoTransacao>>();
            });

            this.Component(x => x.ValorTotalTransacoes, c =>
            {
                c.Property(o => o.Valor, m => {
                    m.Access(Accessor.NoSetter);
                    m.NotNullable(true);
                    m.Column("valor_total_transacoes");
                });
            });

            this.Component(x => x.ValorTotalDesconto, c =>
            {
                c.Property(o => o.Valor, m => {
                    m.Access(Accessor.NoSetter);
                    m.NotNullable(true);
                    m.Column("valor_total_desconto");
                });
            });

            this.Component(x => x.ValorTotalJuros, c =>
            {
                c.Property(o => o.Valor, m => {
                    m.Access(Accessor.NoSetter);
                    m.NotNullable(true);
                    m.Column("valor_total_juros");
                });
            });

            this.Component(x => x.ValorTotalMulta, c =>
            {
                c.Property(o => o.Valor, m => {
                    m.Access(Accessor.NoSetter);
                    m.NotNullable(true);
                    m.Column("valor_total_multa");
                });
            });

            Bag(x => x.Transacoes, c =>
            {
                c.Table("transacoes_conta");
                c.Key(k => k.Column("id_conta"));
                c.Cascade(Cascade.All | Cascade.DeleteOrphans);
                c.Inverse(true);
                c.Access(Accessor.NoSetter);
            }, r => r.OneToMany());
        }
    }
}