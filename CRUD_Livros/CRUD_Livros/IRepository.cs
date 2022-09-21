using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Livros
{
    public interface IRepository<Livro>
    {
        void Save();
        void Delete();
    }
}
