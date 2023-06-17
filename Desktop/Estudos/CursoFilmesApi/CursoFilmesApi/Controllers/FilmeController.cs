using AutoMapper;
using CursoFilmesApi.Data;
using CursoFilmesApi.Data.Dtos;
using CursoFilmesApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CursoFilmesApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper; 

        public FilmeController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;   
        }

        // Conseguimos colocar as informações de como o método vai se comportar 
        ///<summary>
        ///    Adiciona Um Filme ao banco de dados
        ///</summary>
        ///<param name="filmeDto">Objeto com os campos necessário para criação de um filme</param>
        ///<returns>IActionResult</returns>
        ///<response code="201">Caso inserção seja feita com sucesso</response>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
        {
            Filme filme =_mapper.Map<Filme>(filmeDto);  
            _context.Filmes.Add(filme);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperafilmePorId),new { id = filme.Id}, filme); // Assim que adicionado retorna o objeto criado 
        }

        [HttpGet]
        public IEnumerable<ReadFilmeDto> ListaDeFilmes([FromQuery]int skip =0, [FromQuery] int take=50)
        {
            //Com o Skip(pula a quantidade de elemento) eo Take(recupera a quantidade de elemento) pegamos a quantidade de pagina que queremos
            return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes.Skip(skip).Take(take));
    
        }

        [HttpGet("{id}")]
        public IActionResult RecuperafilmePorId(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(f => f.Id == id);
            if (filme == null) 
            {
                return NotFound();
            }
            var filmeDto = _mapper.Map<ReadFilmeDto>(filme);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            var filme = _context.Filmes.FirstOrDefault(f => f.Id == id);
            if (filme == null) return NotFound();
            _mapper.Map(filmeDto, filme);
            _context.SaveChanges();

            return NoContent();
        }

        // Atualização parcial com o HttpPatch
        [HttpPatch("{id}")]
        public IActionResult AtualizaFilmeParcial(int id,
            JsonPatchDocument<UpdateFilmeDto> patch)
        {
            var filme = _context.Filmes.FirstOrDefault(f => f.Id == id);
            if (filme == null) return NotFound();
            var filmeParaAtualizar = _mapper.Map<UpdateFilmeDto>(filme);
            patch.ApplyTo(filmeParaAtualizar, ModelState);

            if (!TryValidateModel(filmeParaAtualizar)) 
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(filmeParaAtualizar, filme);
            _context.SaveChanges();
            return NoContent();

            // Para poder fazer a mudança de um determinado campo exemplo abaixo em Json
            // { 
            //   "op":"replace",
            //   "path": "/titulo",
            //   "value": "aqui vc coloca a mudança"
            // }
        }


        [HttpDelete("{id}")]
        public IActionResult DeletaFilme(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(f => f.Id == id);
            if (filme == null) return NotFound();
           _context.Remove(filme);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
