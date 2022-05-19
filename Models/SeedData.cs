using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Movies.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MoviesContext(
            serviceProvider.GetRequiredService<
            DbContextOptions<MoviesContext>>()))
            {
                // Look for any movies.
                if (context.Movie.Any() || context.Director.Any() || context.Actor.Any())
                {
                    return; // DB has been seeded
                }

                context.Director.AddRange(
                new Director { /*Id = 1, */FirstName = "Gore", LastName = "Verbinski", BirthDate = DateTime.Parse("1964-3-16") },
                new Director { /*Id = 2, */FirstName = "Tim", LastName = "Burton", BirthDate = DateTime.Parse("1946-11-27") },
                new Director { /*Id = 3, */FirstName = "Greg", LastName = "Mottola", BirthDate = DateTime.Parse("1964-7-11") }
                );
                context.SaveChanges();

                context.Producer.AddRange(
                new Producer { /*Id = 1, */FirstName = "Jerry", LastName = "Bruckheimer", BirthDate = DateTime.Parse("1943-9-21") },
                new Producer { /*Id = 2, */FirstName = "Judd", LastName = "Apatow", BirthDate = DateTime.Parse("1967-12-6") },
                new Producer { /*Id = 3, */FirstName = "Michael", LastName = "Finn", BirthDate = DateTime.Parse("1943-5-30") }
                );
                context.SaveChanges();

                context.Actor.AddRange(
                new Actor { /*Id = 1, */FirstName = "Johnny", LastName = "Depp", BirthDate = DateTime.Parse("1948-3-14") },
                new Actor { /*Id = 2, */FirstName = "Johan", LastName = "Hill", BirthDate = DateTime.Parse("1983-12-20") },
                new Actor { /*Id = 3, */FirstName = "Orlando", LastName = "Bloom", BirthDate = DateTime.Parse("1977-1-13") },
                new Actor { /*Id = 4, */FirstName = "Bill", LastName = "Murray", BirthDate = DateTime.Parse("1950-9-21") },
                new Actor { /*Id = 5, */FirstName = "Keira", LastName = "Knightley", BirthDate = DateTime.Parse("1985-3-26") },
                new Actor { /*Id = 6, */FirstName = "Sigourney", LastName = "Weaver", BirthDate = DateTime.Parse("1949-11-8") },
                new Actor { /*Id = 7, */FirstName = "Seth", LastName = "Rogen", BirthDate = DateTime.Parse("1982-5-26") }
                );
                context.SaveChanges();

                context.Genre.AddRange(
                new Genre { /*Id = 1, */GenreName = "Comedy" },
                new Genre { /*Id = 2, */GenreName = "Action" },
                new Genre { /*Id = 3, */GenreName = "Horror" },
                new Genre { /*Id = 4, */GenreName = "Drama" }
                );
                context.SaveChanges();

                context.Movie.AddRange(
                new Movie
                {
                    //Id = 1,
                    Title = "Pirates of the Caribbean",
                    ReleaseDate = DateTime.Parse("2003-2-12"),
                    Rating = 10,
                    Price = 7.99M,
                    DirectorId = context.Director.Single(d => d.FirstName == "Gore" && d.LastName == "Verbinski").Id,
                    ProducerId = context.Producer.Single(d => d.FirstName == "Jerry" && d.LastName == "Bruckheimer").Id
                },

                new Movie
                {
                    //Id = 2,
                    Title = "Sleepy Hollow",
                    ReleaseDate = DateTime.Parse("1999-11-17"),
                    Rating = 8,
                    Price = 5.99M,
                    DirectorId = context.Director.Single(d => d.FirstName == "Tim" && d.LastName == "Burton").Id,
                    ProducerId = context.Producer.Single(d => d.FirstName == "Judd" && d.LastName == "Apatow").Id
                },
                new Movie
                {
                    //Id = 3,
                    Title = "Ed Wood",
                    ReleaseDate = DateTime.Parse("1994-9-28"),
                    Rating = (decimal?)7.8,
                    Price = 3.99M,
                    DirectorId = context.Director.Single(d => d.FirstName == "Tim" && d.LastName == "Burton").Id,
                    ProducerId = context.Producer.Single(d => d.FirstName == "Michael" && d.LastName == "Finn").Id
                },
                new Movie
                {
                    //Id = 4,
                    Title = "Superbad",
                    ReleaseDate = DateTime.Parse("1999-11-17"),
                    Rating = 9,
                    Price = 5.99M,
                    DirectorId = context.Director.Single(d => d.FirstName == "Greg" && d.LastName == "Mottola").Id,
                    ProducerId = context.Producer.Single(d => d.FirstName == "Judd" && d.LastName == "Apatow").Id
                }
                );
                context.SaveChanges();

                context.ActorMovie.AddRange
                (
                new ActorMovie { ActorId = 1, MovieId = 1 },
                new ActorMovie { ActorId = 1, MovieId = 2 },
                new ActorMovie { ActorId = 1, MovieId = 3 },
                new ActorMovie { ActorId = 2, MovieId = 4 },
                new ActorMovie { ActorId = 7, MovieId = 4 },
                new ActorMovie { ActorId = 4, MovieId = 3 },
                new ActorMovie { ActorId = 6, MovieId = 2 },
                new ActorMovie { ActorId = 5, MovieId = 1 },
                new ActorMovie { ActorId = 3, MovieId = 1 }
                );
                context.SaveChanges();

                context.GenreMovie.AddRange
                (
                new GenreMovie { GenreId = 1, MovieId = 1 },
                new GenreMovie { GenreId = 1, MovieId = 4 },
                new GenreMovie { GenreId = 2, MovieId = 1 },
                new GenreMovie { GenreId = 2, MovieId = 3 },
                new GenreMovie { GenreId = 3, MovieId = 4 },
                new GenreMovie { GenreId = 4, MovieId = 3 },
                new GenreMovie { GenreId = 2, MovieId = 2 },
                new GenreMovie { GenreId = 3, MovieId = 2 }
                );
                context.SaveChanges();
            }
        }
    }
}

