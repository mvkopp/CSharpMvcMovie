using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcMovie.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MvcMovie.Controllers
{
    public class MoviesController : Controller
    {
        private static List<Movie> Movies = new List<Movie>
        {
            new Movie
            {
                Id = 1,
                Title = "Le retour du jedi",
                Genre = "SF",
                Price = 1.99M,
                ReleaseDate = new DateTime(1983, 10, 19),
                Rating = "R"
            },
            new Movie
            {
                Id = 2,
                Title = "L'empire contre attaque",
                Genre = "SF",
                Price = 2.99M,
                ReleaseDate = new DateTime(1980, 08, 20),
                Rating = "R"
            },
            new Movie
            {
                Id = 3,
                Title = "Un nouvel espoir",
                Genre = "SF",
                Price = 3.99M,
                ReleaseDate = new DateTime(1977, 10, 19),
                Rating = "R"
            }
        };

        // GET: Movies
        public ActionResult Index()
        {
            return View(Movies);
        }

        // GET: Movies/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = Movies.Single(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Title,ReleaseDate,Genre,Price")] Movie movie)
        {
            try
            {
                // TODO: Add insert logic here

                var lastMovie = Movies.Last();
                var index = lastMovie.Id + 1;

                // Solution du prof
                //var index = Movies.Max(m => m.Id) +1;

                // Set the index
                movie.Id = index;

                Movies.Add(movie);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var movie = Movies.Single(m=>m.Id == id.Value);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST : Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var movieToUpdate = Movies.Single(m => m.Id == id);

                    movieToUpdate.Title = movie.Title;
                    movieToUpdate.Genre = movie.Genre;
                    movieToUpdate.ReleaseDate = movie.ReleaseDate;
                    movieToUpdate.Price = movie.Price;
                }
                catch
                {
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = Movies.FirstOrDefault(m => m.Id == id.Value);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                var movie = Movies.Single(m => m.Id == id);
                Movies.Remove(movie);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}