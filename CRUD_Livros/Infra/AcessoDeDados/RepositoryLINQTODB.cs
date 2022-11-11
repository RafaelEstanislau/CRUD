﻿using LinqToDB;
using LinqToDB.DataProvider.SqlServer;
using System.Configuration;
using System.Data;
using CRUD_Livros.Dominio.RegraDeNegocio;
using CRUD_Livros.Infra.AcessoDeDados;
using Dominio.RegraDeNegocio;
using Dominio.Constantes;

namespace Infra.AcessoDeDados
{
    public class RepositoryLINQTODB : IRepositorio
    {
        private static string BancoConexao()
        {
            return ConfigurationManager.ConnectionStrings["conexaoSql"].ConnectionString;
        }

        public int Salvar(Livro livro)
        {
            try
            {
                using var db = SqlServerTools.CreateDataConnection(BancoConexao());
                {
                    var idDoLivroAdicionado = db.InsertWithInt32Identity(livro);
                    return idDoLivroAdicionado;

                }
            }
            catch (Exception ex)
            {
                throw new Exception(MensagensDeTela.MENSAGEM_ERRO_ADICIONAR_LIVRO, ex);
            }
        }
        public Livro BuscarPorID(int id)
        {
            try
            {
                using var db = SqlServerTools.CreateDataConnection(BancoConexao());
                {
                    var livroBuscado = db.GetTable<Livro>()
                         .FirstOrDefault(l => l.id == id) ?? throw new Exception(MensagensDeTela.FalharAoBuscarPorID(id));
                    return livroBuscado;
                }
            }
            catch (Exception ex)
            {
                throw new NotFoundException(MensagensDeTela.MENSAGEM_ERRO_BUSCAR_LIVRO, ex);
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
                throw new NotFoundException(MensagensDeTela.MENSAGEM_ERRO_CARREGAR_LIVROS, ex);
            }
        }

        public Livro Editar(Livro livro)
        {
            try
            {
                using var db = SqlServerTools.CreateDataConnection(BancoConexao());
                {
                    db.Update(livro);
                    return BuscarPorID(livro.id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MensagensDeTela.MENSAGEM_ERRO_EDITAR_LIVRO, ex);
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
                throw new Exception(MensagensDeTela.MENSAGEM_ERRO_EXCLUIR_LIVRO, ex);
            }
        }

    }
}
