using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Repositorios;

namespace EventoWeb.Comum.Aplicacao.Inscricoes;

public class AppInscricaoAtualizacao(IContexto contexto, IInscricoes inscricoes)
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

    private void AtualizarInscricaoInfantil(InscricaoInfantil infantil)
    {
        throw new NotImplementedException();
    }

    private void AtualizarInscricaoAdulto(InscricaoParticipante participante)
    {
        throw new NotImplementedException();
    }
}