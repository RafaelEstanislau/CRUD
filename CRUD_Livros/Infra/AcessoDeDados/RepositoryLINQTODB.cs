using LinqToDB;
using LinqToDB.DataProvider.SqlServer;
using System.Configuration;
using System.Data;
using CRUD_Livros.Dominio.RegraDeNegocio;
using CRUD_Livros.Infra.AcessoDeDados;

namespace Infra.AcessoDeDados
{
    public class RepositoryLINQTODB : IRepositorio
    {
        private static string BancoConexao()
        {
            return ConfigurationManager.ConnectionStrings["conexaoSql"].ConnectionString;
        }

        public void Salvar(Livro livro)
        {
            try
            {
                using var db = SqlServerTools.CreateDataConnection(BancoConexao());
                {
                    db.Insert(livro);

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar livro", ex);
            }
        }
        public Livro BuscarPorID(int id)
        {
            try
            {
                using var db = SqlServerTools.CreateDataConnection(BancoConexao());
                {
                    var livroBuscado = db.GetTable<Livro>()
                         .FirstOrDefault(l => l.id == id) ?? throw new Exception("Livro com id " + id + " não encontrado");
                      return livroBuscado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar por livros", ex);
            }
        }

        public List<Livro> BuscarTodos()
        {
            try
            {
                using var db = SqlServerTools.CreateDataConnection(BancoConexao());
                {
                    var listaDeLivros =
                    from livros in db.GetTable<Livro>()
                    select livros;
                    return listaDeLivros.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar lista de livros", ex);
            }
        }

        public void Editar(Livro livro)
        {
            try
            {
                using var db = SqlServerTools.CreateDataConnection(BancoConexao());
                {
                    db.Update(livro);

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao editar livro", ex);
            }
        }

        public void Excluir(int id)
        {
            try
            {
                using var db = SqlServerTools.CreateDataConnection(BancoConexao());
                {
                    db.GetTable<Livro>()
                        .Where(l => l.id == id)
                        .Delete();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir livro", ex);
            }
        }

    }
}
