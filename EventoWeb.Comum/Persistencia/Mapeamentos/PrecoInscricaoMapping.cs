using EventoWeb.Comum.Negocio.Entidades;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace EventoWeb.Comum.Persistencia.Mapeamentos
{
    public class PrecoInscricaoMapping : ClassMapping<PrecoInscricao>
    {
        public PrecoInscricaoMapping()
        {
            Table("precos_inscricao");

            Id(x => x.Id, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("id");
                m.Generator(Generators.Native, g =>
                {
                    g.Params(new { sequence = "gen_preco_inscricao" });
                });
            });            
            
            ManyToOne(x => x.Evento, m =>
            {
                m.Access(Accessor.Property);
                m.Column(("id_evento"));
                m.NotNullable(true);
            });

            Component(x => x.IdadeMax, c =>
            {
                c.Access(Accessor.Field);
                c.Property(o => o.Valor, m =>
                {
                    m.Access(Accessor.NoSetter);
                    m.NotNullable(false);
                    m.Column("idade_max");
                    m.NotNullable(true);
                });                
            });

            Bag(x => x.Valores, m =>
            {
                m.Access(Accessor.Field);
                m.Key(k => k.Column("id_preco_inscricao")); 
                m.Cascade(Cascade.All);
            }, r => r.OneToMany());
        }
    }
}
