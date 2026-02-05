using System.Data;
using FluentMigrator;

namespace EventoWeb.Comum.Persistencia.MigracoesBD
{
    [Migration(01)]
    public class Migracao01 : Migration
    {
        public override void Down()
        {
        }

        public override void Up()
        {
            CriarTabelaArquivoBinario();
            CriarTabelaEventos();

            CriarTabelaPessoas();
            CriarTabelaInscricoes();

            CriarTabelaFormasPagamento();
            CriarTabelaContasBancarias();
            CriarTabelaTransacoes();
            CriarTabelaContas();
            CriarTabelaTransacoesConta();


            CriarTabelaPedidos();
            CriarTabelaPedidosInscricoes();

            CriarTabelaPagamentos();
            CriarTabelaPagamentosLogs();

            CriarTabelaPrecosInscricao();
            CriarTabelaPrecosInscricaoValores();
        }       

        private void CriarTabelaArquivoBinario()
        {
            Create.Table("arquivos_binarios")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("arquivo").AsBinary().NotNullable()
                .WithColumn("tipo_arquivo").AsInt16().NotNullable();
        }

        private void CriarTabelaEventos()
        {
            Create.Table("eventos")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("nome").AsString(200).NotNullable()
                .WithColumn("data_inicio_insc").AsDateTime().NotNullable()
                .WithColumn("data_fim_insc").AsDateTime().NotNullable()
                .WithColumn("data_inicio_evento").AsDateTime().NotNullable()
                .WithColumn("data_fim_evento").AsDateTime().NotNullable()
                .WithColumn("data_registro").AsDateTime().NotNullable()
                .WithColumn("id_arquivo_logotipo").AsInt32().Nullable()
                    .ForeignKey("fk_evento_arqbin", "arquivos_binarios", "id").OnUpdate(Rule.Cascade)
                .WithColumn("idade_minima_insc_adulto").AsInt32().NotNullable()
                .WithColumn("regulamento").AsString(int.MaxValue).Nullable();
        }

        private void CriarTabelaPessoas()
        {
            Create.Table("pessoas")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()

                .WithColumn("cpf").AsString(11).Nullable()

                .WithColumn("nome").AsString(200).NotNullable()

                .WithColumn("celular").AsString(15).NotNullable()
                .WithColumn("email").AsString(100).NotNullable()

                .WithColumn("alergia_alimentos").AsString(100).Nullable()

                .WithColumn("data_nascimento").AsDate().Nullable()

                .WithColumn("eh_diabetico").AsBoolean().NotNullable()
                .WithColumn("eh_vegetariano").AsBoolean().NotNullable()

                .WithColumn("sexo").AsInt16().Nullable()
                .WithColumn("usa_adocante_diar").AsBoolean().Nullable();
        }

        private void CriarTabelaInscricoes()
        {
            Create.Table("inscricoes")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()

                .WithColumn("tipo_inscricao").AsString(30).NotNullable()

                .WithColumn("confirmado").AsBoolean().NotNullable()
                .WithColumn("data_recebimento").AsDate().NotNullable()
                .WithColumn("dormira").AsBoolean().NotNullable()

                .WithColumn("id_evento").AsInt32().NotNullable()
                    .ForeignKey("fk_insc_evento", "eventos", "id").OnUpdate(Rule.Cascade)

                .WithColumn("nome_cracha").AsString(150).Nullable()

                .WithColumn("id_pessoa").AsInt32().NotNullable()
                    .ForeignKey("fk_insc_pessoa", "pessoas", "id").OnUpdate(Rule.Cascade)

                .WithColumn("situacao").AsInt16().NotNullable()

                .WithColumn("observacoes").AsString(int.MaxValue).Nullable()

                // Subclasses (InscricaoInfantil / InscricaoParticipante)
                .WithColumn("id_insc_responsavel_1").AsInt32().Nullable()
                    .ForeignKey("fk_insc_inf_1", "inscricoes", "id").OnUpdate(Rule.Cascade)
                .WithColumn("id_insc_responsavel_2").AsInt32().Nullable()
                    .ForeignKey("fk_insc_inf_2", "inscricoes", "id").OnUpdate(Rule.Cascade)

                .WithColumn("tipo").AsInt16().Nullable()
                .WithColumn("instituicoes_espiritas_freq").AsString(300).Nullable();
        }

        private void CriarTabelaPedidos()
        {
            Create.Table("pedidos")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("valor").AsDecimal(18, 2).NotNullable()
                .WithColumn("tipo").AsInt16().NotNullable()
                .WithColumn("id_forma_pagamento").AsInt32().NotNullable()
                    .ForeignKey("fk_pedido_forma_pag", "formas_pagamento", "id").OnUpdate(Rule.Cascade)
                .WithColumn("id_conta").AsInt32().NotNullable()
                    .ForeignKey("fk_pedido_conta", "contas", "id").OnUpdate(Rule.Cascade).OnDelete(Rule.Cascade)
                .WithColumn("id_pessoa_pagadora").AsInt32().NotNullable()
                    .ForeignKey("fk_pedido_pessoa_pag", "pessoas", "id").OnUpdate(Rule.Cascade);

        }

        private void CriarTabelaPedidosInscricoes()
        {
            Create.Table("pedidos_inscricoes")
                .WithColumn("id_pedido").AsInt32().NotNullable()
                    .ForeignKey("fk_pedinsc_pedido", "pedidos", "id").OnUpdate(Rule.Cascade)
                .WithColumn("id_inscricao").AsInt32().NotNullable()
                    .ForeignKey("fk_pedinsc_insc", "inscricoes", "id").OnUpdate(Rule.Cascade);

            Create.PrimaryKey("pk_pedidos_inscricoes")
                .OnTable("pedidos_inscricoes")
                .Columns("id_pedido", "id_inscricao");
        }

        private void CriarTabelaPagamentos()
        {
            Create.Table("pagamentos")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()

                // Mantido conforme mapping (apesar do nome ser suspeito)
                .WithColumn("id_pedido").AsInt32().NotNullable()
                    .ForeignKey("fk_pag_pedido", "pedidos", "id").OnUpdate(Rule.Cascade)

                .WithColumn("valor").AsDecimal(18, 2).NotNullable()
                .WithColumn("desconto").AsDecimal(18, 2).NotNullable()

                .WithColumn("valor_pago").AsDecimal(18, 2).Nullable()
                .WithColumn("data_pago").AsDateTime().Nullable()

                .WithColumn("data_registro_pagamento").AsDateTime().NotNullable()

                .WithColumn("meio_pagamento").AsInt16().Nullable()
                .WithColumn("situacao_pagamento").AsInt16().NotNullable()

                .WithColumn("numero_parcelas").AsInt32().Nullable();
        }

        private void CriarTabelaPagamentosLogs()
        {
            Create.Table("pagamentos_logs")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("id_pagamento").AsInt32().NotNullable()
                    .ForeignKey("fk_paglog_pag", "contas_bancarias", "id").OnUpdate(Rule.Cascade)
                .WithColumn("tipo").AsInt16().NotNullable()
                .WithColumn("data").AsDateTime().NotNullable()
                .WithColumn("mensagem").AsString(500).Nullable()
                .WithColumn("dados").AsString(4000).Nullable();
        }

        private void CriarTabelaFormasPagamento()
        {
            Create.Table("formas_pagamento")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("nome").AsString(200).NotNullable()
                .WithColumn("nr_parcelas_minima").AsInt32().NotNullable()
                .WithColumn("nr_parcelas_maxima").AsInt32().NotNullable();
        }

        private void CriarTabelaTransacoes()
        {
            Create.Table("transacoes")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("id_conta_bancaria").AsInt32().NotNullable()
                    .ForeignKey("fk_trans_cb", "contas_bancarias", "id").OnUpdate(Rule.Cascade)
                .WithColumn("data_hora").AsDateTime().NotNullable()
                .WithColumn("descricao").AsString(200).Nullable()
                .WithColumn("tipo").AsInt16().NotNullable()
                .WithColumn("valor").AsDecimal(18,2).NotNullable();
        }

        private void CriarTabelaTransacoesConta()
        {
            Create.Table("transacoes_conta")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("id_conta").AsInt32().NotNullable()
                    .ForeignKey("fk_tc_ct", "contas", "id").OnUpdate(Rule.Cascade).OnDelete(Rule.Cascade)
                .WithColumn("id_transacao").AsInt32().NotNullable()
                    .ForeignKey("fk_tc_tr", "transacoes", "id").OnUpdate(Rule.Cascade)
                .WithColumn("data").AsDateTime().NotNullable()
                .WithColumn("valor_transacao").AsDecimal(18, 2).NotNullable()
                .WithColumn("valor_multa").AsDecimal(18, 2).NotNullable()
                .WithColumn("valor_juros").AsDecimal(18, 2).NotNullable()
                .WithColumn("valor_desconto").AsDecimal(18, 2).NotNullable();
        }

        private void CriarTabelaContas()
        {
            Create.Table("contas")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("id_pessoa").AsInt32().NotNullable()
                    .ForeignKey("fk_ct_pe", "pessoas", "id").OnUpdate(Rule.Cascade)
                .WithColumn("valor").AsDecimal(18, 2).NotNullable()
                .WithColumn("liquidado").AsBoolean().NotNullable()
                .WithColumn("data_criado").AsDateTime().NotNullable()
                .WithColumn("descricao").AsString(200).Nullable()
                .WithColumn("data_vencimento").AsDateTime().NotNullable()
                .WithColumn("tipo").AsInt16().NotNullable()
                .WithColumn("valor_total_transacoes").AsDecimal(18, 2).NotNullable()
                .WithColumn("valor_total_desconto").AsDecimal(18, 2).NotNullable()
                .WithColumn("valor_total_juros").AsDecimal(18, 2).NotNullable()
                .WithColumn("valor_total_multa").AsDecimal(18, 2).NotNullable();
        }

        private void CriarTabelaContasBancarias()
        {
            Create.Table("contas_bancarias")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("nome").AsString(250).NotNullable();
        }

        private void CriarTabelaPrecosInscricao()
        {
            Create.Table("precos_inscricao")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("id_evento").AsInt32().NotNullable()
                    .ForeignKey("fk_preco_evento", "eventos", "id").OnUpdate(Rule.Cascade)
                .WithColumn("idade_max").AsInt32().NotNullable();
        }

        private void CriarTabelaPrecosInscricaoValores()
        {
            Create.Table("preco_inscricao_valores")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("id_preco_inscricao").AsInt32().NotNullable()
                    .ForeignKey("fk_piv_pi", "precos_inscricao", "id").OnUpdate(Rule.Cascade).OnDelete(Rule.Cascade)
                .WithColumn("id_forma_pagamento").AsInt32().NotNullable()
                    .ForeignKey("fk_piv_forma", "formas_pagamento", "id").OnUpdate(Rule.Cascade)
                .WithColumn("valor").AsDecimal(18, 2).NotNullable();
        }
    }
}