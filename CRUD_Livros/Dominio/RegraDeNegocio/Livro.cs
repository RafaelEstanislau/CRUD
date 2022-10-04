namespace CRUD_Livros.Dominio.RegraDeNegocio
{
    public class Livro
    {
        public string? nome { get; set; }
        public string? editora { get; set; }
        public string? autor { get; set; }
        public DateTime ano { get; set; }
        public int id { get; set; }
    }
}