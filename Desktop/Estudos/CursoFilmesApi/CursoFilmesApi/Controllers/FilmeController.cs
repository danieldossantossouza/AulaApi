using CursoFilmesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CursoFilmesApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private static List<Filme> filmes = new List<Filme>();
        private static int Id = 0;

        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] Filme filme)
        {
            filme.Id = Id++;
            filmes.Add(filme);
            return CreatedAtAction(nameof(RecuperafilmePorId),new { id = filme.Id}, filme); // Assim que adicionado retorna o objeto criado 
        }

        [HttpGet]
        public IEnumerable<Filme> ListaDeFilmes([FromQuery]int skip =0, [FromQuery] int take=50)
        {
            //Com o Skip(pula a quantidade de elemento) eo Take(recupera a quantidade de elemento) pegamos a quantidade de pagina que queremos
            return filmes.Skip(skip).Take(take);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperafilmePorId(int id)
        {
            var filme = filmes.FirstOrDefault(f => f.Id == id);
            if (filme == null) 
            {
                return NotFound();
            }
            return Ok();
        }

    }
}
