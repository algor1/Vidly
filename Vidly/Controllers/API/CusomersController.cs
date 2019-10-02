using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    public class CusomersController : ApiController
    {
        private MyDBContext _context;
        public CusomersController()
        {
            _context = new MyDBContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }

        public Customer GetCustomer(int id)
        {
            Customer customer= _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                throw new HttpRequestException(HttpStatusCode.NotFound);
            }
            return customer;
        }
        [HttpPost]
        public Customer CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            return customer;
        }

    }
}
