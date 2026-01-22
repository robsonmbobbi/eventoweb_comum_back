using EventoWeb.Comum.Negocio.Entidades;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace EventoWeb.Comum.Persistencia.Mapeamentos
{
    public class PagamentoMapping : ClassMapping<Pagamento>
    {
        public PagamentoMapping()
        {
            Table("PAGAMENTOS");

            Id(x => x.Id, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("ID");
                m.Generator(Generators.Native, g =>
                {
                    g.Params(new { sequence = "GEN_PAGAMENTO" });
                });
            });            
            
            ManyToOne(x => x.Pedido, m =>
            {
                m.Access(Accessor.Property);
                m.Column(("ID_PEDIDO"));
                m.NotNullable(true);
            });

            Property(x => x.Valor, m =>
            {
                m.Access(Accessor.Field);
                m.Column("VALOR");
                m.NotNullable(true);
            });

            Property(x => x.Desconto, m =>
            {
                m.Access(Accessor.Field); // Acessa campo m_Desconto
                m.Column("DESCONTO");
                m.NotNullable(true);
            });

            Property(x => x.ValorPago, m =>
            {
                m.Access(Accessor.Property);
                m.Column("VALOR_PAGO");
            });

            Property(x => x.DataPago, m =>
            {
                m.Access(Accessor.Property);
                m.Column("DATA_PAGO");
            });

            Property(x => x.DataRegistro, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("DATA_REGISTRO_PAGAMENTO");
                m.NotNullable(true);
            });

            Property(x => x.MeioPagamento, m =>
            {
                m.Access(Accessor.Property);
                m.Column("MEIO_PAGAMENTO");
                m.Type<EnumGeneric<EnumMeioPagamento>>();
            });

            Property(x => x.SituacaoPagamento, m =>
            {
                m.Access(Accessor.Property);
                m.Column("SITUACAO_PAGAMENTO");
                m.NotNullable(true);
                m.Type<EnumGeneric<EnumSituacaoPagamento>>();
            });

            Property(x => x.NumeroParcelas, m =>
            {
                m.Access(Accessor.Field); // Acessa campo m_NumeroParcelas
                m.Column("NUMERO_PARCELAS");
            });

            Bag(x => x.Logs, m =>
            {
                m.Access(Accessor.Field);
                m.Key(k => k.Column("ID_PAGAMENTO")); 
                m.Cascade(Cascade.All);
            }, r => r.OneToMany());
        }
    }
}
