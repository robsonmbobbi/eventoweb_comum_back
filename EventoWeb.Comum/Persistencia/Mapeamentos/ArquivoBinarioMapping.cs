using EventoWeb.Comum.Negocio.Entidades;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace EventoWeb.Comum.Persistencia.Mapeamentos
{
    public class ArquivoBinarioMapping : ClassMapping<ArquivoBinario>
    {
        public ArquivoBinarioMapping()
        {
            Table("arquivos_binarios");
            Id(x => x.Id, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("id");
                m.Generator(Generators.Native, g =>
                {
                    g.Params(new { sequence = "gen_arquivo_binario" });
                });
            });

            Property(x => x.Arquivo, m =>
            {
                m.Access(Accessor.NoSetter);
                m.NotNullable(true);
                m.Column("arquivo");
                m.Type(NHibernateUtil.BinaryBlob);
            });

            Property(x => x.Tipo, m =>
            {
                m.Access(Accessor.NoSetter);
                m.NotNullable(true);
                m.Column("tipo_arquivo");
                m.Type<EnumGeneric<EnumTipoArquivoBinario>>();
            });
        }
    }
}
