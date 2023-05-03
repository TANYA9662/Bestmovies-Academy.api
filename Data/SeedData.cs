

using System.Text.Json;
using bestmovies_academy.api.Entities;

namespace bestmovies_academy.api.Data
{
    public class SeedData
    {
        public static async Task LoadData(MoviesContext context)
        {
            //Steg 1
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive = true
            };
            
            //Steg 2. Ladda data om movies tabell är tom
            if(context.Movies.Any()) return;
            
            //Steg 3. Läsa in json fil
            var json = System.IO.File.ReadAllText("Data/json/movies.json");
            
            //Steg 4. Omvandla json objekt till lista moviesmodel objekt
            var movies = JsonSerializer.Deserialize<List<Movie>>(json, options);

            // Steg 5. Skicka lista med MovieModel till database
            if(movies is not null && movies.Count > 0){
                await context.Movies.AddRangeAsync(movies);
                await context.SaveChangesAsync();
            }
        }
    }
}
    
