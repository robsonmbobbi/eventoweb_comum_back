using EventoWeb.Comum.Negocio.Entidades.Notificacoes;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace EventoWeb.Comum.Persistencia.Mapeamentos
{
    public class ModeloMensagemNotificacaoMapping : ClassMapping<ModeloMensagemNotificacao>
    {
        public ModeloMensagemNotificacaoMapping()
        {
            Table("modelos_mensagem_notificacao");
            Id(x => x.Id, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("id");
                m.Generator(Generators.Native, g =>
                {
                    g.Params(new { sequence = "gen_modelo_mensagem_notificacao" });
                });
            });

            ManyToOne(x => x.Evento, m =>
            {
                m.Access(Accessor.Property);
                m.Column("id_evento");
                m.NotNullable(true);
            });

            Property(x => x.Meio, m =>
            {
                m.Access(Accessor.Property);
                m.Column("meio");
                m.Type<EnumGeneric<EnumMeioNotificacao>>();
                m.NotNullable(true);
            });

            Property(x => x.Tipo, m =>
            {
                m.Access(Accessor.Property);
                m.Column("tipo");
                m.Type<EnumGeneric<EnumTipoNotificacao>>();
                m.NotNullable(true);
            });

            Property(x => x.Assunto, m =>
            {
                m.Column("assunto");
                m.Length(600);
                m.Access(Accessor.Property);
                m.NotNullable(false);
            });

            Component(x => x.Nome, c =>
            {
                c.Access(Accessor.Property);
                c.Property(y => y.Nome, m =>
                {
                    m.Access(Accessor.NoSetter);
                    m.Column("nome");
                    m.Length(200);
                    m.NotNullable(true);
                });
            });

            Property(x => x.Mensagem, m =>
            {
                m.Column("mensagem");
                m.Access(Accessor.Property);
                m.NotNullable(true);
            });

            Property(x => x.Ativo, m =>
            {
                m.Column("ativo");
                m.Access(Accessor.Property);
                m.NotNullable(true);
            });
        }
    }
}
