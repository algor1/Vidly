using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data.Entity;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dto;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    public class CustomersController : ApiController
    {
        private MyDBContext _context;
        public CustomersController()
        {
            _context = new MyDBContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }

        public IHttpActionResult GetCustomers()
        {
            IEnumerable < CustomerDto > customers= _context.Customers.
                                        Include(c=>c.MembershipType).ToList().
                                        Select(Mapper.Map<Customer,CustomerDto>);
            return Ok(customers);
        }

        public IHttpActionResult GetCustomer(int id)
        {
            Customer customer= _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
               NotFound();
            }

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
                
            Customer customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri+"/"+customer.Id), customerDto);
        }
        [HttpPut]
        public void PutCustomer(int id,CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                BadRequest();
            Customer customerInDb= _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
                NotFound();
            Mapper.Map<CustomerDto, Customer>(customerDto, customerInDb);
            
            _context.SaveChanges();
        }
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            Customer customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();

        }

    }
}
