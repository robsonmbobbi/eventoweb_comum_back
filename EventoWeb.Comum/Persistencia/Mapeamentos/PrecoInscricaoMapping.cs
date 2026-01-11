using EventoWeb.Comum.Negocio.Entidades;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace EventoWeb.Comum.Persistencia.Mapeamentos
{
    public class PrecoInscricaoMapping : ClassMapping<PrecoInscricao>
    {
        public PrecoInscricaoMapping()
        {
            Table("PRECOS_INSCRICAO");

            Id(x => x.Id, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("ID");
                m.Generator(Generators.Native, g =>
                {
                    g.Params(new { sequence = "GEN_PRECO_INSCRICAO" });
                });
            });            
            
            ManyToOne(x => x.Evento, m =>
            {
                m.Access(Accessor.Property);
                m.Column(("ID_EVENTO"));
                m.NotNullable(true);
            });

            Property(x => x.IdadeMax, m =>
            {
                m.Access(Accessor.Field);
                m.Column("IDADE_MAX");
                m.NotNullable(true);
            });

            Property(x => x.Preco, m =>
            {
                m.Access(Accessor.Field); // Acessa campo m_Desconto
                m.Column("PRECO");
                m.NotNullable(true);
            });
        }
    }
}
