
using CascadeComboboxAsp2.Data;
using CascadeComboboxAsp2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CascadeComboboxAsp2.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var webAppDbContext = _context.Customers.Include(c => c.City);
            return View(await webAppDbContext.ToListAsync());
        }


        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.City)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }


        // GET: Customers/Create
        [HttpGet]
        public ActionResult Create()
        {
            CustomerFormVM model = new CustomerFormVM();
            ConfigureViewModel(model);
            return View(model);
        }
        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerFormVM vm)
        {
            if (ModelState.IsValid)
            {
                var customer = new Customer();
                {
                    customer.FirstName = vm.FirstName;
                    customer.LastName = vm.LastName;
                    customer.CityId = vm.SelectedCity.Value;
                    customer.LocalityId = vm.SelectedLocality.Value;
                    customer.SubLocalityId = vm.SelectedSubLocality.Value;
                }
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            else
            {
                ConfigureViewModel(vm);
                return View(vm);
            }
        }


        [HttpGet]
        public JsonResult FetchLocalities(int ID)
        {
            var data = _context.Localities
                .Where(l => l.CityId == ID)
                .Select(l => new { val = l.Id, text = l.Name });
            return Json(data);
        }


        [HttpGet]
        public JsonResult FetchSubLocalities(int ID)
        {
            var data = _context.SubLocalities
                .Where(l => l.LocalityId == ID)
                .Select(l => new { val = l.Id, text = l.Name });
            return Json(data);
        }


        private void ConfigureViewModel(CustomerFormVM model)
        {
           List<City> cities = _context.Cities.ToList();
            model.CityList = new SelectList(cities, "Id", "Name");
          
            if (model.SelectedCity.HasValue)
            {
                IEnumerable<Locality> localities = _context.Localities.Where(l => l.CityId == model.SelectedCity.Value);
                model.LocalityList = new SelectList(localities, "Id", "Name");
            }
            else
            {
                model.LocalityList = new SelectList(Enumerable.Empty<SelectListItem>());
            }
            if (model.SelectedLocality.HasValue)
            {
                IEnumerable<SubLocality> subLocalities = _context.SubLocalities.Where(l => l.LocalityId == model.SelectedLocality.Value);
                model.SubLocalityList = new SelectList(subLocalities, "Id", "Name");
            }
            else
            {
                model.SubLocalityList = new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            
            /*var customervm = new CustomerFormVM();
            {
                Customer customer = _context.Customers.SingleOrDefault(c => c.Id == id);
                if (customer == null)
                {
                    return NotFound();
                }

                customervm.CustomerId = customer.Id;
                customervm.SelectedCity = customer.CityId;
                customervm.SelectedLocality = customer.LocalityId;
                customervm.SelectedSubLocality = customer.SubLocalityId;
                customervm.FirstName = customer.FirstName;
                customervm.LastName = customer.LastName;
                
            }*/
            CustomerFormVM modelvm = new CustomerFormVM();
            {
                Customer customer = _context.Customers.SingleOrDefault(c => c.Id == id);
                var city = _context.Cities.SingleOrDefault(c => c.Id == customer.CityId); 
                var locality = _context.Localities
                    .SingleOrDefault(l => l.Id == customer.LocalityId);
                var subLocality = _context.SubLocalities
                    .SingleOrDefault(l => l.Id == customer.SubLocalityId);
                modelvm.FirstName = customer.FirstName;
                modelvm.CustomerId = customer.Id;
                modelvm.LastName = customer.LastName;
                modelvm.SelectedCity = city.Id;
                modelvm.SelectedLocality = locality.Id;
                modelvm.SelectedSubLocality = subLocality.Id;
                ConfigureViewModel(modelvm);
            }
            return View(modelvm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerFormVM vmEdit)
        {
            if (ModelState.IsValid)
            {
                Customer customer = _context.Customers.SingleOrDefault(c => c.Id == vmEdit.CustomerId);

                if (customer == null)
                {
                    return NotFound();
                }

                customer.Id = vmEdit.CustomerId;
                customer.FirstName = vmEdit.FirstName;
                customer.LastName = vmEdit.LastName;
                customer.CityId = Convert.ToInt32(vmEdit.SelectedCity.ToString());
                customer.LocalityId = Convert.ToInt32(vmEdit.SelectedLocality.ToString());
                customer.SubLocalityId = Convert.ToInt32(vmEdit.SelectedSubLocality.ToString());


                _context.Entry(customer).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vmEdit);
        }


        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.City)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }


        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.SingleOrDefaultAsync(m => m.Id == id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }

    }
}
