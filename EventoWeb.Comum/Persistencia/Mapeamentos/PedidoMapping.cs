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

            Property(x => x.Tipo, m =>
            {
                m.Access(Accessor.Property);
                m.Column("tipo");
                m.NotNullable(true);
                m.Type<EnumGeneric<EnumTipoPedido>>();
            });
            
            ManyToOne(x => x.Pagador, m =>
            {
                m.Access(Accessor.Property);
                m.Column("id_pessoa_pagadora");
                m.NotNullable(true);
                m.Cascade(Cascade.All);
            });

            ManyToOne(x => x.Conta, m =>
            {
                m.Access(Accessor.Property);
                m.Column("id_conta");
                m.NotNullable(true);
                m.Cascade(Cascade.All | Cascade.DeleteOrphans);
            });

            ManyToOne(x => x.FormaPagamento, m =>
            {
                m.Access(Accessor.Property);
                m.Column("id_forma_pagamento");
                m.NotNullable(false);
            });

            Bag(x => x.Inscricoes, m =>
            {
                m.Access(Accessor.Field);
                m.Key(k => k.Column("id_pedido"));
                m.Table("pedidos_inscricoes");
                m.Inverse(false); 
                m.Cascade(Cascade.All | Cascade.DeleteOrphans);
            }, r => r.ManyToMany(x =>
            {
                x.Column("id_inscricao");
            }));
        }
    }
}
