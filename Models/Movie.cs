using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movies.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Title { get; set; }
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }
        [Range(1, 100)]
        [DataType(DataType.Currency)]
        public decimal? Price { get; set; }
        [Range(1, 10)]
        public decimal? Rating { get; set; }
        [Display(Name = "Director")]
        public int? DirectorId { get; set; }
        public Director Director { get; set; }

        [Display(Name = "Producer")]
        public int? ProducerId { get; set; }
        public Producer Producer { get; set; }

        public ICollection <ActorMovie> Actors { get; set; }
        public ICollection<GenreMovie> Genres { get; set; }
    }

}

