using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.DataProvider.SqlServer;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CRUD_Livros.Dominio.RegraDeNegocio;
using CRUD_Livros.Infra.AcessoDeDados;

namespace Infra.AcessoDeDados
{
    public class RepositoryLINQTODB : IRepositorio
    {
        private static SqlConnection? sqlConexao;

        private static string BancoConexao()
        {
            return ConfigurationManager.ConnectionStrings["conexaoSql"].ConnectionString;
        }

        public Livro BuscarPorID(int id)
        {
            try
            {
                using var db = SqlServerTools.CreateDataConnection(BancoConexao());
                {
                    var livroBuscado = db.GetTable<Livro>()
                         .FirstOrDefault(u => u.id == id) ?? throw new Exception("Usuário com id" +id+ "não encontrado");
                      return livroBuscado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar lista de usuários", ex);
            }
        }

        public List<Livro> BuscarTodos()
        {
            try
            {
                using var db = SqlServerTools.CreateDataConnection(BancoConexao());
                {
                    var listaDeLivros =
                    from usuarios in db.GetTable<Livro>()
                    select usuarios;
                    return listaDeLivros.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar lista de usuários", ex);
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
                throw new Exception("Erro ao carregar lista de usuários", ex);
            }
        }

        public void Excluir(int id)
        {
            try
            {
                using var db = SqlServerTools.CreateDataConnection(BancoConexao());
                {
                    db.GetTable<Livro>()
                        .Where(u => u.id == id)
                        .Delete();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar lista de usuários", ex);
            }
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
                throw new Exception("Erro ao carregar lista de usuários", ex);
            }
        }
    }
}
