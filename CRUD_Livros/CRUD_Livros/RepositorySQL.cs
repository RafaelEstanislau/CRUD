using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace CRUD_Livros
{
    public class RepositorySQL : IRepositorySQL
    {
        SqlConnection conexao;
        SqlCommand comando;
        SqlDataAdapter da;
        SqlDataReader dr;

        string strSQL;


        public void Salvar(Livro livro)
        {
            using (conexao = new SqlConnection(@"Server = INVENT127; Database = Livros; User Id = sa; Password = sap@123; ")) { 
            {

            }
            try
            {
                strSQL = "INSERT INTO CAD_LIVROS VALUES (@TITULO, @AUTOR, @EDITORA, @LANCAMENTO)";
                comando = new SqlCommand(strSQL, conexao);

                comando.Parameters.AddWithValue("@TITULO", livro.nome);
                comando.Parameters.AddWithValue("@AUTOR", livro.autor);
                comando.Parameters.AddWithValue("@EDITORA", livro.editora);
                comando.Parameters.AddWithValue("@LANCAMENTO", livro.ano);

                conexao.Open();
                comando.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            }
        }
       public void BuscarTodos(DataGridView grid)
        {
            using (conexao = new SqlConnection(@"Server = INVENT127; Database = Livros; User Id = sa; Password = sap@123; ")) { 
            {
            }
            try
            {
                strSQL = "SELECT * FROM CAD_LIVROS";
                DataSet ds = new();
                da = new SqlDataAdapter(strSQL, conexao);

                conexao.Open();
                da.Fill(ds);
                
                grid.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            }
        }
        public Livro BuscarPorID(int id)
        {
            Livro livroBuscado = new();
            using (conexao = new SqlConnection(@"Server = INVENT127; Database = Livros; User Id = sa; Password = sap@123; ")) { 
            {
            }
            try
            {
                strSQL = "SELECT * FROM CAD_LIVROS WHERE ID = @ID";
                comando = new SqlCommand(strSQL, conexao);
                comando.Parameters.AddWithValue("@ID", id);
                conexao.Open();
                dr = comando.ExecuteReader();
                while (dr.Read())
                {
                    livroBuscado.id = Convert.ToInt32(dr["id"]);
                    livroBuscado.nome = (string)dr["titulo"];
                    livroBuscado.editora = (string)dr["editora"];
                    livroBuscado.autor = (string)dr["autor"];
                    livroBuscado.ano = DateTime.Parse(dr["lancamento"].ToString());
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            }
            return livroBuscado;
        }
        public void Editar(Livro livro)
        {
            using (conexao = new SqlConnection(@"Server = INVENT127; Database = Livros; User Id = sa; Password = sap@123; ")) { 
            {

            }
            try
            {
                strSQL = "UPDATE CAD_LIVROS SET TITULO = @TITULO, AUTOR = @AUTOR, EDITORA = @EDITORA, LANCAMENTO = @LANCAMENTO WHERE ID = @ID";
                comando = new SqlCommand(strSQL, conexao);

                comando.Parameters.AddWithValue("@ID", livro.id);
                comando.Parameters.AddWithValue("@TITULO", livro.nome);
                comando.Parameters.AddWithValue("@AUTOR", livro.autor);
                comando.Parameters.AddWithValue("@EDITORA", livro.editora);
                comando.Parameters.AddWithValue("@LANCAMENTO", livro.ano);

                conexao.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            }
        }
        public void Excluir(int id)
        {
             
            
            
            try
            {
                conexao = new SqlConnection(@"Server = INVENT127; Database = Livros; User Id = sa; Password = sap@123; ");
                
                strSQL = "DELETE CAD_LIVROS WHERE ID = @ID";
                comando = new SqlCommand(strSQL, conexao);
                comando.Parameters.AddWithValue("@ID", id);

                conexao.Open();
                comando.ExecuteNonQuery();
                MessageBox.Show("Livro excluído");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conexao.Close(); 
            }
        
        }
    }
}
