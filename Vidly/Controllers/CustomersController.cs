using AutoMapper;
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
 
            return View();
        }

        public ActionResult Detail(int id)
        {
            
            Customer customer = _context.Customers.Include(c =>c.MembershipType).Single(c => c.Id == id);

            return View(customer);

        }
        public ActionResult New()
        {

            var viewModel = new CustomerFormViewModel()
            {
                Customer = new Customer(),
                MemberShipTypes = _context.MembershipTypes.ToList()
            };
            return View("CustomerForm",viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel()
                {
                    Customer = customer,
                    MemberShipTypes = _context.MembershipTypes.ToList()
                };

                return View( "CustomerForm",viewModel);
            }
            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                var customerInDb=_context.Customers.Single(c => c.Id==customer.Id);

                customerInDb.Name = customer.Name;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                customerInDb.Birthdate = customer.Birthdate;
            }
            _context.SaveChanges();
            
            
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            var viewModel = new CustomerFormViewModel()
            {
                Customer = customer,
                MemberShipTypes = _context.MembershipTypes.ToList()
            };


            return View("CustomerForm",viewModel);
        }
    }
}