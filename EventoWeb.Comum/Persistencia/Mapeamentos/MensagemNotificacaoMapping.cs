using EventoWeb.Comum.Negocio.Entidades.Notificacoes;
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
            
            Property(x => x.Destinatario, m =>
            {
                m.Access(Accessor.Property);
                m.Column("destinatario");
                m.Length(500);
                m.NotNullable(true);
            });
            
            Property(x => x.VariaveisJson, m =>
            {
                m.Access(Accessor.Property);
                m.Column("variaveis_json");
                m.NotNullable(false);
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
            
            Property(x => x.Erro, m =>
            {
                m.Access(Accessor.Property);
                m.Column("erro");
                m.NotNullable(false);
            });
        }
    }
}
