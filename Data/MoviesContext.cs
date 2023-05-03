using bestmovies_academy.api.Entities;
using Microsoft.EntityFrameworkCore;

namespace bestmovies_academy.api.Data
{
    public class MoviesContext:DbContext
    {
         public DbSet< Movie> Movies{ get; set; }

        public MoviesContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}