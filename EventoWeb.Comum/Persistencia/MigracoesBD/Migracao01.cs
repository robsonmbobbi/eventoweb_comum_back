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

            CriarTabelaPedidos();
            CriarTabelaPedidosInscricoes();

            CriarTabelaPagamentos();
            CriarTabelaPagamentosLogs();

            CriarTabelaPrecosInscricao();
        }

        private void CriarTabelaArquivoBinario()
        {
            Create.Table("arquivos_binarios")
                .WithColumn("ID").AsInt32().PrimaryKey().Identity()
                .WithColumn("ARQUIVO").AsBinary().NotNullable()
                .WithColumn("TIPO_ARQUIVO").AsInt16().NotNullable();
        }

        private void CriarTabelaEventos()
        {
            Create.Table("eventos")
                .WithColumn("ID").AsInt32().PrimaryKey().Identity()
                .WithColumn("NOME").AsString(250).NotNullable()
                .WithColumn("DATA_INICIO_INSC").AsDateTime().NotNullable()
                .WithColumn("DATA_FIM_INSC").AsDateTime().NotNullable()
                .WithColumn("DATA_INICIO_EVENTO").AsDateTime().NotNullable()
                .WithColumn("DATA_FIM_EVENTO").AsDateTime().NotNullable()
                .WithColumn("DATA_REGISTRO").AsDateTime().NotNullable()
                .WithColumn("ID_ARQUIVO_LOGOTIPO").AsInt32().Nullable()
                    .ForeignKey("FK_EVENTO_ARQBIN", "arquivos_binarios", "ID").OnDelete(Rule.Cascade).OnUpdate(Rule.Cascade)
                .WithColumn("IDADE_MINIMA_INSC_ADULTO").AsInt32().NotNullable()
                .WithColumn("REGULAMENTO").AsString(int.MaxValue).Nullable();
        }

        private void CriarTabelaPessoas()
        {
            Create.Table("pessoas")
                .WithColumn("ID").AsInt32().PrimaryKey().Identity()

                .WithColumn("CPF").AsString(11).Nullable()

                .WithColumn("NOME").AsString(150).NotNullable()

                .WithColumn("CELULAR").AsString(15).NotNullable()
                .WithColumn("EMAIL").AsString(100).NotNullable()

                .WithColumn("ALERGIA_ALIMENTOS").AsString(100).Nullable()

                .WithColumn("DATA_NASCIMENTO").AsDate().NotNullable()

                .WithColumn("EH_DIABETICO").AsBoolean().NotNullable()
                .WithColumn("EH_VEGETARIANO").AsBoolean().NotNullable()

                .WithColumn("SEXO").AsInt16().Nullable()
                .WithColumn("USA_ADOCANTE_DIAR").AsBoolean().Nullable();
        }

        private void CriarTabelaInscricoes()
        {
            Create.Table("inscricoes")
                .WithColumn("ID_INSCRICAO").AsInt32().PrimaryKey().Identity()

                .WithColumn("TIPO_INSCRICAO").AsString(30).NotNullable()

                .WithColumn("CONFIRMADO").AsBoolean().NotNullable()
                .WithColumn("DATA_RECEBIMENTO").AsDate().NotNullable()
                .WithColumn("DORMIRA").AsBoolean().NotNullable()

                .WithColumn("ID_EVENTO").AsInt32().NotNullable()
                    .ForeignKey("FK_INSC_EVENTO", "eventos", "ID").OnDelete(Rule.Cascade).OnUpdate(Rule.Cascade)

                .WithColumn("NOME_CRACHA").AsString(150).Nullable()

                .WithColumn("ID_PESSOA").AsInt32().NotNullable()
                    .ForeignKey("FK_INSC_PESSOA", "pessoas", "ID").OnDelete(Rule.Cascade).OnUpdate(Rule.Cascade)

                .WithColumn("SITUACAO").AsInt16().NotNullable()

                .WithColumn("OBSERVACOES").AsString(int.MaxValue).Nullable()

                // Subclasses (InscricaoInfantil / InscricaoParticipante)
                .WithColumn("ID_INSC_RESPONSAVEL_1").AsInt32().Nullable()
                    .ForeignKey("FK_INSC_INF_1", "inscricoes", "ID_INSCRICAO").OnDelete(Rule.Cascade).OnUpdate(Rule.Cascade)
                .WithColumn("ID_INSC_RESPONSAVEL_2").AsInt32().Nullable()
                    .ForeignKey("FK_INSC_INF_2", "inscricoes", "ID_INSCRICAO").OnDelete(Rule.Cascade).OnUpdate(Rule.Cascade)

                .WithColumn("TIPO").AsInt16().Nullable()
                .WithColumn("INSTITUICOES_ESPIRITAS_FREQ").AsString(300).Nullable();
        }

        private void CriarTabelaPedidos()
        {
            Create.Table("PEDIDOS")
                .WithColumn("ID").AsInt32().PrimaryKey().Identity()
                .WithColumn("VALOR").AsDecimal(18, 2).NotNullable()
                .WithColumn("FORMA_PAGAMENTO").AsInt16().NotNullable();
        }

        private void CriarTabelaPedidosInscricoes()
        {
            Create.Table("PEDIDOS_INSCRICOES")
                .WithColumn("ID_PEDIDO").AsInt32().NotNullable()
                    .ForeignKey("FK_PEDINSC_PEDIDO", "PEDIDOS", "ID").OnDelete(Rule.Cascade).OnUpdate(Rule.Cascade)
                .WithColumn("ID_INSCRICAO").AsInt32().NotNullable()
                    .ForeignKey("FK_PEDINSC_INSC", "inscricoes", "ID_INSCRICAO").OnDelete(Rule.Cascade).OnUpdate(Rule.Cascade);

            Create.UniqueConstraint("UK_PEDIDOS_INSCRICOES_PEDIDO_INSC")
                .OnTable("PEDIDOS_INSCRICOES")
                .Columns("ID_PEDIDO", "ID_INSCRICAO");
        }

        private void CriarTabelaPagamentos()
        {
            Create.Table("PAGAMENTOS")
                .WithColumn("ID").AsInt32().PrimaryKey().Identity()

                // Mantido conforme mapping (apesar do nome ser suspeito)
                .WithColumn("ID_PEDIDO").AsInt32().NotNullable()
                    .ForeignKey("FK_PAG_PEDIDO", "PEDIDOS", "ID").OnDelete(Rule.Cascade).OnUpdate(Rule.Cascade)

                .WithColumn("VALOR").AsDecimal(18, 2).NotNullable()
                .WithColumn("DESCONTO").AsDecimal(18, 2).NotNullable()

                .WithColumn("VALOR_PAGO").AsDecimal(18, 2).Nullable()
                .WithColumn("DATA_PAGO").AsDateTime().Nullable()

                .WithColumn("DATA_REGISTRO_PAGAMENTO").AsDateTime().NotNullable()

                .WithColumn("MEIO_PAGAMENTO").AsInt16().Nullable()
                .WithColumn("SITUACAO_PAGAMENTO").AsInt16().NotNullable()

                .WithColumn("NUMERO_PARCELAS").AsInt32().Nullable();
        }

        private void CriarTabelaPagamentosLogs()
        {
            Create.Table("PAGAMENTOS_LOGS")
                .WithColumn("ID").AsInt32().PrimaryKey().Identity()
                .WithColumn("ID_PAGAMENTO").AsInt32().NotNullable()
                    .ForeignKey("FK_PAGLOG_PAG", "PAGAMENTOS", "ID").OnDelete(Rule.Cascade).OnUpdate(Rule.Cascade)

                .WithColumn("TIPO").AsInt16().NotNullable()
                .WithColumn("DATA").AsDateTime().NotNullable()

                .WithColumn("MENSAGEM").AsString(500).Nullable()
                .WithColumn("DADOS").AsString(4000).Nullable();
        }

        private void CriarTabelaPrecosInscricao()
        {
            Create.Table("PRECOS_INSCRICAO")
                .WithColumn("ID").AsInt32().PrimaryKey().Identity()
                .WithColumn("ID_EVENTO").AsInt32().NotNullable()
                    .ForeignKey("FK_PRECO_EVENTO", "eventos", "ID").OnDelete(Rule.Cascade).OnUpdate(Rule.Cascade)

                .WithColumn("IDADE_MAX").AsInt32().NotNullable()
                .WithColumn("PRECO").AsDecimal(18, 2).NotNullable();
        }
    }
}