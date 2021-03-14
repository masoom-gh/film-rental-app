using System.ComponentModel.DataAnnotations;

namespace FilmStudio.Profiles.Models
{
    public class FilmUpdateModel
    {
        [Required] public string FilmName { get; set; }
        [Required] public int ReleaseYear { get; set; }
        [Required] public string Country { get; set; }
        [Required] public string Director { get; set; }
        [Required] public int TotalNumberOfCopies { get; set; }
        [Required] public string ImageUrl { get; set; }
    }
}
