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
    public class CategorieController : Controller
    {
        private BestelApp db = new BestelApp();

        //
        // GET: /Categorie/

        public ActionResult Index()
        {
            return View(db.Categorie.ToList());
        }

        //
        // GET: /Categorie/Details/5

        public ActionResult Details(int id = 0)
        {
            Categorie categorie = db.Categorie.Find(id);
            if (categorie == null)
            {
                return HttpNotFound();
            }
            return View(categorie);
        }

        //
        // GET: /Categorie/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Categorie/Create

        [HttpPost]
        public ActionResult Create(Categorie categorie)
        {
            if (ModelState.IsValid)
            {
                db.Categorie.Add(categorie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categorie);
        }

        //
        // GET: /Categorie/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Categorie categorie = db.Categorie.Find(id);
            if (categorie == null)
            {
                return HttpNotFound();
            }
            return View(categorie);
        }

        //
        // POST: /Categorie/Edit/5

        [HttpPost]
        public ActionResult Edit(Categorie categorie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categorie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categorie);
        }

        //
        // GET: /Categorie/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Categorie categorie = db.Categorie.Find(id);
            if (categorie == null)
            {
                return HttpNotFound();
            }
            return View(categorie);
        }

        //
        // POST: /Categorie/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Categorie categorie = db.Categorie.Find(id);
            db.Categorie.Remove(categorie);
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