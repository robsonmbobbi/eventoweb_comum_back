using EventoWeb.Comum.Negocio.Entidades;

namespace EventoWeb.Comum.Aplicacao.Inscricoes;

public class DTOPessoa
{
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public string CPF { get; set; }
    public string? AlergiaAlimentos { get; set; }
    public string Celular { get; set; }
    public bool EhDiabetico { get; set; }
    public bool EhVegetariano { get; set; }
    public string Email { get; set; }
    public EnumSexo? Sexo { get; set; }
    public bool UsaAdocanteDiariamente { get; set; }
}