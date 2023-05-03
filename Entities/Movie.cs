namespace bestmovies_academy.api.Entities
{
    public class Movie
    {
        public int Id { get; set; }

          public string Name { get; set; }
         public string Category { get; set; }
      
         public string Release { get; set; }
  
         public string ImageUrl {get; set;}
     
         public string Description { get; set; }
        
    }
}