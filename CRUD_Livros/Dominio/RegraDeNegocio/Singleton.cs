﻿namespace CRUD_Livros.Domain
{
    public class Singleton
    {
        private static List<Livro> listaSingleton;

        public static List<Livro> Instance()
        {
            listaSingleton ??= new List<Livro>();

            return listaSingleton;
        }
        public static int ProximoId(int idAtual)
        {
            if(listaSingleton.Count != 0)
            {
                idAtual = listaSingleton.Last().id;
            }

            return ++idAtual;
        }
    }
}