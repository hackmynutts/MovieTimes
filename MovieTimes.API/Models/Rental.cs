using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTimes.API.Models
{
    public class Rental
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int UserId { get; set; } 
        public int MovieId { get; set; }
        public string MovieTitle { get; set; } 
        public string PosterUrl { get; set; } 
        public DateTime RentalDate { get; set; }

        [ForeignKey("UserId")]public User? User { get; set; }
    }
}