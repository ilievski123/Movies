using Microsoft.AspNetCore.Mvc.Rendering;
using Movies.Models;
using System.Collections.Generic;


namespace Movies.ViewModels
{
    public class MovieActorViewModel
    {
        public Movie Movie { get; set; }
        public IList<Movie> Movies { get; set; }
        public IEnumerable<int>? SelectedActors { get; set; }
        public IEnumerable<SelectListItem>? ActorList { get; set; }
        public string SearchString { get; set; }
    }
}
