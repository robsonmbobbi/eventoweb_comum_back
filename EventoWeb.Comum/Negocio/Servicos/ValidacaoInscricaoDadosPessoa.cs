using EventoWeb.Comum.Negocio.Entidades;

namespace EventoWeb.Comum.Negocio.Servicos;

public class ValidacaoInscricaoDadosPessoa: IValidacao<Inscricao>
{
    public void Validar(Inscricao entidade)
    {
        if (entidade.Pessoa.Cidade == null || entidade.Pessoa.UF == null)
        {
            throw new Exception("A cidade e estado da pessoa devem ser informados para realizar a inscrição");
        }

    }
}