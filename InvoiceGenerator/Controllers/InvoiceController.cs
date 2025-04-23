using InvoiceGenerator.Data;
using InvoiceGenerator.Models;
using InvoiceGenerator.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace InvoiceGenerator.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly AppicationDbContext _context;
        public InvoiceController(AppicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var invoices = _context.Invoices.ToList();
            return View(invoices);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InvoiceViewModel model, string InvoiceItemsJson)
        {
            model.Invoice.CustomerAddress = "N/A";
            _context.Invoices.Add(model.Invoice);
            _context.SaveChanges();

            var items = JsonConvert.DeserializeObject<List<InvoiceItem>>(InvoiceItemsJson);
            foreach (var item in items)
            {
                item.Invoice = model.Invoice;
                _context.InvoiceItems.Add(item);
            }

            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Edit(int id)
        {
            var invoice = _context.Invoices.Find(id);

            var items = _context.InvoiceItems.Where(i => i.Invoice.Id == id).ToList();

            var model = new InvoiceViewModel
            {
                Invoice = invoice,
                InvoiceItems = items
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, InvoiceViewModel model, string InvoiceItemsJson)
        {
            model.Invoice.CustomerAddress = "N/A";
            _context.Entry(model.Invoice).State = EntityState.Modified;

            // Remove old items
            var oldItems = _context.InvoiceItems.Where(i => i.Invoice.Id == model.Invoice.Id);
            _context.InvoiceItems.RemoveRange(oldItems);

            // Add updated items
            var newItems = JsonConvert.DeserializeObject<List<InvoiceItem>>(InvoiceItemsJson);
            foreach (var item in newItems)
            {
                item.Invoice = model.Invoice;
                _context.InvoiceItems.Add(item);
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Invoices/Delete/5
        public ActionResult Delete(int id)
        {
            var invoice = _context.Invoices.Find(id);
            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var invoice = _context.Invoices.Find(id);
            if (invoice != null)
            {
                // Delete associated invoice items first
                var items = _context.InvoiceItems.Where(i => i.Invoice.Id == id);
                _context.InvoiceItems.RemoveRange(items);

                _context.Invoices.Remove(invoice);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

    }
}
