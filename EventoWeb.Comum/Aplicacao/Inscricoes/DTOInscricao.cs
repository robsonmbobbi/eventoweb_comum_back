using EventoWeb.Comum.Negocio.Entidades;

namespace EventoWeb.Comum.Aplicacao.Inscricoes;

public enum EnumTipoInscricao { Infantil, Adulto }

public class DTOInscricao
{
    public int? Id { get; set; }
    public EnumTipoInscricao Tipo { get; set; }
    public int IdEvento { get; set; }
    public string? InstituicoesEspiritasFrequenta { get; set; }
    public bool DormeEvento { get; set; }
    public string? NomeCracha { get; set; }
    public string? Observacoes { get; set; }
    public DTOPessoa Pessoa { get; set; }
    public DTOResponsavel? Responsavel1 { get; set; }
    public DTOResponsavel? Responsavel2 { get; set; }
    public EnumSituacaoInscricao Situacao { get; set; }
    public EnumTipoParticipante? TipoParticipante { get; set; }
}