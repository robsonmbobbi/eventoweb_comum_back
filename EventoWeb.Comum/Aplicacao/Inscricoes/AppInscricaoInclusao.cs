using EventoWeb.Comum.Aplicacao.Eventos;
using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Repositorios;

namespace EventoWeb.Comum.Aplicacao.Inscricoes;

public class AppInscricaoInclusao : AppInscricaoBase
{
    private readonly IEventos m_Eventos;
    private readonly IPessoas m_Pessoas;

    public AppInscricaoInclusao(IContexto contexto, IInscricoes inscricoes, 
        IPessoas pessoas, IEventos eventos) : 
        base(contexto, inscricoes)
    {
        m_Pessoas = pessoas;
        m_Eventos = eventos;
    }
    
    public DTOInscricao? dtoInscricao { get; set; }

    public DTOInscricao Incluir()
    {
        if (dtoInscricao == null)
            throw new Exception("Os dados da inscrição não foram informados!");
        
        ExecutarSeguramente(() =>
        {
            Inscricao inscricao;
            if (dtoInscricao.Tipo == EnumTipoInscricao.Infantil)
            {
                inscricao = IncluirInscricaoInfantil();
            }
            else
            {
                inscricao = IncluirInscricaoAdulto();
            }

            dtoInscricao.Id = inscricao.Id;
        });

        return dtoInscricao;
    }

    private Inscricao IncluirInscricaoAdulto()
    {
        var evento = ObterEvento();
        var pessoa = GerenciarPessoa();

        var inscricao = new InscricaoParticipante(
            evento,
            pessoa,
            DateTime.Now);
        inscricao.InstituicoesEspiritasFrequenta = dtoInscricao!.InstituicoesEspiritasFrequenta;
        inscricao.DormeEvento = dtoInscricao!.DormeEvento;
        inscricao.NomeCracha = dtoInscricao!.NomeCracha;
        inscricao.Observacoes = dtoInscricao!.Observacoes;

        return inscricao;
    }

    private Inscricao IncluirInscricaoInfantil()
    {
        var pessoa = GerenciarPessoa();
        var evento = ObterEvento();

        var inscricao = new InscricaoInfantil(
            pessoa,
            evento,
            null,
            null,
            DateTime.Now,
            dtoInscricao!.DormeEvento);
    }

    private Evento ObterEvento()
    {
        return m_Eventos.Obter(dtoInscricao!.IdEvento) ??
               throw new Exception($"Evento com o id {dtoInscricao.IdEvento} não encontrado");
    }

    private Pessoa GerenciarPessoa()
    {
        
    }
}