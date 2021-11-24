using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using InsuranceCompany.Models;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using InsuranceCompany.ViewModels;
using InsuranceCompany.Utils;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InsuranceCompany.Controllers
{
    public class ClientController : Controller
    {
        private readonly InsuranceContext _context;

        public static int ID { get; private set; }

        public ClientController(InsuranceContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var clients = _context.Clients;
            return View(await clients.ToListAsync());

        }

        public IActionResult Welcome(string name = "empty", int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;

            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FirstOrDefaultAsync(e => e.ID == id);
            if (client == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Where(m => m.CLIENT_ID == id.ToString())
                .ToListAsync();

            var ordersSerialized = new List<Orders>();

            if (orders.Capacity > 0)
            {

                var products = await this._context.Products.ToListAsync();

                foreach (Orders order in orders)
                {
                    var foundProduct = products.Find(Product => Product.ID.Equals(int.Parse(order.PRODUCT_ID)));

                    ordersSerialized.Add(new Orders
                    {
                        ID = order.ID,
                        CLIENT_ID = client?.FIRST_NAME + ' ' + client?.LAST_NAME,
                        PRODUCT_ID = foundProduct?.Policy,
                        VALIDITY_FROM = DateHelpers
                            .UnixTimestampToDateTime(int.Parse(order.VALIDITY_FROM))
                            .ToString("yyyy-MM-dd"),
                        VALIDITY_TO = DateHelpers
                            .UnixTimestampToDateTime(int.Parse(order.VALIDITY_TO))
                            .ToString("yyyy-MM-dd"),
                    });
                }
            }

            var vm = new ClientDetailsViewModel{
                Client = client,
                Orders = ordersSerialized,
            };

            return View(vm);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clients = await _context.Clients.FindAsync(id);
            if (clients == null)
            {
                return NotFound();
            }
            return View(clients);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FIRST_NAME,LAST_NAME,Birth,Adress,PASSPORT,PHONE,EMAIL")] Clients client)
        {
            if (id != client.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientsExists(client.ID))
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
            return View(client);
        }

        private bool ClientsExists(int iD)
        {
            throw new NotImplementedException();
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<ActionResult> Delete(int id)
        {

            var client = await _context.Clients.FirstOrDefaultAsync(e => e.ID == id);
            if (client == null) {
                return NotFound();
            }

            return View(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, string confirmButton)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(e => e.ID == id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);

            var orders = await _context.Orders.Where(e => e.CLIENT_ID == id.ToString()).ToListAsync();

            foreach (Orders order in orders)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // POST: Client/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("ID,FIRST_NAME,LAST_NAME,Birth,Adress,PASSPORT,PHONE,EMAIL")] Clients client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(client);
        }
    }
    
}

        