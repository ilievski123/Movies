using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Genre name")]
        public string GenreName { get; set; }

        public ICollection<GenreMovie> Movies { get; set; }
    }
}
