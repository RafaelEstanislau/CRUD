using FluentMigrator;
namespace CRUD_Livros.Migrations
{
    [Migration(051020221126)]
    public class Migration051022_1007AdicionaColunas : Migration
    {
        public override void Up()
        {
        try
        {
            Create.Table("Livro2")
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
                Delete.Table("Livro2");
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar tabela", ex); ;
            }
        }
    }
}