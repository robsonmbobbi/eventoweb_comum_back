using EventoWeb.Comum.Negocio.Entidades;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace EventoWeb.Comum.Persistencia.Mapeamentos
{
    public class PedidoMapping : ClassMapping<Pedido>
    {
        public PedidoMapping()
        {
            Table("pedidos");

            Id(x => x.Id, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("id");
                m.Generator(Generators.Native, g =>
                {
                    g.Params(new { sequence = "gen_pedido" });
                });
            });

            Property(x => x.Valor, m =>
            {
                m.Access(Accessor.Property);
                m.Column("valor");
                m.NotNullable(true);
            });

            Property(x => x.FormaPagamento, m =>
            {
                m.Access(Accessor.Property);
                m.Column("forma_pagamento");
                m.NotNullable(true);
                m.Type<EnumGeneric<EnumFormaPagamento>>();
            });
            
            ManyToOne(x => x.Pagador, m =>
            {
                m.Access(Accessor.Property);
                m.Column("id_pessoa_pagadora");
                m.NotNullable(true);
                m.Cascade(Cascade.All | Cascade.DeleteOrphans);
            });

            OneToOne(x => x.Pagamento, m =>
            {
                m.Access(Accessor.Property);
                m.Cascade(Cascade.All);
            });

            Bag(x => x.Inscricoes, m =>
            {
                m.Access(Accessor.Field);
                m.Key(k => k.Column("id_pedido"));
                m.Table("pedidos_inscricoes");
                m.Inverse(false); 
                m.Cascade(Cascade.All);
            }, r => r.ManyToMany(x =>
            {
                x.Column("id_inscricao");
            }));
        }
    }
}
