using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;
using CRUD_Livros.Dominio.RegraDeNegocio;

namespace CRUD_Livros.Infra.AcessoDeDados
{
    public class Repository : IRepository<Livro>

    {
        protected List<Livro> listaDeLivros = Singleton.Instance();

        public void Salvar(Livro livro)
        {
            listaDeLivros.Add(livro);
        }
        public List<Livro> BuscarTodos()
        {
            return listaDeLivros.ToList();
        }
        public int BuscarPorID(int indexGrid)
        {
            int idSelecionado = listaDeLivros.First(l => l.id == indexGrid).id;

            return idSelecionado;
        }
        public Livro Editar(int id)
        {
            var IDEditado = BuscarPorID(id);
            Livro livroEditado = new();
            livroEditado = listaDeLivros.First(l => l.id == id);
            return livroEditado;
        }
        public void Deletar(Livro livro)
        {
            listaDeLivros.Remove(livro);
        }
    }
}
