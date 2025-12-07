using EventoWeb.Comum.Negocio.Entidades;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace EventoWeb.Comum.Persistencia.Mapeamentos
{
    public class PagamentoMapping : ClassMapping<Pagamento>
    {
        public PagamentoMapping()
        {
            OneToOne(x => x.Pedido, m =>
            {
                m.Access(Accessor.Property);
                m.Constrained(true);
            });

            Property(x => x.Valor, m =>
            {
                m.Access(Accessor.Field); // Acessa campo m_Valor para evitar validações no setter
                m.Column("PAGAMENTO_VALOR");
                m.NotNullable(true);
            });

            Property(x => x.Desconto, m =>
            {
                m.Access(Accessor.Field); // Acessa campo m_Desconto
                m.Column("PAGAMENTO_DESCONTO");
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
                m.Access(Accessor.Field); // Acessa m_Logs
                m.Table("PAGAMENTO_LOGS");
                m.Key(k => k.Column("ID_PEDIDO")); // FK aponta para a entidade pai (Pedido)
                m.Cascade(Cascade.All);
            }, r => r.Component(new PagamentoLog()));
        }
    }
}
