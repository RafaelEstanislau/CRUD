
namespace Dominio.Constantes
{
    public static class MensagensDeTela
    {
        public const string MENSAGEM_ERRO_CARREGAR_LIVROS = "Erro ao carregar lista de livros";
        public const string MENSAGEM_ERRO_BUSCAR_LIVRO = "Erro ao buscar por livro";
        public const string MENSAGEM_ERRO_ADICIONAR_LIVRO = "Erro ao adicionar livro";
        public const string MENSAGEM_ERRO_EDITAR_LIVRO = "Erro ao editar livro";
        public const string MENSAGEM_ERRO_EXCLUIR_LIVRO = "Erro ao excluir livro";
        
        public const string TITULO_PROBLEM_DETAILS = "Não foi possível concluir a ação";
        public const string DETALHE_PROBLEM_DETAILS = "Houve erro na validação dos campos";

        public static string FalharAoBuscarPorID(int id)
        {
            return $"Livro com id {id} não encontrado";
        }

    }
}
