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
        public void AdicionaFilme([FromBody] Filme filme)
        {
            filme.Id = Id++;
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
