using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUD_Livros.Dominio.RegraDeNegocio;

namespace CRUD_Livros.Infra.AcessoDeDados
{
    public interface IRepositorySQL
    {
        public void Salvar(Livro livro);

        public List<Livro> BuscarTodos();

        public Livro BuscarPorID(int id);

        public void Editar(Livro livro);

        public void Excluir(int id);
    }
}
