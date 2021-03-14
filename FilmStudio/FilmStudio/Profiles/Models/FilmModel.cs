using System.ComponentModel.DataAnnotations;

namespace FilmStudio.Profiles.Models
{
    public class FilmModel
    {
        public int FilmId { get; set; }
        [Required] public string FilmName { get; set; }
        [Required] public int ReleaseYear { get; set; }
        [Required] public string Country { get; set; }
        [Required] public string Director { get; set; }
        [Required] public int TotalNumberOfCopies { get; set; }
        public int NumberOfRentedCopies { get; set; }
        [Required] public string ImageUrl { get; set; }
    }
}
