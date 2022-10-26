
using CRUD_Livros.Infra.AcessoDeDados;
using CRUD_Livros.Dominio.RegraDeNegocio;
using Microsoft.AspNetCore.Mvc;

namespace LivrosAPI.Controllers
{
    [ApiController]
    [Route("livros")]
    public class LivroController : ControllerBase
    {
        private readonly IRepositorio _livroServico;

        public LivroController(IRepositorio livroServico)
        {
            _livroServico = livroServico;
        }
        [HttpPost]
        public IActionResult CriarLivros(Livro livroASerAdicionado)
        {
            _livroServico.Salvar(livroASerAdicionado);

            return CreatedAtAction(
                actionName: nameof(BuscarTodos),
                livroASerAdicionado
                );
        }

        [HttpGet]
        public IActionResult BuscarTodos()
        {
            var livrosBuscados = _livroServico.BuscarTodos();
            if(livrosBuscados.Count == 0)
            {
                return NotFound();
            }

            return Ok(livrosBuscados);
        }
        [HttpGet("{id}")]
        public IActionResult ObterClientePorId(int id)
        {
            var livro = _livroServico.BuscarPorID(id);
            if (livro == null)
            {
                return NotFound();
            }
            return Ok(livro);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarLivros(Livro livroASerEditado)
        {
            if(livroASerEditado == null)
            {
                NotFound();
            }
            else
            {
              _livroServico.Editar(livroASerEditado);
            }
           
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirLivros(int id)
        {
            var livroASerDeletado = _livroServico.BuscarPorID(id);
            if(livroASerDeletado == null)
            {
                return NotFound();
            }
            _livroServico.Excluir(id);
            return Ok(id);
        }
    }
}
