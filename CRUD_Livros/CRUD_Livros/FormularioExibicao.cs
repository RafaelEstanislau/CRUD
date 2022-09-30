using CRUD_Livros.DataAccessLibrary;
using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml.Xsl;
using static System.Reflection.Metadata.BlobBuilder;
using static System.Windows.Forms.DataFormats;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using CRUD_Livros.Domain;

namespace CRUD_Livros.UserInterface
{
    public partial class FormularioExibicao : Form
    {
        public List<Livro> listaDeLivros = Singleton.Instance();
        public FormularioExibicao()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ExibirLista();
        }
        private void BotaoCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                var formulario2 = new FormularioPreenchimento(null);
                formulario2.textBoxID.Enabled = false;
                formulario2.ShowDialog();

                if(formulario2.DialogResult == DialogResult.OK)
                {
                    RepositorySQL repo2 = new();
                    repo2.Salvar(formulario2.Livro);
                }
            }
            catch(Exception ex)
            {         
                    var message = $"{ex.Message} {ex.InnerException?.Message}";
                    MessageBox.Show(message);
            }
            ExibirLista();
        }
        private void BotaoEditar_Click(object sender, EventArgs e)
        {
            try
            {
                var id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[4].Value);
                RepositorySQL repoedita = new();
                Livro livroBuscado = new();

                livroBuscado = repoedita.BuscarPorID(id);
                var formulario2 = new FormularioPreenchimento(livroBuscado);
                formulario2.textBoxID.Enabled = false;
                formulario2.ShowDialog();
                if (formulario2.DialogResult == DialogResult.OK)
                {
                    repoedita.Editar(livroBuscado);
                    ExibirLista();
                }
                }
            catch (Exception ex)
            {
                var message = $"{ex.Message} {ex.InnerException?.Message}";
                MessageBox.Show(message);
            }
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
                        var id = dataGridView1.CurrentRow.Cells[4].Value;
                        RepositorySQL repoDeleta = new();
                        repoDeleta.Excluir(Convert.ToInt32(id));
                        MessageBox.Show("Livro excluído");
                        ExibirLista();
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
            catch (Exception ex)
            {
                var message = $"{ex.Message} {ex.InnerException?.Message}";
                MessageBox.Show(message);
            }
        }
        public void ExibirLista()
        {
            RepositorySQL exibicao = new();
            dataGridView1.DataSource = exibicao.BuscarTodos().ToList();
            dataGridView1.ClearSelection();
        }
    }
}