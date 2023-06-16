﻿using AutoMapper;
using CursoFilmesApi.Data;
using CursoFilmesApi.Data.Dtos;
using CursoFilmesApi.Models;
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

        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
        {
            Filme filme =_mapper.Map<Filme>(filmeDto);  
            _context.Filmes.Add(filme);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperafilmePorId),new { id = filme.Id}, filme); // Assim que adicionado retorna o objeto criado 
        }

        [HttpGet]
        public IEnumerable<Filme> ListaDeFilmes([FromQuery]int skip =0, [FromQuery] int take=50)
        {
            //Com o Skip(pula a quantidade de elemento) eo Take(recupera a quantidade de elemento) pegamos a quantidade de pagina que queremos
            return _context.Filmes.Skip(skip).Take(take);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperafilmePorId(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(f => f.Id == id);
            if (filme == null) 
            {
                return NotFound();
            }
            return Ok();
        }

    }
}
