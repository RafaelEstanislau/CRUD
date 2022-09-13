using CRUD_Livros;
using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CRUD_Livros
{
    public partial class Form1 : Form
    {
        public static List<Livro> listaDeLivros = new();
        public static int index;


        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e) //Cadastro
        {
            var trocaTela = new Form2();
            trocaTela.ShowDialog();


            //dataGridView1.DataSource = form2.CadastraLivro();
            ListarLivros();
        }

        private void button2_Click(object sender, EventArgs e) //Editar
        {
            index = this.dataGridView1.CurrentRow.Index;
            dataGridView1.CurrentRow.Selected = true;



            var trocaTexto = new Form2();

            trocaTexto.textBox1.Text = dataGridView1.SelectedCells[0].Value.ToString();
            trocaTexto.textBox2.Text = dataGridView1.SelectedCells[1].Value.ToString();
            trocaTexto.textBox3.Text = dataGridView1.SelectedCells[2].Value.ToString();
            trocaTexto.textBox4.Text = dataGridView1.SelectedCells[3].Value.ToString();
            trocaTexto.textBox5.Text = dataGridView1.SelectedCells[4].Value.ToString();
            trocaTexto.ShowDialog();

            ListarLivros();

        }
        public List<Livro> ListarLivros()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = listaDeLivros.ToList();
            return listaDeLivros.ToList();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = dataGridView1.CurrentRow.Index;

        }

        private void button3_Click(object sender, EventArgs e)
        {

            //int rowIndex = dataGridView1.CurrentRow.Index;
            // this.dataGridView1.Rows.RemoveAt(rowIndex);

            var bindingList = new BindingList<Livro>(listaDeLivros);

            dataGridView1.DataSource = bindingList;

            if (dataGridView1.SelectedRows.Count > 0)
            {
                dataGridView1.Rows.RemoveAt(this.dataGridView1.CurrentRow.Index);
            }
            else
            {
                MessageBox.Show("Selecione um livro para deletar");
            }

        }





















        /* public List<Livro>RetornaLivro()
         {
             if (dataGridView1.SelectedCells.Count > 0)
             {
                 string nomeGrid = dataGridView1.SelectedCells[0].Value.ToString();
                 string editoraGrid = dataGridView1.SelectedCells[1].Value.ToString();
                 string autorGrid = dataGridView1.SelectedCells[2].Value.ToString();
                 string anoGrid = dataGridView1.SelectedCells[3].Value.ToString();
                 string idGrid = dataGridView1.SelectedCells[4].Value.ToString();
                 retornoDeLivros.Add(new Livro() { nome = nomeGrid, autor = autorGrid, editora = editoraGrid, ano = anoGrid, id = idGrid });
             }


             return retornoDeLivros;
         }*/
    }
}