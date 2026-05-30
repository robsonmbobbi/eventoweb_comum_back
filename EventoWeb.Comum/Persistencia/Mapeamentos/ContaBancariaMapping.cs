using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using EventoWeb.Comum.Negocio.Entidades.Financeiro;

namespace EventoWeb.Comum.Infraestrutura.Mapeamentos.Financeiro
{
    public class ContaBancariaMapping : ClassMapping<ContaBancaria>
    {
        public ContaBancariaMapping()
        {
            Table("contas_bancarias");

            Id(x => x.Id, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("id");
                m.Generator(Generators.Native, g =>
                {
                    g.Params(new { sequence = "gen_conta_bancaria" });
                });
            });

            Component(x => x.NomeConta, c =>
            {
                c.Access(Accessor.NoSetter);
                c.Property(o => o.Valor, m =>
                {
                    m.Access(Accessor.NoSetter);
                    m.NotNullable(true);
                    m.Length(250);
                    m.Column("nome");
                });
            });
        }
    }
}