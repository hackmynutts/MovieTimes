namespace MovieTimes.UI.Models
{
    public class MovieDTO
    {
        public int id { get; set; }
        public int tmdb_id { get; set; } = 0;
        public string title { get; set; } = string.Empty;
        public string original_title { get; set; } = string.Empty;
        public string overview { get; set; } = string.Empty;
        public DateOnly release_date { get; set; }
        public string poster_path { get; set; } = string.Empty;
        public string PosterUrl { get; set; } = string.Empty;
        public int RentedByUserId { get; set; } = 0;
    }
}
