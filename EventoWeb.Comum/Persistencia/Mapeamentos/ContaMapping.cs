using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Entidades.Financeiro;
using EventoWeb.Comum.Negocio.ObjetosValor;
using EventoWeb.Comum.Persistencia.Mapeamentos;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;

namespace EventoWeb.Comum.Infraestrutura.Mapeamentos.Financeiro
{
    public class ContaMapping : ClassMapping<Conta>
    {
        public ContaMapping()
        {
            Table("contas");

            Id(x => x.Id, m =>
            {
                m.Access(Accessor.NoSetter);
                m.Column("id");
                m.Generator(Generators.Native, g =>
                {
                    g.Params(new { sequence = "gen_conta" });
                });
            });

            ManyToOne(x => x.Pessoa, m =>
            {
                m.Column("id_pessoa");
                m.NotNullable(true);
            });

            Component(x => x.Valor, c =>
            {
                c.Property(v => v.Valor, pm => 
                {
                    pm.Access(Accessor.Property);
                    pm.Column("valor");
                    pm.NotNullable(true);
                });
            });

            Property(x => x.Liquidado, m =>
            {
                m.Column("liquidado"); 
                m.NotNullable(true);
            });

            Property(x => x.DataCriado, m =>
            {
                m.Column("data_criado");
                m.NotNullable(true);
            });
            
            Property(x => x.Descricao, m => 
            { 
                m.Column("descricao"); 
                m.Length(200); 
                m.NotNullable(false); 
            });
            
            Property(x => x.DataVencimento, m => 
            {
                m.Column("data_vencimento"); 
                m.NotNullable(true);
            });
            
            Property(x => x.Tipo, m => 
            {
                m.Access(Accessor.Property);
                m.Column("tipo");
                m.NotNullable(true);
                m.Type<EnumGeneric<EnumTipoTransacao>>();
            });

            Property(x => x.ValorTotalTransacoes, m => 
            { 
                m.Column("valor_total_transacoes");
                m.NotNullable(true);
            });
            
            Property(x => x.ValorTotalDesconto, m => 
            { 
                m.Column("valor_total_desconto");
                m.NotNullable(true);
            });
            
            Property(x => x.ValorTotalJuros, m =>
            {
                m.Column("valor_total_juros");
                m.NotNullable(true);
            });
            
            Property(x => x.ValorTotalMulta, m =>
            {
                m.Column("valor_total_multa");
                m.NotNullable(true);
            });

            Bag(x => x.Transacoes, c =>
            {
                c.Table("transacoes_conta");
                c.Key(k => k.Column("id_conta"));
                c.Cascade(Cascade.All | Cascade.DeleteOrphans);
                c.Inverse(true);
                c.Access(Accessor.NoSetter);
            }, r => r.OneToMany());
        }
    }
}