using EventoWeb.Comum.Negocio.Entidades;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace EventoWeb.Comum.Persistencia.Mapeamentos
{
    public class PagamentoLogMapping : ClassMapping<PagamentoLog>
    {
        public PagamentoLogMapping()
        {
            Table("PAGAMENTOS_LOGS");

            Id(x => x.Id, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("ID");
                m.Generator(Generators.Native, g =>
                {
                    g.Params(new { sequence = "GEN_PAGAMENTO_LOG" });
                });
            }); 
            
            ManyToOne(x=>x.Pagamento, m =>
            {
                m.Column("ID_PAGAMENTO");
                m.NotNullable(true);
            });
            
            Property(x => x.Tipo, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("TIPO");
                m.NotNullable(true);
                m.Type<EnumGeneric<EnumTipoPagamentoLog>>();
            });

            Property(x => x.Data, m =>
            {
                m.Access(Accessor.Property);
                m.Column("DATA");
                m.NotNullable(true);
            });
            
            Property(x => x.Mensagem, m =>
            {
                m.Access(Accessor.Property);
                m.Column("MENSAGEM");
                m.Length(500);
            });            

            Property(x => x.Dados, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("DADOS");
                m.Length(4000);
            });
        }
    }
}
