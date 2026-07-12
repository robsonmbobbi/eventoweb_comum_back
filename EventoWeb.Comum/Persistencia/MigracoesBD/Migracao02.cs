using FluentMigrator;

namespace EventoWeb.Comum.Persistencia.MigracoesBD
{
    [Migration(02)]
    public class Migracao02 : Migration
    {
        public override void Down()
        {
            RemoverForeignKeysEventos();
            RemoverColunaEvento("pedidos");
            RemoverColunaEvento("contas");
            RemoverColunaEvento("transacoes");
            RemoverColunaEvento("transacoes_conta");
            RemoverColunaEvento("contas_bancarias");
            RemoverColunaEvento("integradores_financeiros");
            RemoverColunaEvento("integracao_financeira_formas_pags");
            RemoverColunaEvento("registros_integracao_financeira");
        }

        public override void Up()
        {
            AdicionarColunaEvento("pedidos");
            AdicionarColunaEvento("contas");
            AdicionarColunaEvento("transacoes");
            AdicionarColunaEvento("transacoes_conta");
            AdicionarColunaEvento("contas_bancarias");
            AdicionarColunaEvento("integradores_financeiros");
            AdicionarColunaEvento("integracao_financeira_formas_pags");
            AdicionarColunaEvento("registros_integracao_financeira");

            PopularEventosParaPedidos();
            PopularEventosParaContas();
            PopularEventosParaTransacoes();
            PopularEventosParaTransacoesConta();
            PopularEventosParaContasBancarias();
            PopularEventosParaIntegradoresFinanceiros();
            PopularEventosParaIntegracoesFinanceirasPorFormaPag();
            PopularEventosParaRegistrosIntegracaoFinanceira();

            CriarForeignKeysEventos();
        }

        private void AdicionarColunaEvento(string tabela)
        {
            Alter.Table(tabela)
                .AddColumn("id_evento")
                .AsInt32()
                .Nullable();
        }

        private void RemoverColunaEvento(string tabela)
        {
            Delete.Column("id_evento").FromTable(tabela);
        }

        private void PopularEventosParaPedidos()
        {
            Execute.Sql(@"
                UPDATE pedidos
                SET id_evento = COALESCE(
                    (
                        SELECT inscricoes.id_evento
                        FROM pedidos_inscricoes
                        JOIN inscricoes ON inscricoes.id = pedidos_inscricoes.id_inscricao
                        WHERE pedidos_inscricoes.id_pedido = pedidos.id
                        LIMIT 1
                    ),
                    (SELECT MIN(id) FROM eventos)
                )
                WHERE id_evento IS NULL;
            ");
        }

        private void PopularEventosParaContas()
        {
            Execute.Sql(@"
                UPDATE contas
                SET id_evento = COALESCE(
                    (
                        SELECT pedidos.id_evento
                        FROM pedidos
                        WHERE pedidos.id_conta = contas.id
                        LIMIT 1
                    ),
                    (SELECT MIN(id) FROM eventos)
                )
                WHERE id_evento IS NULL;
            ");
        }

        private void PopularEventosParaTransacoes()
        {
            Execute.Sql(@"
                UPDATE transacoes
                SET id_evento = COALESCE(
                    (
                        SELECT contas_bancarias.id_evento
                        FROM contas_bancarias
                        WHERE contas_bancarias.id = transacoes.id_conta_bancaria
                        LIMIT 1
                    ),
                    (SELECT MIN(id) FROM eventos)
                )
                WHERE id_evento IS NULL;
            ");
        }

        private void PopularEventosParaTransacoesConta()
        {
            Execute.Sql(@"
                UPDATE transacoes_conta
                SET id_evento = COALESCE(
                    (
                        SELECT contas.id_evento
                        FROM contas
                        WHERE contas.id = transacoes_conta.id_conta
                        LIMIT 1
                    ),
                    (SELECT MIN(id) FROM eventos)
                )
                WHERE id_evento IS NULL;
            ");
        }

        private void PopularEventosParaContasBancarias()
        {
            Execute.Sql(@"
                UPDATE contas_bancarias
                SET id_evento = COALESCE(id_evento, (SELECT MIN(id) FROM eventos))
                WHERE id_evento IS NULL;
            ");
        }

        private void PopularEventosParaIntegradoresFinanceiros()
        {
            Execute.Sql(@"
                UPDATE integradores_financeiros
                SET id_evento = COALESCE(
                    (
                        SELECT contas_bancarias.id_evento
                        FROM contas_bancarias
                        WHERE contas_bancarias.id = integradores_financeiros.id_conta_bancaria
                        LIMIT 1
                    ),
                    (SELECT MIN(id) FROM eventos)
                )
                WHERE id_evento IS NULL;
            ");
        }

        private void PopularEventosParaIntegracoesFinanceirasPorFormaPag()
        {
            Execute.Sql(@"
                UPDATE integracao_financeira_formas_pags
                SET id_evento = COALESCE(
                    (
                        SELECT integradores_financeiros.id_evento
                        FROM integradores_financeiros
                        WHERE integradores_financeiros.id = integracao_financeira_formas_pags.id_integrador_financeiro
                        LIMIT 1
                    ),
                    (SELECT MIN(id) FROM eventos)
                )
                WHERE id_evento IS NULL;
            ");
        }

        private void PopularEventosParaRegistrosIntegracaoFinanceira()
        {
            Execute.Sql(@"
                UPDATE registros_integracao_financeira
                SET id_evento = COALESCE(
                    (
                        SELECT contas.id_evento
                        FROM contas
                        WHERE contas.id = registros_integracao_financeira.id_conta
                        LIMIT 1
                    ),
                    (
                        SELECT integradores_financeiros.id_evento
                        FROM integradores_financeiros
                        WHERE integradores_financeiros.id = registros_integracao_financeira.id_integrador_financeiro
                        LIMIT 1
                    ),
                    (SELECT MIN(id) FROM eventos)
                )
                WHERE id_evento IS NULL;
            ");
        }

        private void CriarForeignKeysEventos()
        {
            Create.ForeignKey("fk_pedido_evento")
                .FromTable("pedidos").ForeignColumn("id_evento")
                .ToTable("eventos").PrimaryColumn("id")
                .OnUpdate(Rule.Cascade);

            Create.ForeignKey("fk_conta_evento")
                .FromTable("contas").ForeignColumn("id_evento")
                .ToTable("eventos").PrimaryColumn("id")
                .OnUpdate(Rule.Cascade);

            Create.ForeignKey("fk_transacao_evento")
                .FromTable("transacoes").ForeignColumn("id_evento")
                .ToTable("eventos").PrimaryColumn("id")
                .OnUpdate(Rule.Cascade);

            Create.ForeignKey("fk_transacao_conta_evento")
                .FromTable("transacoes_conta").ForeignColumn("id_evento")
                .ToTable("eventos").PrimaryColumn("id")
                .OnUpdate(Rule.Cascade);

            Create.ForeignKey("fk_conta_bancaria_evento")
                .FromTable("contas_bancarias").ForeignColumn("id_evento")
                .ToTable("eventos").PrimaryColumn("id")
                .OnUpdate(Rule.Cascade);

            Create.ForeignKey("fk_integrador_financeiro_evento")
                .FromTable("integradores_financeiros").ForeignColumn("id_evento")
                .ToTable("eventos").PrimaryColumn("id")
                .OnUpdate(Rule.Cascade);

            Create.ForeignKey("fk_integracao_financeira_forma_pag_evento")
                .FromTable("integracao_financeira_formas_pags").ForeignColumn("id_evento")
                .ToTable("eventos").PrimaryColumn("id")
                .OnUpdate(Rule.Cascade);

            Create.ForeignKey("fk_registro_integracao_financeira_evento")
                .FromTable("registros_integracao_financeira").ForeignColumn("id_evento")
                .ToTable("eventos").PrimaryColumn("id")
                .OnUpdate(Rule.Cascade);
        }

        private void RemoverForeignKeysEventos()
        {
            Delete.ForeignKey("fk_pedido_evento").OnTable("pedidos");
            Delete.ForeignKey("fk_conta_evento").OnTable("contas");
            Delete.ForeignKey("fk_transacao_evento").OnTable("transacoes");
            Delete.ForeignKey("fk_transacao_conta_evento").OnTable("transacoes_conta");
            Delete.ForeignKey("fk_conta_bancaria_evento").OnTable("contas_bancarias");
            Delete.ForeignKey("fk_integrador_financeiro_evento").OnTable("integradores_financeiros");
            Delete.ForeignKey("fk_integracao_financeira_forma_pag_evento").OnTable("integracao_financeira_formas_pags");
            Delete.ForeignKey("fk_registro_integracao_financeira_evento").OnTable("registros_integracao_financeira");
        }
    }
}
