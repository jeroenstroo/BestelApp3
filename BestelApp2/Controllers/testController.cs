using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BestelApp2.Models;

namespace BestelApp2.Controllers
{
    public class testController : Controller
    {
        private BestelApp db = new BestelApp();

        //
        // GET: /test/

        public ActionResult Index()
        {
            var bestellingen = db.Bestellingen.Include(b => b.UserProfile);
            return View(bestellingen.ToList());
        }

        //
        // GET: /test/Details/5

        public ActionResult Details(int id = 0)
        {
            Bestellingen bestellingen = db.Bestellingen.Find(id);
            if (bestellingen == null)
            {
                return HttpNotFound();
            }
            return View(bestellingen);
        }

        //
        // GET: /test/Create

        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName");
            return View();
        }

        //
        // POST: /test/Create

        [HttpPost]
        public ActionResult Create(Bestellingen bestellingen)
        {
            if (ModelState.IsValid)
            {
                db.Bestellingen.Add(bestellingen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", bestellingen.UserId);
            return View(bestellingen);
        }

        //
        // GET: /test/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Bestellingen bestellingen = db.Bestellingen.Find(id);
            if (bestellingen == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", bestellingen.UserId);
            return View(bestellingen);
        }

        //
        // POST: /test/Edit/5

        [HttpPost]
        public ActionResult Edit(Bestellingen bestellingen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bestellingen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", bestellingen.UserId);
            return View(bestellingen);
        }

        //
        // GET: /test/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Bestellingen bestellingen = db.Bestellingen.Find(id);
            if (bestellingen == null)
            {
                return HttpNotFound();
            }
            return View(bestellingen);
        }

        //
        // POST: /test/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Bestellingen bestellingen = db.Bestellingen.Find(id);
            db.Bestellingen.Remove(bestellingen);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}