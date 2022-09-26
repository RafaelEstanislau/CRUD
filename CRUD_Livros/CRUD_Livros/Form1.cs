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

                var formulario2 = new Form2(null);
                formulario2.textBoxID.Enabled = false;
                formulario2.ShowDialog();
                if (formulario2.DialogResult == DialogResult.OK)
                {
                    if (formulario2.Livro.id == 0)
                    {
                        var ultimoId = 0;

                        formulario2.Livro.id = Singleton.ProximoId(ultimoId);
                        repo.Salvar(formulario2.Livro);
                    }
                    else
                    {
                        repo.Salvar(formulario2.Livro);
                    }
                }
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
                        var livroSelecionadoIndex = dataGridView1.CurrentRow.Index +1;

                        Repository repositorio = new();
                        Livro livroEditado = new();

                        var idLivroBuscado = repositorio.BuscarPorID(livroSelecionadoIndex);
                        livroEditado = repositorio.Editar(idLivroBuscado);
                        
                        Form2 formulario2 = new(livroEditado);
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
            Repository repositorio = new();

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = repositorio.BuscarTodos();
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
                        int removeIndex = dataGridView1.CurrentRow.Index + 1;

                        Repository repositorio = new();
                        Livro livroASerDeletado = new();

                        var livroASerDeletadoID = repositorio.BuscarPorID(removeIndex);
                        livroASerDeletado = listaDeLivros.FirstOrDefault(l => l.id == livroASerDeletadoID)
                                            ?? throw new Exception($"Livro não encontrado com ID {livroASerDeletadoID}");

                        repositorio.Deletar(livroASerDeletado);
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