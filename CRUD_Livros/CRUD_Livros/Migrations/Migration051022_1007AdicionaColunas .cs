using FluentMigrator;
namespace CRUD_Livros.Migrations
{
    [Migration(051020221125)]
    public class Migration051022_1008AdicionaNovaColunaPropriedade : Migration
    {
        public override void Up()
        {
        try
        {
            Create.Table("Livro")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Titulo").AsString().NotNullable()
                .WithColumn("Autor").AsString().NotNullable()
                .WithColumn("Editora").AsString().NotNullable()
                .WithColumn("Lancamento").AsDateTime().NotNullable()
                ;
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao criar tabela", ex);
        }
            
        }
        public override void Down()
        {
            try
            {
                Delete.Table("Livro");
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar tabela", ex); ;
            }
        }
    }
}