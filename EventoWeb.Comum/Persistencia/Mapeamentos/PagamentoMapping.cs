using EventoWeb.Comum.Negocio.Entidades;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace EventoWeb.Comum.Persistencia.Mapeamentos
{
    public class PagamentoMapping : ClassMapping<Pagamento>
    {
        public PagamentoMapping()
        {
            Table("pagamentos");

            Id(x => x.Id, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("id");
                m.Generator(Generators.Native, g =>
                {
                    g.Params(new { sequence = "gen_pagamento" });
                });
            });            
            
            ManyToOne(x => x.Pedido, m =>
            {
                m.Access(Accessor.Property);
                m.Column(("id_pedido"));
                m.NotNullable(true);
            });

            Property(x => x.Valor, m =>
            {
                m.Access(Accessor.Field);
                m.Column("valor");
                m.NotNullable(true);
            });

            Property(x => x.Desconto, m =>
            {
                m.Access(Accessor.Field); // Acessa campo m_Desconto
                m.Column("desconto");
                m.NotNullable(true);
            });

            Property(x => x.ValorPago, m =>
            {
                m.Access(Accessor.Property);
                m.Column("valor_pago");
            });

            Property(x => x.DataPago, m =>
            {
                m.Access(Accessor.Property);
                m.Column("data_pago");
            });

            Property(x => x.DataRegistro, m =>
            {
                m.Access(Accessor.Property);
                m.Column("data_registro_pagamento");
                m.NotNullable(true);
            });

            Property(x => x.MeioPagamento, m =>
            {
                m.Access(Accessor.Property);
                m.Column("meio_pagamento");
                m.Type<EnumGeneric<EnumMeioPagamento>>();
            });

            Property(x => x.SituacaoPagamento, m =>
            {
                m.Access(Accessor.Property);
                m.Column("situacao_pagamento");
                m.NotNullable(true);
                m.Type<EnumGeneric<EnumSituacaoPagamento>>();
            });

            Property(x => x.NumeroParcelas, m =>
            {
                m.Access(Accessor.Field); // Acessa campo m_NumeroParcelas
                m.Column("numero_parcelas");
            });

            Bag(x => x.Logs, m =>
            {
                m.Access(Accessor.Field);
                m.Key(k => k.Column("id_pagamento")); 
                m.Cascade(Cascade.All);
            }, r => r.OneToMany());
        }
    }
}
