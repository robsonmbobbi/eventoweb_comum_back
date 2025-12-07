using EventoWeb.Comum.Negocio.Entidades;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace EventoWeb.Comum.Persistencia.Mapeamentos
{
    public class PagamentoLogMapping : ClassMapping<PagamentoLog>
    {
        public PagamentoLogMapping()
        {
            // A propriedade 'Pagamento' não é mapeada aqui com 'Parent' pois o pai da coleção é o 'Pedido' (Entidade),
            // e não o componente 'Pagamento'. NHibernate injeta a entidade pai se o tipo bater, mas aqui há incompatibilidade de tipos.

            Property(x => x.Tipo, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("TIPO");
                m.NotNullable(true);
                m.Type<EnumGeneric<EnumTipoPagamentoLog>>();
            });

            Property(x => x.Data, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("DATA");
                m.NotNullable(true);
            });

            Property(x => x.Dados, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("DADOS");
                m.Length(4000); // Tamanho estimado para dados de log
            });
        }
    }
}
