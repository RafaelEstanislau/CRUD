using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUD_Livros.Domain;

namespace CRUD_Livros.DataAccessLibrary
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
