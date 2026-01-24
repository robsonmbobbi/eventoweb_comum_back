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
                m.Column("ID");
                m.Generator(Generators.Native, g =>
                {
                    g.Params(new { sequence = "GEN_PEDIDO" });
                });
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
            
            ManyToOne(x => x.Pagador, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("ID_PESSOA_PAGADORA");
                m.NotNullable(true);
                m.Cascade(Cascade.All | Cascade.DeleteOrphans);
            });

            OneToOne(x => x.Pagamento, m =>
            {
                m.Access(Accessor.Property);
            });

            Bag(x => x.Inscricoes, m =>
            {
                m.Access(Accessor.Field);
                m.Key(k => k.Column("ID_PEDIDO"));
                m.Table("PEDIDOS_INSCRICOES");
                m.Inverse(false); 
                m.Cascade(Cascade.All);
            }, r => r.ManyToMany(x =>
            {
                x.Column("ID_INSCRICAO");
            }));
        }
    }
}
