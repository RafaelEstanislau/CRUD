using CRUD_Livros;
using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml.Xsl;
using static System.Reflection.Metadata.BlobBuilder;
using static System.Windows.Forms.DataFormats;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CRUD_Livros
{
    public partial class Form1 : Form
    {
        public List<Livro> listaDeLivros = Singleton.Instance();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            RepositorySQL repobusca = new();
            repobusca.BuscarTodos(dataGridView1);
        }
        private void BotaoCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                Repository repo = new();

                var formulario2 = new Form2(null);
                formulario2.textBoxID.Enabled = false;
                formulario2.ShowDialog();
             
                RepositorySQL repo2 = new();
                repo2.Salvar(formulario2.Livro);
            }
            catch
            {
                MessageBox.Show("Erros ao cadastrar");
            }
            RepositorySQL repobusca = new();
            repobusca.BuscarTodos(dataGridView1);
        }
        private void BotaoEditar_Click(object sender, EventArgs e)
        {
            try
            {
                var id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

                RepositorySQL repoedita = new();
                Livro livroBuscado = new();

                livroBuscado = repoedita.BuscarPorID(id);
                var formulario2 = new Form2(livroBuscado);

                formulario2.textBoxID.Enabled = false;
                formulario2.ShowDialog();
                repoedita.Editar(livroBuscado);
                repoedita.BuscarTodos(dataGridView1);
            }
            catch
            {
                MessageBox.Show("Erro ao editar livro");
            }
        }
        
        /*public List<Livro> ListarLivros()
        {
            Repository repositorio = new();

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = repositorio.BuscarTodos();
            dataGridView1.ClearSelection();
            return listaDeLivros.ToList();
        }*/
        private void BotaoDeletar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count != 0)
                {
                    if (MessageBox.Show("Tem certeza que deseja deletar o livro? ", "Confirmação",
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        var id = dataGridView1.CurrentRow.Cells[0].Value;
                        RepositorySQL repoDeleta = new();
                        repoDeleta.Excluir(Convert.ToInt32(id));
                        repoDeleta.BuscarTodos(dataGridView1);
                    }
                    else
                    {
                        MessageBox.Show("Livro não foi excluído");
                    }
                }
                else
                {
                    MessageBox.Show("Não há livro selecionado");
                }
            }
            catch (Exception)
            {
              MessageBox.Show("Erro ao deletar");
            }
            
        }
    }
}