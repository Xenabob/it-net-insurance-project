using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InsuranceCompany.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using InsuranceCompany.Utils;



// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InsuranceCompany.Controllers
{
    public class OrderController : Controller
    {
        private readonly InsuranceContext _context;

        public OrderController(InsuranceContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var clients = await this._context.Clients.ToListAsync();
            var products = await this._context.Products.ToListAsync();

            var orders = new List<Orders>();
            foreach (Orders order in (await _context.Orders.ToListAsync()))
            {
                var foundClient = clients.Find(Client => Client.ID.Equals(int.Parse(order.CLIENT_ID)));
                var foundProduct = products.Find(Product => Product.ID.Equals(int.Parse(order.PRODUCT_ID)));

                orders.Add(new Orders
                {
                    ID = order.ID,
                    CLIENT_ID = foundClient?.FIRST_NAME + ' ' + foundClient?.LAST_NAME,
                    PRODUCT_ID = foundProduct?.Policy,
                    VALIDITY_FROM = DateHelpers
                        .UnixTimestampToDateTime(int.Parse(order.VALIDITY_FROM))
                        .ToString("yyyy-MM-dd"),
                    VALIDITY_TO = DateHelpers
                        .UnixTimestampToDateTime(int.Parse(order.VALIDITY_TO))
                        .ToString("yyyy-MM-dd"),
                });
            }

            return View(orders);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);


        }
        public async Task<IActionResult> Edit(int? id)
        {

            await this.FetchProductAndClients();
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            order.VALIDITY_FROM = DateHelpers
                .UnixTimestampToDateTime(int.Parse(order.VALIDITY_FROM))
                .ToString("yyyy-MM-dd");

            order.VALIDITY_TO = DateHelpers
                .UnixTimestampToDateTime(int.Parse(order.VALIDITY_TO))
                .ToString("yyyy-MM-dd");

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID, PRODUCT_ID, CLIENT_ID, VALIDITY_FROM, VALIDITY_TO")] Orders order)
        {
            if (id != order.ID)
            {
                return NotFound();
            }

            order.VALIDITY_FROM = DateHelpers.ToUnixTimestamp(DateTime.Parse(order.VALIDITY_FROM.ToString())).ToString();
            order.VALIDITY_TO = DateHelpers.ToUnixTimestamp(DateTime.Parse(order.VALIDITY_TO.ToString())).ToString();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientsExists(order.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        private bool ClientsExists(int iD)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> Create()
        {
            await this.FetchProductAndClients();


            return View();
        }
        private async Task FetchProductAndClients()
        {
            var clients = await this._context.Clients.ToListAsync();
            var products = await this._context.Products.ToListAsync();


            ViewData["Clients"] = clients
                .Select(client => new SelectListItem
                {
                    Value = client.ID.ToString(),
                    Text = client.FIRST_NAME.ToString() + ' ' + client.LAST_NAME.ToString()
                }).ToList();

            ViewData["Products"] = products
                .Select(product => new SelectListItem
                {
                    Value = product.ID.ToString(),
                    Text = product.Policy.ToString()
                }).ToList();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("PRODUCT_ID, CLIENT_ID, VALIDITY_FROM, VALIDITY_TO")] Orders order

            )

        {
            order.VALIDITY_FROM = DateHelpers.ToUnixTimestamp(DateTime.Parse(order.VALIDITY_FROM.ToString())).ToString();
            order.VALIDITY_TO = DateHelpers.ToUnixTimestamp(DateTime.Parse(order.VALIDITY_TO.ToString())).ToString();
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(e => e.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, string confirmButton)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(e => e.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}

