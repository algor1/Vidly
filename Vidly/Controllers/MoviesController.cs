﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity.Validation;

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
            Movie movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();

            MovieFormViewModel viewModel = new MovieFormViewModel(movie);

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
            return View();
        }

        public ActionResult Detail(int id)
        {
            var movie=_context.Movies.Include(m => m.Genre).Single(m => m.Id==id);
            return View(movie);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (movie.Id == 0)
            {
                movie.AddedDate = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDB = _context.Movies.Single(m => m.Id == movie.Id);
                movieInDB.Name = movie.Name;
                movieInDB.ReleaseDate = movie.ReleaseDate;
                movieInDB.GenreId = movie.GenreId;
                movieInDB.NumberInStock = movie.NumberInStock;
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        

    }
}