namespace CRUD_Livros
{
    public class Singleton
    {
        private static List<Livro> instance;

        public static List<Livro> Instance()
        {
                instance ??= new List<Livro>();

            return instance;
        }

        public static int ProximoId(int idAtual)
        {
            return idAtual == 0 ? idAtual = 1 : idAtual = ++idAtual;
        }
    }
}
