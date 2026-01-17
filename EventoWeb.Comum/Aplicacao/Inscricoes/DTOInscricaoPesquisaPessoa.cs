namespace EventoWeb.Comum.Aplicacao.Inscricoes;

public enum EnumSituacaoPesquisaPessoa
{
    InscricaoNoLimbo,
    InscricaoRealizada,
    InscricaoNaoExiste
}

public class DTOInscricaoPesquisaPessoa
{
    public EnumSituacaoPesquisaPessoa Situacao { get; set; }
    public DTOInscricao Inscricao { get; set; }
    public DTOPessoa Pessoa { get; set; }
}