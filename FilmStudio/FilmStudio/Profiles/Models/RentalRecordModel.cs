using System;
using System.ComponentModel.DataAnnotations;
using FilmStudio.Entities;

namespace FilmStudio.Profiles.Models
{
    public class RentalRecordModel
    {
        [Required] public int OrderId { get; set; }
        public DateTime RentalDate { get; set; }
        public Film Film { get; set; }

        public string FilmStudioName { get; set; }
        public string FilmStudioUsername { get; set; }
        public string FilmStudioChairmanName { get; set; }
    }
}
