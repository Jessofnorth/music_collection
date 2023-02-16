using music_collection.Models;
using Microsoft.EntityFrameworkCore;

namespace music_collection.Data
{
    public class MusicContext : DbContext
    {
        public MusicContext(DbContextOptions<MusicContext> options) : base(options)
        {
            //constructor
        }

        public DbSet<Artist>? Artists { get; set; } 
        public DbSet<Album>? Albums { get; set; }
        public DbSet<Friend>? Friend { get; set; } 
        public DbSet<Loan>? Loan { get; set; }
    }
}