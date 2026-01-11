namespace EventoWeb.Comum.Aplicacao.Eventos;

public class DTOEvento
{
    public int? Id { get; set; }
    public string Nome { get; set; }
    public DateTime DataInicialInscricao { get; set; }
    public DateTime DataFinalInscricao { get; set; }
    public DateTime DataInicialRealizacao { get; set; }
    public DateTime DataFinalRealizacao { get; set; }
    public string? Logotipo { get; set; }
    public int IdadeMinimaAdulto { get; set; }
    public string? Regulamento { get; set; }
}