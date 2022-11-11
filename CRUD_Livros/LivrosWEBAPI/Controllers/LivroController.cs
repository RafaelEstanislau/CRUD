
using CRUD_Livros.Infra.AcessoDeDados;
using CRUD_Livros.Dominio.RegraDeNegocio;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using FluentValidation;
using Dominio.RegraDeNegocio;


namespace LivrosWEBAPI.Controllers
{
    [Route("livros")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly IRepositorio _livroServico;

        public LivroController(IRepositorio livroServico)
        {
            _livroServico = livroServico;
        }
        [HttpPost]
        public IActionResult CriarLivros([FromBody] Livro livroASerAdicionado)
        {

            Validacao validator = new();
            validator.ValidateAndThrow(livroASerAdicionado);
            var id = _livroServico.Salvar(livroASerAdicionado);
            livroASerAdicionado.id = id;
            return Created($"livro/{livroASerAdicionado.id}", livroASerAdicionado);


        }

        [HttpGet]
        public IActionResult BuscarTodos()
        {
            var livrosBuscados = _livroServico.BuscarTodos();
            if (livrosBuscados == null)
            {
                return NotFound();
            }

            return Ok(livrosBuscados);
        }
        [HttpGet("{id}")]
        public IActionResult ObterLivroPorId(int id)
        {
            var livro = _livroServico.BuscarPorID(id);
            if (livro == null)
            {
                return NotFound();
            }
            return Ok(livro);
        }

        [HttpPut("{id}")]
        public IActionResult EditarLivro(Livro livroASerEditado)
        {
            
                Livro livroEditado = new();
                if (livroASerEditado == null)
                {
                    NotFound();
                }
                else
                {
                    Validacao validator = new();
                    validator.ValidateAndThrow(livroASerEditado);
                    livroEditado = _livroServico.Editar(livroASerEditado);

                }

                return Ok(livroEditado);
            
            

               // var detalheErroDeEdicao = $"Não foi possível editar o livro de id {livroASerEditado.id}";
              
                //return Problem(detalheErroDeEdicao, HttpContext.Request.Path, (int)HttpStatusCode.InternalServerError, ex.Message);
            

        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirLivros(int id)
        {
           
                var livroASerDeletado = _livroServico.BuscarPorID(id);
                if (livroASerDeletado == null)
                {
                    return NotFound();
                }
                _livroServico.Excluir(id);
                return Ok(id);
            
           
                //var detalheErroDeExclusao = $"Não foi possível excluir o livro de ID {id}";
                //return Problem(detalheErroDeExclusao, HttpContext.Request.Path, (int)HttpStatusCode.InternalServerError, ex.Message);
            
        }
    }
}
