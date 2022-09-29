using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUD_Livros.Domain;

namespace CRUD_Livros.DataAccessLibrary
{
    public interface IRepositorySQL
    {
        public void Salvar(Livro livro);

        public void BuscarTodos(DataGridView grid);

        public Livro BuscarPorID(int id);

        public void Editar(Livro livro);

        public void Excluir(int id);
    }
}
