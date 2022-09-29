using System.Data;
using System.Windows.Forms;
using System.Xml.Xsl;
using CRUD_Livros.Domain;

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
               
                textBoxNome.Text = livro.nome;
                textBoxEditora.Text = livro.editora;
                textBoxAutor.Text = livro.autor;
                dateTimePicker1.Text = livro.ano.ToString();
                Livro = livro;
            }
        }
        private void SalvaFormulario_Click(object sender, EventArgs e) 
        {
            try
            {
                Livro.nome = textBoxNome.Text;
                Livro.editora = textBoxEditora.Text;
                Livro.autor = textBoxAutor.Text;
                Livro.ano = dateTimePicker1.Value;
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
