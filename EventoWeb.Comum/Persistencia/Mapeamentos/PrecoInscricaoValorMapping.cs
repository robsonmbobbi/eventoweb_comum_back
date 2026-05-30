using EventoWeb.Comum.Negocio.Entidades;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace EventoWeb.Comum.Persistencia.Mapeamentos
{
    public class PrecoInscricaoValorMapping : ClassMapping<PrecoInscricaoValor>
    {
        public PrecoInscricaoValorMapping()
        {
            Table("preco_inscricao_valores");

            Id(x => x.Id, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("id");
                m.Generator(Generators.Native, g =>
                {
                    g.Params(new { sequence = "gen_preco_inscricao_valor" });
                });
            }); 
            
            ManyToOne(x=>x.Preco, m =>
            {
                m.Column("id_preco_inscricao");
                m.NotNullable(true);
            });
            
            ManyToOne(x=>x.Forma, m =>
            {
                m.Column("id_forma_pagamento");
                m.NotNullable(true);
            });

            Component(x => x.Valor, m =>
            {
                m.Access(Accessor.NoSetter);

                m.Property(c => c.Valor, y =>
                {
                    y.Access(Accessor.Property);
                    y.Column("valor");
                    y.NotNullable(true);
                });
            });
        }
    }
}
