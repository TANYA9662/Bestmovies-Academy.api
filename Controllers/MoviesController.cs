using bestmovies_academy.api.Data;
using bestmovies_academy.api.Entities;
using bestmovies_academy.api.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bestmovies_academy.api.Controllers
{
    [ApiController]
    [Route("api/v1/movies")]
    public class MoviesController : ControllerBase
    {
       private readonly MoviesContext _context;
        private readonly string _imageBaseUrl;
      
        public MoviesController(MoviesContext context, IConfiguration config)
        {
             
            _context = context; 
            _imageBaseUrl = config.GetSection("apiImageUrl").Value;                  
        }
        [HttpGet()]
        public async Task<IActionResult> ListAll()

        {
          var result = await _context.Movies
          .Select(m => new 
          {
            Id = m.Id,
            Name = m.Name,
            Category = m.Category,
            Release =m.Release,
            ImageUrl = _imageBaseUrl +  m.ImageUrl ?? "no-movie.png",
            Description = m.Description,
       })    
         .ToListAsync();

         return Ok(result);
      }
      [HttpGet("{id}")] 
      public async Task<IActionResult> GetById(int id)
      {
         var result = await _context.Movies
         .Select(m => new
        {
          Id = m.Id,
          Name = m.Name,
          Category = m.Category,
          Release =m.Release,
          ImageUrl = _imageBaseUrl +  m.ImageUrl ?? "no-movie.png",
          Description = m.Description,
        })     
       .SingleOrDefaultAsync(c =>c.Id == id);

         return Ok(result);
      } 
      [HttpPost()]
        public async Task<IActionResult> Create(MoviePostViewModel  movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var movieToAdd = new Movie
        {
         
          Name = movie.Name,
          Category = movie.Category,
          Release = movie.Release,
          ImageUrl =  "no-movie.png",
          Description = movie.Description,
        };
        try
            {
                await _context.Movies.AddAsync(movieToAdd);

                if (await _context.SaveChangesAsync() > 0)
                {
                    // return StatusCode(201);
                    return CreatedAtAction(nameof(GetById), new { id = movieToAdd.Id },
                    new
                    {
                        Id = movieToAdd.Id,
                        Category = movieToAdd.Category,
                        Release = movieToAdd.Release,
                        Description = movieToAdd.Description
                    });
                }

                return StatusCode(500, "Internal Server Error");
            }
            catch (Exception ex)
            {
                // loggning till en databas som hanterar debug information...
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }

        }

      
      [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,MovieUpdateViewModel movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movieToUpdate = await _context.Movies.FindAsync(id);

            if (movieToUpdate == null)
            {
                return NotFound();
            }
           
            movieToUpdate.Name = movie.Name;
            movieToUpdate.Category = movie.Category;
            movieToUpdate.Release = movie.Release;
            movieToUpdate.ImageUrl = movie.ImageUrl;
            movieToUpdate.Description = movie.Description;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        
         [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var movieToDelete = await _context.Movies.FindAsync(id);

            if (movieToDelete == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movieToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}


    
