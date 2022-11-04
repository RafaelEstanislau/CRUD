using FluentValidation;
namespace CRUD_Livros.Dominio.RegraDeNegocio
{
    public class Validacao : AbstractValidator<Livro>
    {
        public Validacao()
        { 
            RuleFor(livro => livro.titulo).NotEmpty();
            RuleFor(livro => livro.editora).NotEmpty();
            RuleFor(livro => livro.autor).NotEmpty();
            RuleFor(livro => livro.lancamento).NotEmpty().ExclusiveBetween(DateTime.MinValue, DateTime.Today);
        }
    }
}
