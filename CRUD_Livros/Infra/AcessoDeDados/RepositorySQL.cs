using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using CRUD_Livros.Dominio.RegraDeNegocio;

namespace CRUD_Livros.Infra.AcessoDeDados
{
    public class RepositorySQL : IRepositorio
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

                        cmd.Parameters.AddWithValue("@TITULO", livro.titulo);
                        cmd.Parameters.AddWithValue("@AUTOR", livro.autor);
                        cmd.Parameters.AddWithValue("@EDITORA", livro.editora);
                        cmd.Parameters.AddWithValue("@LANCAMENTO", livro.lancamento);

                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Não é possível salvar no banco de dados " , ex);
                    }
                }
            }
        }
        public List<Livro> BuscarTodos()
        {
            var lista = new List<Livro>();
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
                        DataTable tabelaTodos = ds.Tables[0];
                        
                        lista = (from DataRow dr in tabelaTodos.Rows
                                 select new Livro()
                                 {
                                     id = Convert.ToInt32(dr["id"]),
                                     titulo = dr["titulo"].ToString(),
                                     autor = dr["autor"].ToString(),
                                     editora = dr["editora"].ToString(),
                                     lancamento = DateTime.Parse(dr["lancamento"].ToString()),
                                 }).ToList();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Não existe conexão com o banco de dados", ex);
                    }
                }
            }
            return lista;
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
                            livroBuscado.titulo = (string)dr["titulo"];
                            livroBuscado.editora = (string)dr["editora"];
                            livroBuscado.autor = (string)dr["autor"];
                            livroBuscado.lancamento = DateTime.Parse(dr["lancamento"].ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("ID não encontrado", ex);
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
                        cmd.Parameters.AddWithValue("@TITULO", livro.titulo);
                        cmd.Parameters.AddWithValue("@AUTOR", livro.autor);
                        cmd.Parameters.AddWithValue("@EDITORA", livro.editora);
                        cmd.Parameters.AddWithValue("@LANCAMENTO", livro.lancamento);

                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Erro ao editar", ex);
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
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Erro ao excluir", ex);
                    }
                }
            }
        }
    }
}
