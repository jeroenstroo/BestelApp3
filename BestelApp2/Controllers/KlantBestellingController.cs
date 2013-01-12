using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BestelApp2.Models;
using System.Web.Security;

namespace BestelApp2.Controllers
{
    
    public class KlantBestellingController : Controller
    {
        private BestelApp db = new BestelApp();

        //
        // GET: /KlantBestelling/

        public ActionResult Index()
        {
            int userId = (int) Membership.GetUser().ProviderUserKey; //Vraagt de current UserId op
            var bestellingen = db.Bestellingen.Where(b => b.UserId == userId); //Enkel bestellingen weergeven van de user die is ingelogd
            return View(bestellingen.ToList());
        }

        //
        // GET: /KlantBestelling/Details/5

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
        // GET: /KlantBestelling/Create

        public ActionResult Create()
        {
            //ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName");
            PopulateAlleProducten();
            return View();
        }

        private void PopulateAlleProducten()
        {

            var allProducten = db.Producten;
            var allCategories = db.Categorie;

            List<string> categorieLijst = new List<string>();   //Zet alle categorieën in een lijst, want je kan maar 1 table per keer lezen.
            foreach (var categorie in allCategories)
            {
                categorieLijst.Add(categorie.CategorieNaam);
            }

            var producten = new HashSet<int>(allProducten.Select(i => i.ProductId));
            var viewModel = new List<BestellingsProducten>();

            foreach (var product in allProducten)
            {

                int inttemp = product.CategorieId;
                string stringtemp = categorieLijst[product.CategorieId - 1];

                viewModel.Add(new BestellingsProducten
                {
                    ProductId = product.ProductId,
                    ProductNaam = product.ProductNaam,
                    CategorieNaam = categorieLijst[product.CategorieId - 1], //Gebruik de lijst met categorieën
                    Assigned = false //Alle checkboxes uitgevinkt
                });
            }

            ViewBag.Producten = viewModel;
        }

        //
        // POST: /KlantBestelling/Create

        [HttpPost]
        public ActionResult Create(FormCollection formCollection, string[] selectedProducten)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Bestellingen.Add(bestellingen);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", bestellingen.UserId);
            //return View(bestellingen);
            var bestellingToCreate = new Bestellingen();

            if (TryUpdateModel(bestellingToCreate, "", null, new string[] { "Producten" }))
            {
                try
                {
                    CreateBestellingsProducten(selectedProducten, bestellingToCreate);
                    db.Bestellingen.Add(bestellingToCreate);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateBestellingsProducten(bestellingToCreate);
            return View(bestellingToCreate);
        }

        private void CreateBestellingsProducten(string[] selectedProducten, Bestellingen bestellingToCreate)
        {

            bestellingToCreate.Product = new List<Producten>(); //Beginnen met een lege lijst

            if (selectedProducten == null) //Niks geselecteerd
            {
                return;
            }

            var selectedProductenHS = new HashSet<string>(selectedProducten);

            bestellingToCreate.Datum = DateTime.Now; //Aanmaakdatum
            bestellingToCreate.UserId = (int) Membership.GetUser().ProviderUserKey; //Add user die de bestelling heeft gemaakt

            foreach (var product in db.Producten)
            {
                if (selectedProductenHS.Contains(product.ProductId.ToString())) //Kijk welk product geselecteerd is
                {
                    bestellingToCreate.Product.Add(product); //Product toevoegen

                }
            }
        }

        //
        // GET: /KlantBestelling/Edit/5

        public ActionResult Edit(int id = 0)
        {
            //Bestellingen bestellingen = db.Bestellingen.Find(id);
            //if (bestellingen == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", bestellingen.UserId);
            //return View(bestellingen);

            Bestellingen bestellingen = db.Bestellingen
          .Include(i => i.Product)
          .Where(i => i.BestellingId == id)
          .Single();
            PopulateBestellingsProducten(bestellingen);
            return View(bestellingen);
        }

        private void PopulateBestellingsProducten(Bestellingen bestelling)
        {
            var allProducten = db.Producten;
            var allCategories = db.Categorie;

            List<string> categorieLijst = new List<string>();   //Zet alle categorieën in een lijst, want je kan maar 1 table per keer lezen.
            foreach (var categorie in allCategories)
            {
                categorieLijst.Add(categorie.CategorieNaam);
            }

            var bestellingsProducten = new HashSet<int>(bestelling.Product.Select(c => c.ProductId));
            var viewModel = new List<BestellingsProducten>();

            foreach (var product in allProducten)
            {

                int inttemp = product.CategorieId;
                string stringtemp = categorieLijst[product.CategorieId - 1];

                viewModel.Add(new BestellingsProducten
                {
                    ProductId = product.ProductId,
                    ProductNaam = product.ProductNaam,
                    CategorieNaam = categorieLijst[product.CategorieId - 1], //Gebruik de lijst met categorieën
                    Assigned = bestellingsProducten.Contains(product.ProductId)
                });
            }

            ViewBag.Producten = viewModel;
        }

        //
        // POST: /KlantBestelling/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection formCollection, string[] selectedProducten)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(bestellingen).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", bestellingen.UserId);
            //return View(bestellingen);

            var bestellingToUpdate = db.Bestellingen
                .Include(i => i.Product)
                .Where(i => i.BestellingId == id)
                .Single();
            if (TryUpdateModel(bestellingToUpdate, "", null, new string[] { "Producten" }))
            {
                try
                {
                    UpdateBestellingsProducten(selectedProducten, bestellingToUpdate);
                    db.Entry(bestellingToUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException)
                {
                    //Log the error (add a variable name after DataException)
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateBestellingsProducten(bestellingToUpdate);
            return View(bestellingToUpdate);
        }

        private void UpdateBestellingsProducten(string[] selectedProducten, Bestellingen bestellingToUpdate)
        {

            if (selectedProducten == null)
            {
                bestellingToUpdate.Product = new List<Producten>();
                return;
            }

            var selectedCoursesHS = new HashSet<string>(selectedProducten);
            var instructorCourses = new HashSet<int>
                (bestellingToUpdate.Product.Select(c => c.ProductId));
            foreach (var product in db.Producten)
            {
                if (selectedCoursesHS.Contains(product.ProductId.ToString()))
                {
                    if (!instructorCourses.Contains(product.ProductId))
                    {
                        bestellingToUpdate.Product.Add(product);
                    }
                }
                else
                {
                    if (instructorCourses.Contains(product.ProductId))
                    {
                        bestellingToUpdate.Product.Remove(product);
                    }
                }

            }
        }

        //
        // GET: /KlantBestelling/Delete/5

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
        // POST: /KlantBestelling/Delete/5

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