using EventoWeb.Comum.Negocio.Entidades;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace EventoWeb.Comum.Persistencia.Mapeamentos
{
    public class PedidoMapping : ClassMapping<Pedido>
    {
        public PedidoMapping()
        {
            Table("PEDIDOS");

            Id(x => x.Id, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("ID_PEDIDO");
                m.Generator(Generators.Native, g => g.Params(new { sequence = "GEN_PEDIDOS" }));
            });

            Property(x => x.Valor, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("VALOR");
                m.NotNullable(true);
            });

            Property(x => x.FormaPagamento, m =>
            {
                m.Access(Accessor.Property);
                m.Column("FORMA_PAGAMENTO");
                m.NotNullable(true);
                m.Type<EnumGeneric<EnumFormaPagamento>>();
            });

            OneToOne(x => x.Pagamento, m =>
            {
                m.Access(Accessor.Property);
            });

            Bag(x => x.Inscricoes, m =>
            {
                m.Access(Accessor.Field);
                m.Key(k => k.Column("ID_PEDIDO"));
                m.Inverse(false); // Pedido gerencia a relação
                m.Cascade(Cascade.All);
            }, r => r.OneToMany());
        }
    }
}
