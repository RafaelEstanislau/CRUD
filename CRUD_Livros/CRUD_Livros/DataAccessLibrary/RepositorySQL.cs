using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using System.Linq.Expressions;
using CRUD_Livros.Domain;

namespace CRUD_Livros.DataAccessLibrary
{
    public class RepositorySQL : IRepositorySQL
    {

        SqlDataAdapter da;
        SqlDataReader dr;
        private SqlConnection? sqlConexao;
        private SqlConnection conexaoComBanco()
        {
            sqlConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["conexaoSql"].ConnectionString);
            sqlConexao.Open();

            return sqlConexao;
        }

        public void Salvar(Livro livro)
        {
            using (var conexao = conexaoComBanco())
            {
                using (var cmd = conexao.CreateCommand())
                {
                    try
                    {
                        cmd.CommandText = "INSERT INTO CAD_LIVROS VALUES (@TITULO, @AUTOR, @EDITORA, @LANCAMENTO)";

                        cmd.Parameters.AddWithValue("@TITULO", livro.nome);
                        cmd.Parameters.AddWithValue("@AUTOR", livro.autor);
                        cmd.Parameters.AddWithValue("@EDITORA", livro.editora);
                        cmd.Parameters.AddWithValue("@LANCAMENTO", livro.ano);

                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        public void BuscarTodos(DataGridView grid)
        {
            using (var conexao = conexaoComBanco())
            {
                using (var cmd = conexao.CreateCommand())
                {
                    try
                    {
                        cmd.CommandText = "SELECT * FROM CAD_LIVROS";
                        DataSet ds = new();
                        da = new SqlDataAdapter(cmd.CommandText, conexao);
                        da.Fill(ds);
                        grid.DataSource = ds.Tables[0];
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        public Livro BuscarPorID(int id)
        {
            Livro livroBuscado = new();
            using (var conexao = conexaoComBanco())
            {
                using (var cmd = conexao.CreateCommand())
                {
                    try
                    {
                        cmd.CommandText = "SELECT * FROM CAD_LIVROS WHERE ID = @ID";

                        cmd.Parameters.AddWithValue("@ID", id);

                        dr = cmd.ExecuteReader();
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
        }
        public void Editar(Livro livro)
        {
            using (var conexao = conexaoComBanco())
            {
                using (var cmd = conexao.CreateCommand())
                {
                    try
                    {
                        cmd.CommandText = "UPDATE CAD_LIVROS SET TITULO = @TITULO, AUTOR = @AUTOR, EDITORA = @EDITORA, LANCAMENTO = @LANCAMENTO WHERE ID = @ID";

                        cmd.Parameters.AddWithValue("@ID", livro.id);
                        cmd.Parameters.AddWithValue("@TITULO", livro.nome);
                        cmd.Parameters.AddWithValue("@AUTOR", livro.autor);
                        cmd.Parameters.AddWithValue("@EDITORA", livro.editora);
                        cmd.Parameters.AddWithValue("@LANCAMENTO", livro.ano);

                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        public void Excluir(int id)
        {
            using (var conexao = conexaoComBanco())
            {
                using (var cmd = conexao.CreateCommand())
                {
                    try
                    {
                        cmd.CommandText = "DELETE CAD_LIVROS WHERE ID = @ID";
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Livro excluído");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}
