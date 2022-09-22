using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Reflection.Metadata.BlobBuilder;

namespace CRUD_Livros
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

        public void Deletar(Livro livro)
        { 
            listaDeLivros.Remove(livro);
        }
        public void Atualizar(Livro livro)
        {
            listaDeLivros.Add(livro);
        }
    }
}
