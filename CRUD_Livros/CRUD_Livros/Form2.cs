using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_Livros
{
    public partial class Form2 : Form
    {
        public static List<Livro> livrosAntigos = new();
        public Form2()
        {
            InitializeComponent();
           
        }
        
        private void CadastraFormulario_Click(object sender, EventArgs e) 
        {
            
            CadastraLivro();
            
        }
        public void CadastraLivro()
        {
            string nomeLivro;
            string nomeEditora;
            string autorLivro;
            string anoLivro;

            if (textBoxNome.Text.Equals("") || textBoxEditora.Text.Equals("")|| textBoxAutor.Text.Equals("") )
               
            {
                MessageBox.Show("Nenhum campo pode ser vazio!");
            }
            
            else if (dateTimePicker1.Value > DateTime.Now)
            {
                MessageBox.Show("Insira uma data válida anterior à hoje!");

            }
            else
            {
                nomeLivro = textBoxNome.Text;
                nomeEditora = textBoxEditora.Text;
                autorLivro = textBoxAutor.Text;
                anoLivro = dateTimePicker1.Value.ToString("dd/MM/yyyy");
                Form1.listaDeLivros.Add(new Livro()
                {
                    nome = nomeLivro,
                    autor = autorLivro,
                    editora = nomeEditora,
                    ano = Convert.ToDateTime(anoLivro),
                     
                });
                MessageBox.Show("Livro cadastrado com sucesso!");
                this.Close();
            }
    
        }

        private void VoltaFormulario_Click(object sender, EventArgs e)
        {  
            
            this.Close();
        }

        private void EditaFormulario_Click(object sender, EventArgs e) 
            
        {
                EditaLivro();
                
                Form1 listagem = new();
                listagem.ListarLivros();
            

        }
        public List<Livro> EditaLivro()
        {
            if (textBoxNome.Text.Equals("") || textBoxEditora.Text.Equals("") || textBoxAutor.Text.Equals(""))

            {
                MessageBox.Show("Nenhum campo pode ser vazio!");
            }
            else if (dateTimePicker1.Value > DateTime.Now)
            {
                MessageBox.Show("Insira uma data válida anterior à hoje!");

            }
            else
            {
                int idAntigo = Form1.listaDeLivros[Form1.index].id;

                foreach (var livro in Form1.listaDeLivros.Where(l => l.id == idAntigo))
                {
                    livro.nome = textBoxNome.Text;
                    livro.autor = textBoxAutor.Text;
                    livro.editora = textBoxEditora.Text;
                    livro.ano = dateTimePicker1.Value;


                }
                MessageBox.Show("Livro atualizado!");
                this.Close();
            }







            return Form1.listaDeLivros;
        }
    }
}
