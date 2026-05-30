using EventoWeb.Comum.Negocio.Entidades.Notificacoes;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace EventoWeb.Comum.Persistencia.Mapeamentos
{
    public class MensagemNotificacaoMapping : ClassMapping<MensagemNotificacao>
    {
        public MensagemNotificacaoMapping()
        {
            Table("mensagens_notificacao");
            Id(x => x.Id, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("id");
                m.Generator(Generators.Native, g =>
                {
                    g.Params(new { sequence = "gen_mensagem_notificacao" });
                });
            });
            
            ManyToOne(x => x.Modelo, m =>
            {
                m.Access(Accessor.Property);
                m.Column("id_modelo");
                m.NotNullable(true);
            });
            
            Component(x => x.Destinatario, c =>
            {
                c.Access(Accessor.Property);
                c.Property(o => o.Valor, m => {
                    m.Access(Accessor.NoSetter);
                    m.NotNullable(true);
                    m.Column("destinatario");
                    m.Length(500);
                });
            });

            this.Component(x => x.VariaveisJson, c =>
            {
                c.Access(Accessor.Property);
                c.Property(o => o.Valor, m => {
                    m.Access(Accessor.NoSetter);
                    m.NotNullable(false);
                    m.Column("variaveis_json");
                    m.Type(NHibernateUtil.StringClob);
                });
            });

            Property(x => x.Situacao, m =>
            {
                m.Access(Accessor.Property);
                m.Column("situacao");
                m.Type<EnumGeneric<EnumSituacaoEnvioNotificacao>>();
                m.NotNullable(true);
            });
            
            Property(x => x.DataSituacao, m =>
            {
                m.Access(Accessor.Property);
                m.Column("data_situacao");
                m.NotNullable(false);
            });

            this.Component(x => x.Erro, c =>
            {
                c.Access(Accessor.Property);
                c.Property(o => o.Valor, m => {
                    m.Access(Accessor.NoSetter);
                    m.NotNullable(false);
                    m.Column("erro");
                    m.Type(NHibernateUtil.StringClob);
                });
            });
        }
    }
}
