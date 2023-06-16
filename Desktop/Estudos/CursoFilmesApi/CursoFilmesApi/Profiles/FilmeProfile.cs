using AutoMapper;
using CursoFilmesApi.Data.Dtos;
using CursoFilmesApi.Models;

namespace CursoFilmesApi.Profiles;
public class FilmeProfile : Profile
{
    public FilmeProfile()
    {
        CreateMap<CreateFilmeDto, Filme>();   
    }
}
