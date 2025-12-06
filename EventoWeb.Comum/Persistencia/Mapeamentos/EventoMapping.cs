using EventoWeb.Comum.Negocio.Entidades;
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
                  m.Column("ID_EVENTO");
                  m.Generator(Generators.Native, g =>
                  {
                      g.Params(new { sequence = "GEN_EVENTOS" });
                  });
              });

            this.Component(x => x.Nome, c =>
            {
                c.Access(Accessor.NoSetter);
                c.Property(o => o.Nome, m => 
                { 
                    m.NotNullable(true);
                    m.Length(250);
                    m.Column("NOME");
                });
            });

            this.Component(x => x.PeriodoInscricaoOnLine, c =>
             {
                 c.Access(Accessor.NoSetter);
                 c.Property(o => o.DataInicial, m => {
                     m.Access(Accessor.Property);
                     m.NotNullable(true);
                     m.Column("DATA_INICIO_INSC");
                 });
                 c.Property(o => o.DataFinal, m => {
                     m.Access(Accessor.Property);
                     m.NotNullable(true);
                     m.Column("DATA_FIM_INSC");
                 });
             });

            this.Component(x => x.PeriodoRealizacaoEvento, c =>
            {
                c.Access(Accessor.NoSetter);
                c.Property(o => o.DataInicial, m => {
                    m.Access(Accessor.Property);
                    m.NotNullable(true);
                    m.Column("DATA_INICIO_EVENTO");
                });
                c.Property(o => o.DataFinal, m => {
                    m.Access(Accessor.Property);
                    m.NotNullable(true);
                    m.Column("DATA_FIM_EVENTO");
                });
            });

            this.Property(x => x.DataRegistro, m => {
                m.Access(Accessor.NoSetter);
                m.NotNullable(true);
                m.Column("DATA_REGISTRO");
            });
            this.ManyToOne(x => x.Logotipo, m => {
                m.Access(Accessor.Property);
                m.Cascade(Cascade.All | Cascade.DeleteOrphans);
                m.NotNullable(false);
                m.Column("ID_ARQUIVO_LOGOTIPO");
            });
            
            this.Property(x => x.IdadeMinimaInscricaoAdulto, m =>
             {
                 m.Access(Accessor.NoSetter);
                 m.NotNullable(true);
                 m.Column("IDADE_MINIMA_INSC_ADULTO");
             });
        }
    }
}
