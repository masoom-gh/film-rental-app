namespace FilmStudio.Entities
{
    public class Film
    {
        public int FilmId { get; set; }
        public string FilmName { get; set; }
        public int ReleaseYear { get; set; }
        public string Country { get; set; }
        public string Director { get; set; }
        public int TotalNumberOfCopies { get; set; }
        public int NumberOfRentedCopies { get; set; }
        public string ImageUrl { get; set; }
    }
}
