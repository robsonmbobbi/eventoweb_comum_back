using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Entidades.Financeiro;
using EventoWeb.Comum.Negocio.ObjetosValor;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace EventoWeb.Comum.Persistencia.Mapeamentos;

public class FormaPagamentoMapping : ClassMapping<FormaPagamento>
{
    public FormaPagamentoMapping()
    {
        Table("formas_pagamento");

        Id(x => x.Id, m =>
        {
            m.Access(Accessor.NoSetter);
            m.Column("id");
            m.Generator(Generators.Native, g =>
            {
                g.Params(new { sequence = "gen_forma_pagamento" });
            });
        }); 

        this.Component(x => x.Nome, c =>
        {
            c.Access(Accessor.NoSetter);
            c.Property(y=> y.Nome, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("nome");
                m.Length(150);
                m.NotNullable(true);
            });
        });

        Property(m => m.Tipo, m =>
        {
            m.Access(Accessor.Property);
            m.Column("TIPO");
            m.NotNullable(true);
            m.Type<EnumGeneric<EnumTipoPagamento>>();
        });

        this.Component(x => x.Parcelas, c =>
        {
            c.Access(Accessor.NoSetter);
            c.Property(y => y.Minimo, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("NR_PARCELAS_MINIMA");
                m.NotNullable(true);
            });
            c.Property(y => y.Maximo, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("NR_PARCELAS_MAXIMA");
                m.NotNullable(true);
            });
        });
    }
}