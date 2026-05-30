using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Entidades.Notificacoes;
using EventoWeb.Comum.Negocio.ObjetosValor;
using EventoWeb.Comum.Negocio.Repositorios;
using System.Text.Json;

namespace EventoWeb.Comum.Negocio.Servicos.Notificacoes.Inscricoes
{
    public enum EnumTipoNotificacaoInscricao { InscricaoRecebida, InscricaoAceita, InscricaoRejeitada }

    public class SrvNotificacaoInscricao(IModelosMensagemNotificacao modelosNotificacao, IMensagens mensagens)
    {
        private readonly IModelosMensagemNotificacao m_ModelosNotificacao = modelosNotificacao;
        private readonly IMensagens m_Mensagens = mensagens;

        public void Notificar(IEnumerable<Inscricao> inscricoes, EnumTipoNotificacaoInscricao tipo)
        {
            foreach(var inscricao in inscricoes)
            {
                var modelos = m_ModelosNotificacao.ListarPorTipo(inscricao.Evento.Id, (EnumTipoNotificacao)(int)tipo);
                foreach(var modelo in modelos)
                {
                    var destinatario = "";
                    if (modelo.Meio == EnumMeioNotificacao.EMail)
                    {
                        destinatario = inscricao.Pessoa.Email.Endereco;
                    }
                    else
                    {
                        destinatario = inscricao.Pessoa.CelularWP.Numero;
                    }

                    var mensagem = new MensagemNotificacao(modelo, new String500(destinatario), new StringClob(GerarVariaveis(inscricao)));
                    m_Mensagens.Incluir(mensagem);
                }
            }
        }

        private string GerarVariaveis(Inscricao inscricao)
        {
            var infantil = inscricao as InscricaoInfantil;

            return JsonSerializer.Serialize(
                new
                {
                    Tipo = inscricao is InscricaoInfantil ? "Infantil" : "Participante",
                    inscricao.Pessoa.Nome.Valor,
                    CPF = inscricao.Pessoa.CPF.Numero,
                    Email = inscricao.Pessoa.Email.Endereco,
                    Celular = inscricao.Pessoa.CelularWP.Numero,
                    inscricao.Pessoa.EhVegetariano,
                    inscricao.Pessoa.EhDiabetico,
                    inscricao.Pessoa.AlergiaAlimentos,
                    inscricao.Pessoa.UsaAdocanteDiariamente,
                    Evento = inscricao.Evento.Nome.Valor,
                    NomeCracha = inscricao.NomeCracha?.Valor,
                    inscricao.DormeEvento,
                    Observacoes = inscricao.Observacoes?.Valor,
                    Responsavel1 = infantil?.InscricaoResponsavel1.Pessoa.Nome.Valor,
                    Responsavel2 = infantil?.InscricaoResponsavel2?.Pessoa.Nome.Valor
                }
            );
        }
    }
}
