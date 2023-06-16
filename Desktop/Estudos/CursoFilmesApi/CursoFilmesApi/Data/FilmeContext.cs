using CursoFilmesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CursoFilmesApi.Data
{
    public class FilmeContext : DbContext 
    {
        public FilmeContext(DbContextOptions<FilmeContext> opts) : base(opts)
        {

        }
        public DbSet<Filme> Filmes { get; set; }
    }
}
