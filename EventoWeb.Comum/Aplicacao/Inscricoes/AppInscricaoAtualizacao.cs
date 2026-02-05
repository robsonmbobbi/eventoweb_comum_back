using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.ObjetosValor;
using EventoWeb.Comum.Negocio.Repositorios;

namespace EventoWeb.Comum.Aplicacao.Inscricoes;

public class AppInscricaoAtualizacao(IContexto contexto, IInscricoes inscricoes, IPessoas pessoas)
    : AppInscricaoBase(contexto, inscricoes)
{
    public DTOInscricao? DtoInscricao { get; set; }

    public void Atualizar()
    {
        if (DtoInscricao == null)
            throw new Exception("Os dados da inscrição não foram informados!");
        
        ExecutarSeguramente(() =>
        {
            var inscricao = Inscricoes.Obter(DtoInscricao.Id ?? 0) ??
                            throw new Exception($"Inscrição não encontrada com o id {DtoInscricao.Id ?? 0}");

            if (inscricao is InscricaoParticipante participante)
            {
                if (DtoInscricao.Tipo != EnumTipoInscricao.Adulto)
                {
                    throw new Exception(
                        "Os dados da inscrição para atualização se referem a uma inscrição infantil e a inscrição existente é de adulto");
                }
                AtualizarInscricaoAdulto(participante);
            }
            else if (inscricao is InscricaoInfantil infantil)
            {
                if (DtoInscricao.Tipo != EnumTipoInscricao.Infantil)
                {
                    throw new Exception(
                        "Os dados da inscrição para atualização se referem a uma inscrição adulto e a inscrição existente é infantil.");
                }

                AtualizarInscricaoInfantil(infantil);
            }
            
            Inscricoes.Atualizar(inscricao);
        });
    }

    private void AtualizarInscricaoAdulto(InscricaoParticipante participante)
    {
        AtualizarPessoa(participante.Pessoa);
        AtualizarComum(participante);
        participante.InstituicoesEspiritasFrequenta = DtoInscricao!.InstituicoesEspiritasFrequenta;
    }

    private void AtualizarInscricaoInfantil(InscricaoInfantil infantil)
    {
        AtualizarPessoa(infantil.Pessoa);
        AtualizarComum(infantil);
        
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
        
        infantil.AtribuirResponsaveis(responsavel1, responsavel2);
    }
    
    private void AtualizarPessoa(Pessoa pessoa)
    {
        pessoa.Nome = new NomeCompleto(DtoInscricao!.Pessoa.Nome);
        pessoa.Sexo = DtoInscricao.Pessoa.Sexo;
        pessoa.DataNascimento = new DataAniversario(DtoInscricao.Pessoa.DataNascimento);
        pessoa.Email = new EMail(DtoInscricao.Pessoa.Email);
        pessoa.CelularWP = new Telefone(DtoInscricao.Pessoa.Celular);
        
        pessoas.Atualizar(pessoa);
    }    
    
    private void AtualizarComum(Inscricao inscricao)
    {
        inscricao.DormeEvento = DtoInscricao!.DormeEvento;
        inscricao.NomeCracha = DtoInscricao!.NomeCracha;
        inscricao.Observacoes = DtoInscricao!.Observacoes;
    }
}