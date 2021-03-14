using System;
using System.ComponentModel.DataAnnotations;
using FilmStudio.Authentication;

namespace FilmStudio.Entities
{
    public class RentalRecord
    {
        [Key] public int OrderId { get; set; }
        public DateTime RentalDate { get; set; }
        public ApplicationUser FilmStudio { get; set; }
        public Film Film { get; set; }
    }
}
