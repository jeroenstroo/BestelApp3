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
    public class KlantenController : Controller
    {
        private BestelApp db = new BestelApp();

        //
        // GET: /Klanten/

        public ActionResult Index()
        {
            return View(db.Klanten.ToList());
        }

        //
        // GET: /Klanten/Details/5

        public ActionResult Details(int id = 0)
        {
            Klanten klanten = db.Klanten.Find(id);
            if (klanten == null)
            {
                return HttpNotFound();
            }
            return View(klanten);
        }

        //
        // GET: /Klanten/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Klanten/Create

        [HttpPost]
        public ActionResult Create(Klanten klanten)
        {
            if (ModelState.IsValid)
            {
                db.Klanten.Add(klanten);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(klanten);
        }

        //
        // GET: /Klanten/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Klanten klanten = db.Klanten.Find(id);
            if (klanten == null)
            {
                return HttpNotFound();
            }
            return View(klanten);
        }

        //
        // POST: /Klanten/Edit/5

        [HttpPost]
        public ActionResult Edit(Klanten klanten)
        {
            if (ModelState.IsValid)
            {
                db.Entry(klanten).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(klanten);
        }

        //
        // GET: /Klanten/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Klanten klanten = db.Klanten.Find(id);
            if (klanten == null)
            {
                return HttpNotFound();
            }
            return View(klanten);
        }

        //
        // POST: /Klanten/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Klanten klanten = db.Klanten.Find(id);
            db.Klanten.Remove(klanten);
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