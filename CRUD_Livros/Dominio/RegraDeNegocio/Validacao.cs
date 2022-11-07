using FluentValidation;
using static LinqToDB.Common.Configuration;

namespace CRUD_Livros.Dominio.RegraDeNegocio
{
    public class Validacao : AbstractValidator<Livro>
    {
        public Validacao()
        {
            var dataMinimaValida = new DateTime(1860, 1, 1);
            RuleFor(livro => livro.titulo).NotEmpty();
            RuleFor(livro => livro.editora).NotEmpty();
            RuleFor(livro => livro.autor).NotEmpty();
            RuleFor(livro => livro.lancamento).NotEmpty()
                .ExclusiveBetween(dataMinimaValida, DateTime.Today);
        }
    }
}
