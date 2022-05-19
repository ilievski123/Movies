using Microsoft.AspNetCore.Mvc.Rendering;
using Movies.Models;
using System.Collections.Generic;


namespace Movies.ViewModels
{
    public class MovieGenreViewModel
    {
        public Movie Movie { get; set; }
        public IEnumerable<int> SelectedGenres { get; set; }
        public IEnumerable<SelectListItem> GenreList { get; set; }
    }
}
