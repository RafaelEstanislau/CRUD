using CRUD_Livros;
using System.Collections;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CRUD_Livros
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {
            var form2 = new Form2();
            dataGridView1.DataSource = form2.CadastraLivro();
        }

        private void button1_Click_1(object sender, EventArgs e) //Cadastro
        {
            var trocaTela = new Form2();
            trocaTela.Show();
        }

        private void button2_Click(object sender, EventArgs e) //Busca
        {
        
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}