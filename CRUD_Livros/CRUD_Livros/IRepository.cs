using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Livros
{
    public interface IRepository<Livro>
    {
        void Salvar(Livro livro);

        void Deletar(Livro livro);

        Livro Editar(int id);

        List <Livro> BuscarTodos();

        int BuscarPorID(int id);

    }
}
