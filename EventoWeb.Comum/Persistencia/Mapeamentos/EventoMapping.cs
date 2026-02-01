using EventoWeb.Comum.Negocio.Entidades;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace EventoWeb.Comum.Persistencia.Mapeamentos
{
    public class EventoMapping: ClassMapping<Evento>
    {
        public EventoMapping()
        {
            this.Table("eventos");
            this.Id(x => x.Id, m =>
              {
                  m.Access(Accessor.NoSetter);
                  m.Column("id");
                  m.Generator(Generators.Native, g =>
                  {
                      g.Params(new { sequence = "gen_eventos" });
                  });
              });

            this.Component(x => x.Nome, c =>
            {
                c.Access(Accessor.NoSetter);
                c.Property(o => o.Nome, m => 
                { 
                    m.Access(Accessor.NoSetter);
                    m.NotNullable(true);
                    m.Length(250);
                    m.Column("nome");
                });
            });

            this.Component(x => x.PeriodoInscricaoOnLine, c =>
             {
                 c.Access(Accessor.NoSetter);
                 c.Property(o => o.DataInicial, m => {
                     m.Access(Accessor.Property);
                     m.NotNullable(true);
                     m.Column("data_inicio_insc");
                 });
                 c.Property(o => o.DataFinal, m => {
                     m.Access(Accessor.Property);
                     m.NotNullable(true);
                     m.Column("data_fim_insc");
                 });
             });

            this.Component(x => x.PeriodoRealizacaoEvento, c =>
            {
                c.Access(Accessor.NoSetter);
                c.Property(o => o.DataInicial, m => {
                    m.Access(Accessor.Property);
                    m.NotNullable(true);
                    m.Column("data_inicio_evento");
                });
                c.Property(o => o.DataFinal, m => {
                    m.Access(Accessor.Property);
                    m.NotNullable(true);
                    m.Column("data_fim_evento");
                });
            });

            this.Property(x => x.DataRegistro, m => {
                m.Access(Accessor.NoSetter);
                m.NotNullable(true);
                m.Column("data_registro");
            });
            this.ManyToOne(x => x.Logotipo, m => {
                m.Access(Accessor.Property);
                m.Cascade(Cascade.All | Cascade.DeleteOrphans);
                m.NotNullable(false);
                m.Column("id_arquivo_logotipo");
            });
            
            this.Property(x => x.IdadeMinimaInscricaoAdulto, m =>
             {
                 m.Access(Accessor.Property);
                 m.NotNullable(true);
                 m.Column("idade_minima_insc_adulto");
             });
            
            this.Property(x => x.Regulamento, m =>
            {
                m.Access(Accessor.Property);
                m.NotNullable(false);
                m.Column("regulamento");
                m.Type(NHibernateUtil.StringClob);
            });
        }
    }
}
