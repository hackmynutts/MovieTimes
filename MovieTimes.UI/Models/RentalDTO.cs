namespace MovieTimes.UI.Models
{
    public class RentalDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } 
        public int MovieId { get; set; }
        public string MovieTitle { get; set; } 
        public string PosterUrl { get; set; } 
        public DateTime RentalDate { get; set; }
    }
}