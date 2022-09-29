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
            RepositorySQL repobusca = new();
            repobusca.BuscarTodos(dataGridView1);
        }
        private void BotaoCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                Repository repo = new();

                var formulario2 = new FormularioPreenchimento(null);
                formulario2.textBoxID.Enabled = false;
                formulario2.ShowDialog();

                if(formulario2.DialogResult == DialogResult.OK)
                {
                    RepositorySQL repo2 = new();
                    repo2.Salvar(formulario2.Livro);
                }
                
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
                var formulario2 = new FormularioPreenchimento(livroBuscado);
                formulario2.textBoxID.Enabled = false;
                formulario2.ShowDialog();
                if (formulario2.DialogResult == DialogResult.OK)
                {
                    repoedita.Editar(livroBuscado);
                    repoedita.BuscarTodos(dataGridView1);
                }

                }
            catch
            {
                MessageBox.Show("Erro ao editar livro");
            }
        }
        private void BotaoDeletar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count != 0)
                {
                    if (MessageBox.Show("Tem certeza que deseja deletar o livro? ", "Confirma��o",
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        var id = dataGridView1.CurrentRow.Cells[0].Value;
                        RepositorySQL repoDeleta = new();
                        repoDeleta.Excluir(Convert.ToInt32(id));
                        repoDeleta.BuscarTodos(dataGridView1);
                    }
                    else
                    {
                        MessageBox.Show("Livro n�o foi exclu�do");
                    }
                }
                else
                {
                    MessageBox.Show("N�o h� livro selecionado");
                }
            }
            catch (Exception)
            {
              MessageBox.Show("Erro ao deletar");
            }
            
        }
    }
}