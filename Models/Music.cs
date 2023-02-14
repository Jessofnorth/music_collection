using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace music_collection.Models
{
    //artist model with properties
    public class Artist
    {
        [Key] // primary key 
        public int ArtistId { get; set; }

        [Required]
        [Display(Name = "Artist")]
        public string? ArtistName { get; set; }
    }

    //album model with properties
    public class Album
    {
        [Key] // primary key 
        public int AlbumId { get; set; }

        [Required]
        [Display(Name = "Album")]
        public string? AlbumName { get; set; }

        [ForeignKey("Artist")] //foreign-key from Artist table
        public int ArtistId { get; set; }
        // instance of artist class
        public Artist? Artist { get; set; }
    }
}