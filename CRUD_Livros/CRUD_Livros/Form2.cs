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

       
       
        public Form2()
        {
            InitializeComponent();
           
        }
        
        private void button1_Click(object sender, EventArgs e) //CADASTRO
        {
            CadastraLivro();
            MessageBox.Show("Livro cadastrado com sucesso!");
            this.Close();

        }
        public List<Livro>CadastraLivro()
        {

            string nomeLivro = textBox1.Text;
            string nomeEditora = textBox2.Text;
            string autorLivro = textBox3.Text;
            string anoLivro = textBox4.Text;
            string idLivro = textBox5.Text;
            
            
            Form1.listaDeLivros.Add(new Livro() { nome = nomeLivro, autor = autorLivro, editora = nomeEditora, 
                                            ano = anoLivro, id = idLivro });
            
            return Form1.listaDeLivros;
        }

        private void button2_Click(object sender, EventArgs e)
        {  
            //volta para a tela inicial
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e) //EDITAR
            
        {
            EditaLivro();
            this.Close();
            Form1 listagem = new();
            listagem.ListarLivros();
        }
        public List<Livro> EditaLivro()
        {
            
            Livro editado = new()
            {
                nome = textBox1.Text,
                autor = textBox2.Text,
                editora = textBox3.Text,
                ano = textBox4.Text,
                id = textBox5.Text
            };

            Form1.listaDeLivros[Form1.index] = editado;

            
            MessageBox.Show("Livro atualizado!");
            this.Close();
            return Form1.listaDeLivros;
        }
    }
}
