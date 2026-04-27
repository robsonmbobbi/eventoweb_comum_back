using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using EventoWeb.Comum.Negocio.Entidades.Financeiro;
using EventoWeb.Comum.Persistencia.Mapeamentos;

namespace EventoWeb.Comum.Infraestrutura.Mapeamentos.Financeiro
{
    public class TransacaoMapping : ClassMapping<Transacao>
    {
        public TransacaoMapping()
        {
            Table("transacoes");

            Id(x => x.Id, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("id");
                m.Generator(Generators.Native, g =>
                {
                    g.Params(new { sequence = "gen_transacao" });
                });
            });

            ManyToOne(x => x.ContaBancaria, m =>
            {
                m.Column("id_conta_bancaria");
                m.NotNullable(true);
            });

            Property(x => x.DataHora, m =>
            {
                m.Column("data_hora");
                m.NotNullable(true);
            });

            Property(x => x.Descricao, m => 
            { 
                m.Column("descricao"); 
                m.Length(200); 
                m.NotNullable(false); 
            });

            Property(x => x.Tipo, m =>
            {
                m.Access(Accessor.Property);
                m.Column("tipo");
                m.NotNullable(true);
                m.Type<EnumGeneric<EnumTipoTransacao>>();
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
        }
    }
}