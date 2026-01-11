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
}