using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies
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
        public ActionResult Index(int pageindex = 1, string sortBy = "Name")
        {
            var viewModel = GetMovies();
            return View(viewModel);
        }

        private IEnumerable<Movie> GetMovies()
        {
            return new List<Movie>()
            {
                new Movie { Id=1, Name="Shrek!" },
                new Movie { Id=2, Name="Wall-e" }
            };
        }

        [Route("movies/released/{year:regex(2015|2016)}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year,int month)
        {
            return Content($"year/Month {year}/{month}");
        }

    }
}