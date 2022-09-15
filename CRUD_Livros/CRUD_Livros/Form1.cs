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

        private void BotaoCadastrar_Click(object sender, EventArgs e)
        {
            var trocaTela = new Form2();
            trocaTela.ShowDialog();

            ListarLivros();
        }

        private void BotaoEditar_Click(object sender, EventArgs e)
        {
            index = this.dataGridView1.CurrentRow.Index;
            dataGridView1.CurrentRow.Selected = true;



            var trocaTexto = new Form2();
           
            trocaTexto.textBox1.Text = dataGridView1.SelectedCells[0].Value.ToString();
            trocaTexto.textBox2.Text = dataGridView1.SelectedCells[1].Value.ToString();
            trocaTexto.textBox3.Text = dataGridView1.SelectedCells[2].Value.ToString();
            trocaTexto.dateTimePicker1.Text = dataGridView1.SelectedCells[3].Value.ToString();
            trocaTexto.textBox5.Text = dataGridView1.SelectedCells[4].Value.ToString();
            trocaTexto.ShowDialog();

            ListarLivros();

        }
        public List<Livro> ListarLivros()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = listaDeLivros.ToList();
            dataGridView1.ClearSelection();
            return listaDeLivros.ToList();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = dataGridView1.CurrentRow.Index;

        }

        private void BotaoDeletar_Click(object sender, EventArgs e)
        {
           
            removeLista();
            var bindingList = new BindingList<Livro>(listaDeLivros);

            dataGridView1.DataSource = bindingList;
            dataGridView1.Update();
            dataGridView1.Refresh();



        }
        public List<Livro> removeLista()
        {
            
            if (dataGridView1.SelectedRows.Count != 0)
            {
                string nomeCelula = this.dataGridView1.SelectedCells[0].Value.ToString();
                int removeIndex = this.dataGridView1.CurrentRow.Index;
                if (MessageBox.Show("Tem certeza que deseja deletar o livro: " +nomeCelula, "Confirma��o",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    listaDeLivros.RemoveAt(removeIndex);
                    
                }
                else
                {
                    MessageBox.Show("Livro n�o foi deletado");
                  

                }


            }
            else
            {
                MessageBox.Show("Selecione um livro para deletar");
            }
            return listaDeLivros;
        }
    }
}