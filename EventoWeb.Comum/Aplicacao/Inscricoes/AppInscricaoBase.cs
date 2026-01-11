using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Repositorios;

namespace EventoWeb.Comum.Aplicacao.Inscricoes;

public abstract class AppInscricaoBase: AppBase
{
    protected IInscricoes Inscricoes { get; private set; }

    public AppInscricaoBase(IContexto contexto, IInscricoes inscricoes) : 
        base(contexto)
    {
        Inscricoes = inscricoes ?? throw new ArgumentNullException(nameof(inscricoes));
    }

    protected Inscricao ObterOuExcecao(int id)
    {
        return Inscricoes.Obter(id) ?? throw new Exception($"Inscrição não encontrada com o id {id}");
    }
}