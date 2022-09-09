namespace CRUD_Livros
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string recebeDados(object sender, EventArgs e)
        {
            string nomeLivro = textBox1.Text;
            return nomeLivro;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            List<Livro> listaDeLivros = new List<Livro>();
            
            Livro livro = new Livro();
            
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}