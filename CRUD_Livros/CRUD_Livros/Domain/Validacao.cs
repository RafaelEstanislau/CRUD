using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_Livros.Domain
{
    public class Validacao
    {
        public static bool ValidacaoDeCampos(Livro livro)
        {
            bool validacao = true;
            if (livro.nome == string.Empty)
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
            if (livro.ano > DateTime.Now)
            {
                validacao = false;
                throw new Exception("Insira uma data válida anterior à hoje!");
                
            }
            return validacao;
        }
    }
}
