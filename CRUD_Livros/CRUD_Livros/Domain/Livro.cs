namespace CRUD_Livros.Domain
{
    public class Livro
    {
        public string? nome { get; internal set; }
        public string? editora { get; internal set; }
        public string? autor { get; internal set; }
        public DateTime ano { get; internal set; }
        public int id { get; internal set; }

    }
}