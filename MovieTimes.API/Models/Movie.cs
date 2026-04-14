namespace MovieTimes.API.Models
{
    public class Movie
    {
        public int id { get; set; }
        public int tmdb_id { get; set; } = 0;
        public string title { get; set; } = string.Empty;
        public string original_title { get; set; } = string.Empty;
        public string overview { get; set; } = string.Empty;
        public DateOnly release_date { get; set; } 
        public string poster_path { get; set; } = string.Empty;

        public string PosterUrl =>
            $"https://image.tmdb.org/t/p/w500{poster_path}";
    }
}
