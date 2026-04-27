using AsaasClient.Core.Response.Base;

namespace EventoWeb.Comum.Persistencia.Integracoes.Asaas
{
    public static class HelperAsaas
    {
        public static Exception TratarErros(this BaseResponse response)
        {
            Exception? excecao = null;
            for(int i = response.Errors.Count - 1; i >= 0; i--)
            {
                var mensagem = $"Código:{response.Errors[i].Code}. Erro: {response.Errors[i].Description}";
                if (excecao == null)
                    excecao = new Exception(mensagem);
                else
                    excecao = new Exception(mensagem, excecao);
            }

            return excecao!;
        }
    }
}
