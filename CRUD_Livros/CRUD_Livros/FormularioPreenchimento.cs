using CRUD_Livros.Dominio.RegraDeNegocio;

namespace CRUD_Livros.UserInterface
{
    public partial class FormularioPreenchimento : Form
    {
        public Livro Livro { get; set; }
        public FormularioPreenchimento(Livro livro)
        {
            InitializeComponent();

            if (livro == null)
            {
                Livro = new Livro();
            }
            else
            {
                textBoxNome.Text = livro.titulo;
                textBoxEditora.Text = livro.editora;
                textBoxAutor.Text = livro.autor;
                dateTimePicker1.Text = livro.lancamento.ToString();
                Livro = livro;
            }
        }
        private void SalvaFormulario_Click(object sender, EventArgs e) 
        {
            try
            {
                Livro.titulo = textBoxNome.Text;
                Livro.editora = textBoxEditora.Text;
                Livro.autor = textBoxAutor.Text;
                Livro.lancamento = dateTimePicker1.Value;
                if (Validacao.ValidacaoDeCampos(Livro) == true)
                {
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    DialogResult = DialogResult.Cancel;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void VoltaFormulario_Click(object sender, EventArgs e)
        {  
            this.Close();
        }
        private void FormularioPreenchimento_Load(object sender, EventArgs e)
        {

        }
    }
}
