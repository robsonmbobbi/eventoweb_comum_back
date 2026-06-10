using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.ObjetosValor;

namespace EventoWeb.Comum.Testes.Negocio.Fixtures
{
    /// <summary>
    /// Fixtures para criação de dados de teste reutilizáveis relacionados a Pessoa
    /// </summary>
    public static class PessoasFixtures
    {
        public static Pessoa CriarPessoaValida()
        {
            return new Pessoa(
                new CPF("05506427654"),
                new String200("João Silva"),
                new EMail("joao@example.com"),
                new Telefone("37991925134")
            );
        }

        public static Pessoa CriarPessoaComNome(string nome)
        {
            return new Pessoa(
                new CPF("05506427654"),
                new String200(nome),
                new EMail("joao@example.com"),
                new Telefone("37991925134")
            );
        }

        public static Pessoa CriarPessoaComDataNascimento(int dia, int mes, int ano)
        {
            var pessoa = CriarPessoaValida();
            pessoa.DataNascimento = new DataAniversario(new DateTime(ano, mes, dia));
            return pessoa;
        }

        public static Pessoa CriarPessoaComIdade(int idade)
        {
            var dataNascimento = DateTime.Now.AddYears(-idade);
            var pessoa = CriarPessoaValida();
            pessoa.DataNascimento = new DataAniversario(dataNascimento);
            return pessoa;
        }

        public static Pessoa CriarCrianca(int idade = 10)
        {
            return CriarPessoaComIdade(idade);
        }

        public static Pessoa CriarAdulto(int idade = 25)
        {
            return CriarPessoaComIdade(idade);
        }
    }
}
