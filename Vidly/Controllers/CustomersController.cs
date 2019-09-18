using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        MyDBContext _context;

        public CustomersController()
        {
            _context = new MyDBContext();
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _context.Dispose(); 
        }

        public ActionResult Index()
        {
 
            var customers = _context.Customers.Include(c =>c.MembershipType ).ToList();
            return View(customers);
        }

        public ActionResult Detail(int id)
        {
            
            Customer customer = _context.Customers.Single(c => c.Id == id);

            return View(customer);

        }

    }
}