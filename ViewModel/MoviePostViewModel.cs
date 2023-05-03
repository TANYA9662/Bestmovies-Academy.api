using System.ComponentModel.DataAnnotations;

namespace bestmovies_academy.api.ViewModel
{
    public class MoviePostViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Movie is missing")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Category is missing")]
         public string Category { get; set; }
        [Required(ErrorMessage = "ReleseDate is missing")]
         public string Release { get; set; }
     
         public string Description { get; set; }
    }
}