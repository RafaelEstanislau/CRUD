using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_Livros
{
    public class Repository : IRepository<Livro>

    {
        
        protected List<Livro> listaDeLivros = Singleton.Instance();

        public void Save()
        {
            var formulario2 = new Form2(null);
            formulario2.textBoxID.Enabled = false;
            formulario2.ShowDialog();
            if (formulario2.DialogResult == DialogResult.OK)
            {
                if (listaDeLivros.Count() == 0)
                {
                    var ultimoId = 0;

                    formulario2.Livro.id = Singleton.ProximoId(ultimoId);
                    listaDeLivros.Add(formulario2.Livro);
                }
                else
                {
                    var ultimoId = listaDeLivros.Last().id;
                    formulario2.Livro.id = Singleton.ProximoId(ultimoId);

                    listaDeLivros.Add(formulario2.Livro);
                }
            }
        }
        public void Update(Livro livro)
        {

        }
        public void Delete()
        {
            
           // Form1 formulario1 = new();
            //int removeIndex = formulario1.dataGridView1.CurrentRow.Index;
            //listaDeLivros.RemoveAt();
            
            
               
            
           
        }
        
    }
}
