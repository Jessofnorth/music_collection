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
        [Display(Name = "Artist")]
        public int ArtistId { get; set; }
        // instance of artist class
        public Artist? Artist { get; set; }
    }

    //friend model with properties
    public class Friend
    {
        [Key] // primary key 
        public int FriendId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string? FriendName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [Phone]
        public string? Phonenumber { get; set; }
    }

    //loan model with properties
    public class Loan
    {
        [Key] // primary key 
        public int LoanId { get; set; }

        [ForeignKey("Album")] //foreign-key from Album table
        [Display(Name = "Album")]
        public int AlbumId { get; set; }
        // instance of album class
        public Album? Album { get; set; }

        [ForeignKey("Friend")] //foreign-key from Friend table
        [Display(Name = "Friend")]
        public int FriendId { get; set; }
        // instance of friend class
        public Friend? Friend { get; set; }

        [Required]
        [Display(Name = "Date loaned")]
        public DateTime LoanDate { get; set; }
    }
}