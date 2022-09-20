﻿using System.Data;
using System.Windows.Forms;
using System.Xml.Xsl;

namespace CRUD_Livros
{
    public partial class Form2 : Form
    {
        public Livro Livro { get; set; }
        public Form2(Livro livro)
        {
            InitializeComponent();

            if (livro == null)
            {
                Livro = new Livro();
            }
            else
            {
                textBoxID.Text = livro.id.ToString();
                textBoxNome.Text = livro.nome;
                textBoxEditora.Text = livro.editora;
                textBoxAutor.Text = livro.autor;
                dateTimePicker1.Text = livro.ano.ToString();
                Livro = livro;
            }
        }
        
        private void CadastraFormulario_Click(object sender, EventArgs e) 
        {
            try
            {
                ValidarCampos();

                if (Livro.id == 0)
                {
                    Livro.nome = textBoxNome.Text;
                    Livro.editora = textBoxEditora.Text;
                    Livro.autor = textBoxAutor.Text;
                    Livro.ano = dateTimePicker1.Value;
                }
                else
                {
                    Livro.nome = textBoxNome.Text;
                    Livro.editora = textBoxEditora.Text;
                    Livro.autor = textBoxAutor.Text;
                    Livro.ano = dateTimePicker1.Value;
                }
                DialogResult = DialogResult.OK;
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



        public void ValidarCampos()
        {
            if (textBoxNome.Text == string.Empty)
            {
                throw new Exception("Campo Nome deve ser informado");
            }
            if (textBoxEditora.Text == string.Empty)
            {
                throw new Exception("Campo Editora deve ser informado");
            }
            if (textBoxAutor.Text == string.Empty)
            {
                throw new Exception("Campo Autor deve ser informado");
            }
            if (dateTimePicker1.Value > DateTime.Now)
            {
                throw new Exception("Insira uma data válida anterior à hoje!");
            }
        }
    }
}