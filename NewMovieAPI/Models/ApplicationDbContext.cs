using Microsoft.EntityFrameworkCore;

namespace NewMovieAPI.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }

        //public DbSet<MovieGenre> MovieGenres { get; set; }

        ////public DbSet<Theater> Theaters { get; set; }
        ////public DbSet<MovieInTheater> MoviesInTheaters { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // Define composite primary key for MovieGenre
        //    modelBuilder.Entity<MovieGenre>()
        //        .HasKey(mg => new { mg.MovieId, mg.GenreId });

        //}
    }
}
