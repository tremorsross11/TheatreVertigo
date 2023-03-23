using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheatreCMS3.Areas.Rent.Models;
using TheatreCMS3.Models;

namespace TheatreCMS3.Areas.Rent.Controllers
{
    public class RentalHistoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rent/RentalHistories
        public ViewResult Index(string sortOrder)
        {
            // Set up sorting links
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "RentalHistoryId" ? "rentalhistoryid_desc" : "RentalHistoryId";
            
            //Selecting all rental histories from the database
            var rentals = db.RentalHistories.AsQueryable();
                          
            // Apply sort options
            switch (sortOrder)
            {
                case "Damaged":
                    rentals = rentals.Where(r => r.RentalDamaged).OrderByDescending(r => r.RentalHistoryId);
                    break;
                case "Undamaged":
                    rentals = rentals.Where(r => !r.RentalDamaged).OrderByDescending(r => r.RentalHistoryId);
                    break;
                case "A-Z":
                    rentals = rentals.OrderBy(r => r.Rental).ThenByDescending(r => r.RentalHistoryId);
                    break;
                case "Z-A":
                    rentals = rentals.OrderByDescending(r => r.Rental).ThenByDescending(r => r.RentalHistoryId);
                    break;
                default:
                    rentals = rentals.OrderByDescending(r => r.RentalHistoryId);
                    break;
            }
            return View(rentals.ToList());
        }

        // GET: Rent/RentalHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentalHistory rentalHistory = db.RentalHistories.Find(id);
            if (rentalHistory == null)
            {
                return HttpNotFound();
            }
            return View(rentalHistory);
        }

        // GET: Rent/RentalHistories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rent/RentalHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RentalHistoryId,RentalDamaged,DamagesIncurred,Rental")] RentalHistory rentalHistory)
        {
            if (ModelState.IsValid)
            {
                db.RentalHistories.Add(rentalHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rentalHistory);
        }

        // GET: Rent/RentalHistories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentalHistory rentalHistory = db.RentalHistories.Find(id);
            if (rentalHistory == null)
            {
                return HttpNotFound();
            }
            return View(rentalHistory);
        }

        // POST: Rent/RentalHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RentalHistoryId,RentalDamaged,DamagesIncurred,Rental")] RentalHistory rentalHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rentalHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rentalHistory);
        }

        // GET: Rent/RentalHistories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentalHistory rentalHistory = db.RentalHistories.Find(id);
            if (rentalHistory == null)
            {
                return HttpNotFound();
            }
            return View(rentalHistory);
        }

        // POST: Rent/RentalHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RentalHistory rentalHistory = db.RentalHistories.Find(id);
            db.RentalHistories.Remove(rentalHistory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
