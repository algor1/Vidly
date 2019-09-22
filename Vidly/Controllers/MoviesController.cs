using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        MyDBContext _context;

        public MoviesController()
        {
            _context = new MyDBContext();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _context.Dispose();
        }

        
        public ActionResult Edit(int id)
        {
            MovieFormViewModel viewModel = new MovieFormViewModel();
            viewModel.Movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (viewModel == null)
                return HttpNotFound();

            viewModel.Genres = _context.Genres.ToList();
            return View("MovieForm", viewModel);
        }

        public ActionResult New()
        {
            MovieFormViewModel viewModel = new MovieFormViewModel();
            
            viewModel.Genres = _context.Genres.ToList();

            return View("MovieForm", viewModel);
        }

        public ActionResult Index()
        {
            var movies = _context.Movies.Include(c => c.Genre);
            return View(movies);
        }

        public ActionResult Detail(int id)
        {
            var movie=_context.Movies.Include(m => m.Genre).Single(m => m.Id==id);
            return View(movie);
        }
        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            
        }
        

    }
}