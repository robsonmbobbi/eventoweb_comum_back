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
                d.Column("TIPO_INSCRICAO");
                d.Length(30);
            });

            Abstract(true);

            Id(x => x.Id, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("ID_INSCRICAO");
                m.Generator(Generators.Native, g =>
                {
                    g.Params(new { sequence = "GEN_INSCRICAO" });
                });
            });

            Property(x => x.ConfirmadoNoEvento, m =>
              {
                  m.Access(Accessor.Property);
                  m.Column("CONFIRMADO");
                  m.NotNullable(true);
              });

            Property(x => x.DataRecebimento, m =>
              {
                  m.Access(Accessor.Property);
                  m.Column("DATA_RECEBIMENTO");
                  m.NotNullable(true);
              });

            Property(x => x.DormeEvento, m =>
            {
                m.Access(Accessor.Property);
                m.Column("DORMIRA");
                m.NotNullable(true);
            });
            ManyToOne(x => x.Evento, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("ID_EVENTO");
                m.NotNullable(true);
            });

            Property(x => x.NomeCracha, m =>
            {
                m.Access(Accessor.Property);
                m.Column("NOME_CRACHA");
                m.Length(150);
                m.NotNullable(false);
            });
            ManyToOne(x => x.Pessoa, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("ID_PESSOA");
                m.NotNullable(true);
                m.Cascade(Cascade.All | Cascade.DeleteOrphans);
            });
            Property(x => x.Situacao, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("SITUACAO");
                m.NotNullable(true);
                m.Type<EnumGeneric<EnumSituacaoInscricao>>();
            });

            Property(x => x.Observacoes, m =>
            {
                m.Access(Accessor.Property);
                m.Column("OBSERVACOES");
                m.NotNullable(false);
                m.Type(NHibernateUtil.StringClob);
            });
        }
    }

    public class InscricaoInfantilMapping: SubclassMapping<InscricaoInfantil>
    {
        public InscricaoInfantilMapping()
        {
            DiscriminatorValue("INFANTIL");
            
            ManyToOne(x => x.InscricaoResponsavel1, m =>
            {
                m.Column("ID_INSC_RESPONSAVEL_1");
                m.Access(Accessor.NoSetter);
                m.NotNullable(true);
            });

            ManyToOne(x => x.InscricaoResponsavel2, m =>
            {
                m.Column("ID_INSC_RESPONSAVEL_2");
                m.Access(Accessor.NoSetter);
                m.NotNullable(false);
            });
        }
    }

    public class InscricaoParticipanteMapping: SubclassMapping<InscricaoParticipante>
    {
        public InscricaoParticipanteMapping()
        {
            DiscriminatorValue("PART_TRAB");
            
            Property(x => x.Tipo, m =>
            {
                m.Column("TIPO");
                m.Access(Accessor.Property);
                m.NotNullable(false);
                m.Type<EnumGeneric<EnumTipoParticipante>>();
            });
            Property(x => x.InstituicoesEspiritasFrequenta, m =>
            {
                m.Access(Accessor.Property);
                m.Column("INSTITUICOES_ESPIRITAS_FREQ");
                m.NotNullable(false);
            });
        }
    }
}
