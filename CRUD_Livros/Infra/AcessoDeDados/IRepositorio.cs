using CRUD_Livros.Dominio.RegraDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Livros.Infra.AcessoDeDados
{
   public interface IRepositorio
    {
        public int Salvar(Livro livro);

        public List<Livro> BuscarTodos();

        public Livro BuscarPorID(int id);

        public Livro Editar(Livro livro);

        public void Excluir(int id);
    }
}
