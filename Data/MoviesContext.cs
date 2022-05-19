using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movies.Models;

namespace Movies.Models
{
    public class MoviesContext : DbContext
    {
        public MoviesContext (DbContextOptions<MoviesContext> options)
            : base(options)
        {
        }

        public DbSet<Movies.Models.Movie>? Movie { get; set; }

        public DbSet<Movies.Models.Actor>? Actor { get; set; }

        public DbSet<Movies.Models.Director>? Director { get; set; }

        public DbSet<Movies.Models.Genre>? Genre { get; set; }

        public DbSet<Movies.Models.Producer>? Producer { get; set; }

        public DbSet<ActorMovie> ActorMovie { get; set; }
        public DbSet<GenreMovie> GenreMovie { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ActorMovie>()
            .HasOne<Actor>(p => p.Actor)
            .WithMany(p => p.Movies)
            .HasForeignKey(p => p.ActorId);
            
            builder.Entity<ActorMovie>()
            .HasOne<Movie>(p => p.Movie)
            .WithMany(p => p.Actors)
            .HasForeignKey(p => p.MovieId);

            builder.Entity<GenreMovie>()
            .HasOne<Genre>(p => p.Genre)
            .WithMany(p => p.Movies)
            .HasForeignKey(p => p.GenreId);

            builder.Entity<GenreMovie>()
            .HasOne<Movie>(p => p.Movie)
            .WithMany(p => p.Genres)
            .HasForeignKey(p => p.MovieId);

            builder.Entity<Movie>()
            .HasOne<Director>(p => p.Director)
            .WithMany(p => p.Movies)
            .HasForeignKey(p => p.DirectorId);

            builder.Entity<Movie>()
            .HasOne<Producer>(p => p.Producer)
            .WithMany(p => p.Movies)
            .HasForeignKey(p => p.ProducerId);

        }
    }
}
