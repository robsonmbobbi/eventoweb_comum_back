using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Repositorios;
using EventoWeb.Comum.Negocio.Servicos;

namespace EventoWeb.Comum.Aplicacao.Inscricoes;

public class AppInscricaoInclusaoOnLine(
    IContexto contexto,
    IInscricoes inscricoes,
    IPessoas pessoas,
    IEventos eventos)
    : AppInscricaoBase(contexto, inscricoes)
{
    public DTOInscricao? DtoInscricao { get; set; }

    public DTOInscricao Incluir()
    {
        if (DtoInscricao == null)
            throw new Exception("Os dados da inscrição não foram informados!");
        
        ExecutarSeguramente(() =>
        {
            var inscricao = DtoInscricao.Tipo == EnumTipoInscricao.Infantil ? 
                IncluirInscricaoInfantil() : IncluirInscricaoAdulto();

            var srv = new SrvInclusaoInscricao(
                Inscricoes,
                [
                    new ValidacaoInscricaoPessoaJaInscrita(Inscricoes),
                    new ValidacaoInscricaoPeriodoInscricaoOnLine()
                ]);
            srv.Incluir(inscricao);

            DtoInscricao.Id = inscricao.Id;
            DtoInscricao.Situacao = inscricao.Situacao;
        });

        return DtoInscricao;
    }

    private Inscricao IncluirInscricaoAdulto()
    {
        var evento = ObterEvento();
        var pessoa = GerenciarPessoa();

        var inscricao = new InscricaoParticipante(
            evento,
            pessoa,
            DateTime.Now)
        {
            InstituicoesEspiritasFrequenta = DtoInscricao!.InstituicoesEspiritasFrequenta,
            DormeEvento = DtoInscricao!.DormeEvento,
            NomeCracha = DtoInscricao!.NomeCracha,
            Observacoes = DtoInscricao!.Observacoes
        };

        return inscricao;
    }

    private Inscricao IncluirInscricaoInfantil()
    {
        var pessoa = GerenciarPessoa();
        var evento = ObterEvento();

        InscricaoParticipante responsavel1 = 
            Inscricoes.Obter(DtoInscricao!.Responsavel1.IdInscricao) as InscricaoParticipante ??
            throw new Exception($"Inscrição do primeiro responsável não encontrada ou não é do tipo Participante. Id {DtoInscricao.Responsavel1.IdInscricao}");
        InscricaoParticipante? responsavel2 = null;

        if (DtoInscricao.Responsavel2 != null)
        {
            responsavel2 = 
                Inscricoes.Obter(DtoInscricao.Responsavel2.IdInscricao) as InscricaoParticipante ??
                throw new Exception($"Inscrição do segundo responsável não encontrada ou não é do tipo Participante. Id {DtoInscricao.Responsavel2.IdInscricao}");
        }

        var inscricao = new InscricaoInfantil(
            pessoa,
            evento,
            responsavel1,
            responsavel2,
            DateTime.Now,
            DtoInscricao!.DormeEvento)
        {
            NomeCracha = DtoInscricao!.NomeCracha,
            Observacoes = DtoInscricao!.Observacoes
        };

        return inscricao;
    }

    private Evento ObterEvento()
    {
        return eventos.Obter(DtoInscricao!.IdEvento) ??
               throw new Exception($"Evento com o id {DtoInscricao.IdEvento} não encontrado");
    }

    private Pessoa GerenciarPessoa()
    {
        var ehInclusao = false;
        var pessoa = pessoas.ObterPorCPF(DtoInscricao!.Pessoa.CPF);
        if (pessoa == null)
        {
            ehInclusao = true;
            
            pessoa = new Pessoa(
                new CPF(DtoInscricao.Pessoa.CPF),
                new NomeCompleto(DtoInscricao.Pessoa.Nome),
                new EMail(DtoInscricao.Pessoa.Email),
                new Telefone(DtoInscricao.Pessoa.Celular)
            );
        }
        else
        {
            pessoa.Nome = new NomeCompleto(DtoInscricao.Pessoa.Nome);
            pessoa.Sexo = DtoInscricao.Pessoa.Sexo;
            pessoa.DataNascimento = new DataAniversario(DtoInscricao.Pessoa.DataNascimento);
            pessoa.Email = new EMail(DtoInscricao.Pessoa.Email);
            pessoa.CelularWP = new Telefone(DtoInscricao.Pessoa.Celular);
        }

        pessoa.Sexo = DtoInscricao.Pessoa.Sexo;
        pessoa.DataNascimento = new DataAniversario(DtoInscricao.Pessoa.DataNascimento);
        pessoa.AlergiaAlimentos = DtoInscricao.Pessoa.AlergiaAlimentos;
        pessoa.EhDiabetico = DtoInscricao.Pessoa.EhDiabetico;
        pessoa.EhVegetariano = DtoInscricao.Pessoa.EhVegetariano;
        pessoa.UsaAdocanteDiariamente = DtoInscricao.Pessoa.UsaAdocanteDiariamente;

        if (ehInclusao)
        {
            pessoas.Incluir(pessoa);
        }
        else
        {
            pessoas.Atualizar(pessoa);
        }

        return pessoa;
    }
}