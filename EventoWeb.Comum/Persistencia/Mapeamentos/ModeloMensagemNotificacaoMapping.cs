using EventoWeb.Comum.Negocio.Entidades.Notificacoes;
using NHibernate;
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

            Component(x => x.Assunto, c =>
            {
                c.Access(Accessor.Property);
                c.Property(y => y.Valor, m =>
                {
                    m.Access(Accessor.NoSetter);
                    m.Column("assunto");
                    m.Length(200);
                    m.NotNullable(false);
                });
            });

            Component(x => x.Nome, c =>
            {
                c.Access(Accessor.Property);
                c.Property(y => y.Valor, m =>
                {
                    m.Access(Accessor.NoSetter);
                    m.Column("nome");
                    m.Length(200);
                    m.NotNullable(true);
                });
            });

            Component(x => x.Mensagem, c =>
            {
                c.Access(Accessor.Property);
                c.Property(o => o.Valor, m => {
                    m.Access(Accessor.NoSetter);
                    m.NotNullable(true);
                    m.Column("mensagem");
                    m.Type(NHibernateUtil.StringClob);
                });
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
