using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movies.Models;
using Movies.ViewModels;

namespace Movies.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MoviesContext _context;

        public MoviesController(MoviesContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index(string title)
        {
             var moviesContext = _context.Movie.Include(m => m.Director).Include(m => m.Actors).ThenInclude(m => m.Actor).Include(m => m.Genres).ThenInclude(m => m.Genre).Include(m => m.Producer);
             return View(await moviesContext.ToListAsync()); 

   
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .Include(m => m.Director)
                .Include(m => m.Producer)
                .Include(m => m.Actors).ThenInclude(m => m.Actor)
                .Include(m => m.Genres).ThenInclude(m => m.Genre)

                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

         // GET: Movies/Create
         public IActionResult Create()
         {
             ViewData["DirectorId"] = new SelectList(_context.Director, "Id", "FullName");
             ViewData["ProducerId"] = new SelectList(_context.Producer, "Id", "FullName");
             ViewData["Actors"] = new SelectList(_context.ActorMovie, "ActorId", "FullName");
             ViewData["Genres"] = new SelectList(_context.GenreMovie, "GenreId", "GenreName");
             return View();
         }

         // POST: Movies/Create
         // To protect from overposting attacks, enable the specific properties you want to bind to.
         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price,Rating,DirectorId, ProducerId")] Movie movie)
         {
             if (ModelState.IsValid)
             {
                 _context.Add(movie);
                 await _context.SaveChangesAsync();
                 return RedirectToAction(nameof(Index));
             }
             ViewData["DirectorId"] = new SelectList(_context.Director, "Id", "FullName", movie.DirectorId);
             ViewData["ProducerId"] = new SelectList(_context.Producer, "Id", "FullName", movie.ProducerId);
             ViewData["Actors"] = new SelectList(_context.ActorMovie, "ActorId", "FullName", movie.Actors);
             ViewData["Genres"] = new SelectList(_context.GenreMovie, "GenreId", "GenreName", movie.Genres);
            return View(movie);
         }

         // GET: Movies/Edit/5
         public async Task<IActionResult> Edit(int? id)
         {
             if (id == null || _context.Movie == null)
             {
                 return NotFound();
             }

             var movie = _context.Movie.Where(m => m.Id == id).Include(m => m.Actors).First();
             if (movie == null)
             {
                 return NotFound();
             }

             var actors = _context.Actor.AsEnumerable();
             actors = actors.OrderBy(s => s.FullName);
             MovieActorViewModel viewmodel = new MovieActorViewModel
             {
                 Movie = movie,
                 ActorList = new MultiSelectList(actors, "Id", "FullName"),
                 SelectedActors = movie.Actors.Select(sa => sa.ActorId)
             };


             ViewData["DirectorId"] = new SelectList(_context.Director, "Id", "FullName", movie.DirectorId);
             ViewData["ProducedId"] = new SelectList(_context.Producer, "Id", "FullName", movie.ProducerId);
             ViewData["Actors"] = new SelectList(_context.Actor, "ActorId", "FullName", movie.Actors);
             ViewData["Genres"] = new SelectList(_context.Genre, "GenreId", "GenreName", movie.Genres);
            return View(viewmodel);
         }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MovieActorViewModel viewmodel)
        {
            if(id != viewmodel.Movie.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewmodel.Movie);
                    await _context.SaveChangesAsync();

                    var movie = _context.Movie.Where(m => m.Id == id).First();

                    IEnumerable<int> selectedActors = viewmodel.SelectedActors;
                    if (selectedActors != null)
                    {
                        IQueryable<Movie> toBeRemoved = _context.Movie.Where(s => !selectedActors.Contains(s.Id) && s.Id == id);
                        _context.Movie.RemoveRange(toBeRemoved);

                        IEnumerable<int> existMovies = _context.Movie.Where(s => selectedActors.Contains(s.Id) && s.Id == id).Select(s => s.Id);
                        IEnumerable<int> newMovies = selectedActors.Where(s => !existMovies.Contains(s));

                        foreach (int Id in newMovies)
                            _context.Movie.Add(new Movie { Id = Id });

                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        IQueryable<Movie> toBeRemoved = _context.Movie.Where(s => s.Id == id);
                        _context.Movie.RemoveRange(toBeRemoved);
                        await _context.SaveChangesAsync();
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(viewmodel.Movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(viewmodel);

        } 

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .Include(m => m.Director)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Movie == null)
            {
                return Problem("Entity set 'MoviesContext.Movie'  is null.");
            }
            var movie = await _context.Movie.FindAsync(id);
            if (movie != null)
            {
                _context.Movie.Remove(movie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
          return (_context.Movie?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
