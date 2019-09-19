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

        public ActionResult Random()
        {
            var viewModel = new RamdomMovieViewModel();
            viewModel.Movie = new Movie() { Name = "Shrek!" };
            viewModel.Customers= new List<Customer>() {
                new Customer { Id=1, Name="Nik" },
                new Customer{ Id=2,Name="Bob"}
            };
            return View(viewModel);
    
        }
        public ActionResult Edit(int id)
        {
            return Content("id=  "+id);
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

        [Route("movies/released/{year:regex(2015|2016)}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year,int month)
        {
            return Content($"year/Month {year}/{month}");
        }

    }
}