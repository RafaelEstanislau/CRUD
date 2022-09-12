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
        
        public static List<Livro> listaDeLivros = new();
       
        public Form2()
        {
            InitializeComponent();
            
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            CadastraLivro();
            //MessageBox.Show("Livro cadastrado com sucesso!");
            this.Close();

        }
        public List<Livro>CadastraLivro()
        {
            string nomeLivro = textBox1.Text;
            string nomeEditora = textBox2.Text;
            string autorLivro = textBox3.Text;
            string anoLivro = textBox4.Text;
            string idLivro = textBox5.Text;

            listaDeLivros.Add(new Livro() { nome = nomeLivro, autor = autorLivro, editora = nomeEditora, ano = anoLivro, id = idLivro });
            listaDeLivros.ToList();
            return listaDeLivros;
        }

        private void button2_Click(object sender, EventArgs e)
        {  
            //troca a tela
            this.Close();
        }
    }
}
