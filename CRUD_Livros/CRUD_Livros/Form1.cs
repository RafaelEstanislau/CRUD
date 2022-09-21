using CRUD_Livros;
using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml.Xsl;
using static System.Windows.Forms.DataFormats;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CRUD_Livros
{
    public partial class Form1 : Form
    {
        public List<Livro> listaDeLivros = Singleton.Instance();
        public List<Livro> livrosAntigos = new();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int comprimento = listaDeLivros.Count;
            for (int i = 0; i < comprimento; i++)
            {
                if(listaDeLivros[i] == null)
                {
                    BotaoEditar.Enabled = false;
                    BotaoDeletar.Enabled = false;
                }
            }
        }

        private void BotaoCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                Repository repo = new();
                repo.Save();
            }
            catch
            {
                MessageBox.Show("Erros ao cadastrar");
            }

            ListarLivros();
        }

        private void BotaoEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("Não há livro para editar");
                }
                else
                {
                    if (!dataGridView1.CurrentRow.Selected)
                    {
                        MessageBox.Show("Escolha um livro para editar");
                    }
                    else
                    {
                        var livroSelecionadoIndex = dataGridView1.CurrentRow.Index;
                        var livroSelecionado = dataGridView1.Rows[livroSelecionadoIndex].DataBoundItem as Livro;

                        var formulario2 = new Form2(livroSelecionado);
                        formulario2.textBoxID.Enabled = false;
                        formulario2.ShowDialog();

                        ListarLivros();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Erro ao editar livro");
            }
        }
        
        public List<Livro> ListarLivros()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = listaDeLivros.ToList();
            dataGridView1.ClearSelection();
            return listaDeLivros.ToList();
        }

        private void BotaoDeletar_Click(object sender, EventArgs e)
        {
            try 
            {
                if (dataGridView1.SelectedRows.Count != 0)
                {
                    if (MessageBox.Show("Tem certeza que deseja deletar o livro? ", "Confirmação",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                            int removeIndex = dataGridView1.CurrentRow.Index;
                            listaDeLivros.RemoveAt(removeIndex);
                    }
                    else
                    {
                        MessageBox.Show("Livro não foi deletado");
                    }
                }
                else
                {
                    MessageBox.Show("Selecione um livro para deletar");
                }

                var bindingList = new BindingList<Livro>(listaDeLivros);
                dataGridView1.DataSource = bindingList;

                dataGridView1.Update();
                dataGridView1.Refresh();
                dataGridView1.ClearSelection();
            }
            catch
            {
                MessageBox.Show("Erro ao deletar livro");
            }
        }
    }
}