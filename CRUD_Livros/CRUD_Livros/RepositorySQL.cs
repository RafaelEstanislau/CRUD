/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace CRUD_Livros
{
    public class RepositorySQL
    {
        SqlConnection conexao;
        SqlCommand comando;
        SqlDataAdapter da;
        SqlDataReader dr;

        string strSQL;

        public void Salvar(Livro livro)
        {
            try
            {
                conexao = new SqlConnection(@"Server = INVENT127; Database = Livros; User Id = sa; Password = sap@123; ");
                strSQL = "INSERT INTO CAD_LIVROS (TITULO, AUTOR, EDITORA) VALUES (@TITULO, @AUTOR, @EDITORA)";
                comando = new SqlCommand(strSQL, conexao);

                comando.Parameters.AddWithValue("@TITULO", livro.nome);
                comando.Parameters.AddWithValue("@AUTOR", livro.autor);
                comando.Parameters.AddWithValue("@EDITORA", livro.editora);

                conexao.Open();
                comando.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();
                conexao = null;
                comando = null;
            }
        }
        private void btnExibir_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new SqlConnection(@"Server = INVENT127; Database = Livros; User Id = sa; Password = sap@123; ");
                strSQL = "SELECT * FROM CAD_LIVROS";
                DataSet ds = new();
                da = new SqlDataAdapter(strSQL, conexao);

                conexao.Open();
                da.Fill(ds);

                dataGridView1.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();
                conexao = null;
            }
        }
        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new SqlConnection(@"Server = INVENT127; Database = Livros; User Id = sa; Password = sap@123; ");
                strSQL = "SELECT * FROM CAD_LIVROS WHERE ID = @ID";
                comando = new SqlCommand(strSQL, conexao);

                comando.Parameters.AddWithValue("@ID", txtID.Text);

                conexao.Open();
                dr = comando.ExecuteReader();

                while (dr.Read())
                {
                    txtTitulo.Text = (string)dr["titulo"];
                    txtEditora.Text = (string)dr["editora"];
                    txtAutor.Text = (string)dr["autor"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();
                conexao = null;
                comando = null;
            }
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new SqlConnection(@"Server = INVENT127; Database = Livros; User Id = sa; Password = sap@123; ");
                strSQL = "UPDATE CAD_LIVROS SET TITULO = @TITULO, AUTOR = @AUTOR, EDITORA = @EDITORA WHERE ID = @ID";
                comando = new SqlCommand(strSQL, conexao);

                comando.Parameters.AddWithValue("@ID", txtID.Text);
                comando.Parameters.AddWithValue("@TITULO", txtTitulo.Text);
                comando.Parameters.AddWithValue("@AUTOR", txtAutor.Text);
                comando.Parameters.AddWithValue("@EDITORA", txtEditora.Text);

                conexao.Open();
                comando.ExecuteNonQuery();

                MessageBox.Show("Livro editado");
                txtAutor.Text = string.Empty;
                txtTitulo.Text = string.Empty;
                txtEditora.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();
                conexao = null;
                comando = null;
            }
        }
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new SqlConnection(@"Server = INVENT127; Database = Livros; User Id = sa; Password = sap@123; ");
                strSQL = "DELETE CAD_LIVROS WHERE ID = @ID";
                comando = new SqlCommand(strSQL, conexao);
                comando.Parameters.AddWithValue("@ID", txtID.Text);


                conexao.Open();
                comando.ExecuteNonQuery();
                MessageBox.Show("Livro excluído");
                txtID.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();
                conexao = null;
                comando = null;
            }
        }
    }
}
}
*/
