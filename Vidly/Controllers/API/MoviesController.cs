using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dto;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    public class MoviesController : ApiController
    {
        MyDBContext _context;
        public MoviesController()
        {
            _context = new MyDBContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }

        public IHttpActionResult  GetMovies()
        {
            IEnumerable < MovieDto > movies= _context.Movies.Include(m=> m.Genre).ToList().Select(Mapper.Map<Movie, MovieDto>);
            return Ok(movies);
        }

        public IHttpActionResult GetMovie(int id)
        {
            Movie movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<MovieDto>(movie));
        }
        [HttpPost]
        public IHttpActionResult PostMovie(MovieDto movieDto)
        {
            Movie movie= _context.Movies.Add(Mapper.Map<Movie>(movieDto));
            _context.SaveChanges();
            movieDto.Id = movie.Id;
            return Created(new Uri(Request.RequestUri+"/"+movieDto.Id), movieDto);
        }

        public IHttpActionResult PutMovie(int id,MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                BadRequest();

            Movie movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                return NotFound();

            Mapper.Map<MovieDto, Movie>(movieDto, movieInDb);
            _context.SaveChanges();
            
            
            return Ok();
        }
        public IHttpActionResult DeleteMovie(int id)
        {
            Movie movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                return NotFound();
            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();
            return Ok();
        }
    }

}
