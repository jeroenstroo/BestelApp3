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
    public class ProductController : Controller
    {
        private BestelApp db = new BestelApp();

        //
        // GET: /Product/

        public ActionResult Index()
        {
            var producten = db.Producten.Include(p => p.Categorie);
            return View(producten.ToList());
        }

        //
        // GET: /Product/Details/5

        public ActionResult Details(int id = 0)
        {
            Producten producten = db.Producten.Find(id);
            if (producten == null)
            {
                return HttpNotFound();
            }
            return View(producten);
        }

        //
        // GET: /Product/Create

        public ActionResult Create()
        {
            ViewBag.CategorieId = new SelectList(db.Categorie, "CategorieId", "CategorieNaam");
            return View();
        }

        //
        // POST: /Product/Create

        [HttpPost]
        public ActionResult Create(Producten producten)
        {
            if (ModelState.IsValid)
            {
                db.Producten.Add(producten);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategorieId = new SelectList(db.Categorie, "CategorieId", "CategorieNaam", producten.CategorieId);
            return View(producten);
        }

        //
        // GET: /Product/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Producten producten = db.Producten.Find(id);
            if (producten == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategorieId = new SelectList(db.Categorie, "CategorieId", "CategorieNaam", producten.CategorieId);
            return View(producten);
        }

        //
        // POST: /Product/Edit/5

        [HttpPost]
        public ActionResult Edit(Producten producten)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producten).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategorieId = new SelectList(db.Categorie, "CategorieId", "CategorieNaam", producten.CategorieId);
            return View(producten);
        }

        //
        // GET: /Product/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Producten producten = db.Producten.Find(id);
            if (producten == null)
            {
                return HttpNotFound();
            }
            return View(producten);
        }

        //
        // POST: /Product/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Producten producten = db.Producten.Find(id);
            db.Producten.Remove(producten);
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