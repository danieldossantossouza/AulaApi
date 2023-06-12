using CursoFilmesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CursoFilmesApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private static List<Filme> filmes = new List<Filme>();

        [HttpPost]
        public void AdicionaFilme([FromBody] Filme filme)
        {
            filmes.Add(filme);
        }

        [HttpGet]
        public IEnumerable<Filme> ListaDeFilmes()
        {
            return filmes;
        }

        [HttpGet("{id}")]
        public Filme? RecuperafilmePorId(int id)
        {
            return filmes.FirstOrDefault(f=>f.Id == id);
        }

    }
}
