
using LinqToDB.Mapping;

namespace CRUD_Livros.Dominio.RegraDeNegocio
{
    public class Livro
    {
        [PrimaryKey, Identity]
        public int id { get; set; }
        public string? titulo { get; set; }
        public string? editora { get; set; }
        public string? autor { get; set; }
        public DateTime lancamento { get; set; }
        
        
    }
}