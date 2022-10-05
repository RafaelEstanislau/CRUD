namespace CRUD_Livros.Dominio.RegraDeNegocio
{
    public class Validacao
    {
        public static bool ValidacaoDeCampos(Livro livro)
        {
            bool validacao = true;
            if (livro.titulo == string.Empty)
            {
                validacao = false;
                throw new Exception("Campo Nome deve ser informado");
                
            }
            if (livro.editora == string.Empty)
            {
                validacao = false;
                throw new Exception("Campo Editora deve ser informado");
                
            }
            if (livro.autor == string.Empty)
            {
                validacao = false;
                throw new Exception("Campo Autor deve ser informado");
                
            }
            if (livro.lancamento > DateTime.Now)
            {
                validacao = false;
                throw new Exception("Insira uma data válida anterior à hoje!");
                
            }
            return validacao;
        }
    }
}
