using EventoWeb.Comum.Negocio.Entidades;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace EventoWeb.Comum.Persistencia.Mapeamentos
{
    public class PagamentoLogMapping : ClassMapping<PagamentoLog>
    {
        public PagamentoLogMapping()
        {
            Table("pagamentos_logs");

            Id(x => x.Id, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("id");
                m.Generator(Generators.Native, g =>
                {
                    g.Params(new { sequence = "gen_pagamento_log" });
                });
            }); 
            
            ManyToOne(x=>x.Pagamento, m =>
            {
                m.Column("id_pagamento");
                m.NotNullable(true);
            });
            
            Property(x => x.Tipo, m =>
            {
                m.Access(Accessor.Property);
                m.Column("tipo");
                m.NotNullable(true);
                m.Type<EnumGeneric<EnumTipoPagamentoLog>>();
            });

            Property(x => x.Data, m =>
            {
                m.Access(Accessor.Property);
                m.Column("data");
                m.NotNullable(true);
            });
            
            Property(x => x.Mensagem, m =>
            {
                m.Access(Accessor.Property);
                m.Column("mensagem");
                m.Length(500);
            });            

            Property(x => x.Dados, m =>
            {
                m.Access(Accessor.Property);
                m.Column("dados");
                m.Length(4000);
            });
        }
    }
}
