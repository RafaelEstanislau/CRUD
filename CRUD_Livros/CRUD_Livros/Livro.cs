namespace CRUD_Livros
{
    public class Livro
    {
        public string nome { get; internal set; }
        public string editora { get; internal set; }
        public string autor { get; internal set; }
        public DateTime ano { get; internal set; }
        private static int IDNumber { get; set; }
        public int id { get; internal set; }

        public Livro()
        {
            IDNumber++;
            this.id = IDNumber;
        }



        
    }
}