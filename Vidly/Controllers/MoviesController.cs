using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies
        public ActionResult Random()
        {
            Movie movie = new Movie() { Name = "Shrek!" };
            return View(movie);
            //return Content("Gghbdtn");
            //return HttpNotFound();
            //return RedirectToAction("Index", "Home", new { page = 1, Id = 111 });
        }
        public ActionResult Edit(int id)
        {
            return Content("id=  "+id);
        }
        public ActionResult Index(int pageindex = 1, string sortBy = "Name")
        {
            return Content($"page= {pageindex}  sorting by {sortBy}");
        }
        [Route("movies/released/{year:regex(2015|2016)}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year,int month)
        {
            return Content($"year/Month {year}/{month}");
        }
    }
}