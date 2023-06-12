﻿using System.ComponentModel.DataAnnotations;

namespace CursoFilmesApi.Models
{
    public class Filme
    {

        public int Id { get; set; }

        [Required(ErrorMessage= "O título do filme é obrigatorio !")]
        public string? Titulo { get; set; }

        [Required(ErrorMessage = "O gênero do filme é obrigatorio !")]
        [MaxLength(50, ErrorMessage = "O tamanho do gênero não pode exceder a 50 caracteres. ")]
        public string? Genero { get; set; }

        [Required]
        [Range(70, 600, ErrorMessage = "A duração deve ter entre 70 e 600 minutos.")]
        public int Duracao { get; set; }
        
    }
}
