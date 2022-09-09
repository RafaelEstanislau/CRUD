namespace CRUD_Livros
{
    public class Livro
    {
        public string nome { get; internal set; }
        public override string ToString()
        {
            return  "nome: " + nome;
        }
        /*public string autor { get; internal set; }
        public string editora { get; internal set; }
        public int ano { get; internal set; }*/



    }
}