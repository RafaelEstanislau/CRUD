
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
        public IActionResult CriarLivros([FromBody] Livro livroASerAdicionado)
        {
            try
            {
                Validacao.ValidacaoDeCampos(livroASerAdicionado);
                var id = _livroServico.Salvar(livroASerAdicionado);
                livroASerAdicionado.id = id;
            
                return Created($"livro/{livroASerAdicionado.id}", livroASerAdicionado);
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
        public OkObjectResult EditarLivro(Livro livroASerEditado)
        {
            try
            {
                Livro livroEditado = new();
                if (livroASerEditado == null)
                {
                    NotFound();
                }
                else
                {
                    Validacao.ValidacaoDeCampos(livroASerEditado);
                    livroEditado = _livroServico.Editar(livroASerEditado);
                
                }

                return Ok(livroEditado);
            }
            catch (Exception ex)
            {

                throw new Exception("Não foi possível editar", ex);
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
