
using CRUD_Livros.Infra.AcessoDeDados;
using CRUD_Livros.Dominio.RegraDeNegocio;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
            try
            {
                Validacao.ValidacaoDeCampos(livroASerAdicionado);
                _livroServico.Salvar(livroASerAdicionado);

                return CreatedAtAction(
                actionName: nameof(BuscarTodos),
                livroASerAdicionado
                );
            }
            catch (Exception ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
           

            
        }

        [HttpGet]
        public IActionResult BuscarTodos()
        {
            var livrosBuscados = _livroServico.BuscarTodos();
            if(livrosBuscados == null)
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
            try
            {
                if (livroASerEditado == null)
                {
                    NotFound();
                }
                else
                {
                    Validacao.ValidacaoDeCampos(livroASerEditado);
                    _livroServico.Editar(livroASerEditado);
                }

                return Ok();
            }
            catch (Exception ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
          
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
