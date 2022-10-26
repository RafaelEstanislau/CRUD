using CRUD_Livros.Infra.AcessoDeDados;
using CRUD_Livros.Dominio.RegraDeNegocio;
namespace CRUD_Livros.UserInterface
{
    public partial class FormularioExibicao : Form
    {
        private readonly IRepositorio _repositorio;
        public List<Livro> listaDeLivros = Singleton.Instance();
        public FormularioExibicao(IRepositorio repositorio)
        {
            InitializeComponent();
            _repositorio = repositorio;
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
                    _repositorio.Salvar(formulario2.Livro);
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
                var id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                Livro livroBuscado = new();

                livroBuscado = _repositorio.BuscarPorID(id);
                var formulario2 = new FormularioPreenchimento(livroBuscado);
                formulario2.textBoxID.Enabled = false;
                formulario2.ShowDialog();
                if (formulario2.DialogResult == DialogResult.OK)
                {
                    _repositorio.Editar(livroBuscado);
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
                        var id = dataGridView1.CurrentRow.Cells[0].Value;
                        _repositorio.Excluir(Convert.ToInt32(id));
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
            
            dataGridView1.DataSource = _repositorio.BuscarTodos().ToList();
            dataGridView1.ClearSelection();
        }
    }
}