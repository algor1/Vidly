using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
 
            var customers = GetCustomers();
            return View(customers);
        }

        public ActionResult Detail(int id)
        {
            var customers = GetCustomers();
            Customer customer = customers.Single(c => c.Id == id);

            return View(customer);

        }

        private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>() {
                new Customer { Id=1, Name="Nik" },
                new Customer{ Id=2,Name="Bob"}
            };
        }
    }
}