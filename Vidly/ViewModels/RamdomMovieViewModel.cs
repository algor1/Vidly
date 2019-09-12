using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class RamdomMovieViewModel
    {
        public Movie Movie { get; set; }
        public List<Customer> Customers { get; set; }
    }
}