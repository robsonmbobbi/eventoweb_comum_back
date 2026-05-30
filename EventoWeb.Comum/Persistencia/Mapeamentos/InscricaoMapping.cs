using EventoWeb.Comum.Negocio.Entidades;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace EventoWeb.Comum.Persistencia.Mapeamentos
{
    public class InscricaoMapping: ClassMapping<Inscricao>
    {
        public InscricaoMapping()
        {
            Table("inscricoes");
            this.Lazy(false);

            Discriminator(d =>
            {
                d.Column("tipo_inscricao");
                d.Length(30);
            });

            Abstract(true);

            Id(x => x.Id, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("id");
                m.Generator(Generators.Native, g =>
                {
                    g.Params(new { sequence = "gen_inscricao" });
                });
            });

            Property(x => x.ConfirmadoNoEvento, m =>
              {
                  m.Access(Accessor.Property);
                  m.Column("confirmado");
                  m.NotNullable(true);
              });

            Property(x => x.DataRecebimento, m =>
              {
                  m.Access(Accessor.Property);
                  m.Column("data_recebimento");
                  m.NotNullable(true);
              });

            Property(x => x.DormeEvento, m =>
            {
                m.Access(Accessor.Property);
                m.Column("dormira");
                m.NotNullable(true);
            });
            ManyToOne(x => x.Evento, m =>
            {
                m.Access(Accessor.Property);
                m.Column("id_evento");
                m.NotNullable(true);
            });

            this.Component(x => x.NomeCracha, c =>
            {
                c.Access(Accessor.Property);
                c.Property(o => o.Valor, m =>
                {
                    m.Access(Accessor.NoSetter);
                    m.NotNullable(false);
                    m.Length(200);
                    m.Column("nome_cracha");
                });
            });

            ManyToOne(x => x.Pessoa, m =>
            {
                m.Access(Accessor.Property);
                m.Column("id_pessoa");
                m.NotNullable(true);
                m.Cascade(Cascade.All | Cascade.DeleteOrphans);
            });
            Property(x => x.Situacao, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("situacao");
                m.NotNullable(true);
                m.Type<EnumGeneric<EnumSituacaoInscricao>>();
            });

            this.Component(x => x.Observacoes, c =>
            {
                c.Access(Accessor.Property);
                c.Property(o => o.Valor, m =>
                {
                    m.Access(Accessor.NoSetter);
                    m.NotNullable(false);
                    m.Column("observacoes");
                    m.Type(NHibernateUtil.StringClob);
                });
            });
        }
    }

    public class InscricaoInfantilMapping: SubclassMapping<InscricaoInfantil>
    {
        public InscricaoInfantilMapping()
        {
            DiscriminatorValue("infantil");
            
            ManyToOne(x => x.InscricaoResponsavel1, m =>
            {
                m.Column("id_insc_responsavel_1");
                m.Access(Accessor.NoSetter);
                m.NotNullable(true);
            });

            ManyToOne(x => x.InscricaoResponsavel2, m =>
            {
                m.Column("id_insc_responsavel_2");
                m.Access(Accessor.NoSetter);
                m.NotNullable(false);
            });
        }
    }

    public class InscricaoParticipanteMapping: SubclassMapping<InscricaoParticipante>
    {
        public InscricaoParticipanteMapping()
        {
            DiscriminatorValue("part_trab");
            
            Property(x => x.Tipo, m =>
            {
                m.Column("tipo");
                m.Access(Accessor.Property);
                m.NotNullable(false);
                m.Type<EnumGeneric<EnumTipoParticipante>>();
            });

            this.Component(x => x.InstituicoesEspiritasFrequenta, c =>
            {
                c.Access(Accessor.Property);
                c.Property(o => o.Valor, m =>
                {
                    m.Access(Accessor.NoSetter);
                    m.NotNullable(false);
                    m.Column("instituicoes_espiritas_freq");
                    m.Length(200);
                });
            });
        }
    }
}
