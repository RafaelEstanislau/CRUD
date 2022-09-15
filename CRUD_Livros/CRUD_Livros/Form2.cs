using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_Livros
{
    public partial class Form2 : Form
    {
        public static List<Livro> livrosAntigos = new();
        public Form2()
        {
            InitializeComponent();
           
        }
        
        private void CadastraFormulario_Click(object sender, EventArgs e) 
        {
            CadastraLivro();
        }
        public void CadastraLivro()
        {
            string nomeLivro = "";
            string nomeEditora =  "";
            string autorLivro = "" ;
            string idLivro = "";
            string anoLivro = "";

            if (textBox1.Text.Equals("") || textBox2.Text.Equals("")|| textBox3.Text.Equals("") || 
               textBox5.Text.Equals(""))
            {
                MessageBox.Show("Nenhum campo pode ser vazio!");
            }
            
            else if (dateTimePicker1.Value > DateTime.Now)
            {
                MessageBox.Show("Insira uma data válida anterior à hoje!");

            }
            else if (!verificaID(textBox5.Text))
            {
                MessageBox.Show("Não é possível cadastrar um livro com ID repetido");
            }
            else
            {
                nomeLivro = textBox1.Text;
                nomeEditora = textBox2.Text;
                autorLivro = textBox3.Text;
                idLivro = textBox5.Text;
                anoLivro = dateTimePicker1.Value.ToString("dd/MM/yyyy");
                Form1.listaDeLivros.Add(new Livro()
                {
                    nome = nomeLivro,
                    autor = autorLivro,
                    editora = nomeEditora,
                    ano = Convert.ToDateTime(anoLivro),
                    id = idLivro
                });
                MessageBox.Show("Livro cadastrado com sucesso!");
                this.Close();
            }
    
        }

        private void VoltaFormulario_Click(object sender, EventArgs e)
        {  
            
            this.Close();
        }

        private void EditaFormulario_Click(object sender, EventArgs e) 
            
        {

            
                EditaLivro();
                
                Form1 listagem = new();
                listagem.ListarLivros();
            

        }
        public List<Livro> EditaLivro()
        {
            
            Livro editado = new()
            {
                nome = textBox1.Text,
                autor = textBox2.Text,
                editora = textBox3.Text,
                ano = dateTimePicker1.Value.Date,
                id = textBox5.Text
            };
            string idAntigo = Form1.listaDeLivros[Form1.index].id;


            bool idMudou = idAntigo != editado.id;

            if (idMudou)
            {
                idAntigo = editado.id;
                MessageBox.Show("O ID de um livro não pode ser alterado!");
            }
            else
            {
                Form1.listaDeLivros[Form1.index] = editado;
                MessageBox.Show("Livro atualizado!");
                this.Close();
            }

            return Form1.listaDeLivros;
        }

        public bool verificaID(string texto)
        {
            
            bool idUnico = true;
            int comprimento = Form1.listaDeLivros.Count;
            for(int i = 0; i < comprimento; i++)
            {
                if(Form1.listaDeLivros[i].id == textBox5.Text)
                {
                   
                       idUnico = false;
                       
                }
                
            }
            return idUnico;
        }
    }
}
