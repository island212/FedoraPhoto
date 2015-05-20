using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FedoraPhoto.Models;

namespace FedoraPhoto.Controllers
{
    public class FacturesController : Controller
    {
        private Model1 db = new Model1();

        // GET: Factures
        public ActionResult Index()
        {
            var factures = db.Factures.Include(f => f.Seance);
            return View(factures.ToList());
        }

        // GET: Factures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facture facture = db.Factures.Find(id);
            if (facture == null)
            {
                return HttpNotFound();
            }
            return View(facture);
        }

        // GET: Factures/Create
        public ActionResult Create()
        {
            ViewBag.SeanceID = new SelectList(db.Seances, "SeanceID", "Adresse");
            return View();
        }

        // POST: Factures/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FactureID,FraisDeplacement,FraisVisiteVirtuelle,FraisTVQ,FraisTPS,SeanceID")] Facture facture)
        {
            if (ModelState.IsValid)
            {
                facture.Seance.DateFacture = DateTime.Now;
                db.Factures.Add(facture);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SeanceID = new SelectList(db.Seances, "SeanceID", "Adresse", facture.SeanceID);
            return View(facture);
        }

        // GET: Factures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facture facture = db.Factures.Find(id);
            if (facture == null)
            {
                return HttpNotFound();
            }
            ViewBag.SeanceID = new SelectList(db.Seances, "SeanceID", "Adresse", facture.SeanceID);
            return View(facture);
        }

        // POST: Factures/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FactureID,FraisDeplacement,FraisVisiteVirtuelle,FraisTVQ,FraisTPS,SeanceID")] Facture facture)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facture).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SeanceID = new SelectList(db.Seances, "SeanceID", "Adresse", facture.SeanceID);
            return View(facture);
        }

        // GET: Factures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facture facture = db.Factures.Find(id);
            if (facture == null)
            {
                return HttpNotFound();
            }
            return View(facture);
        }

        // POST: Factures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Facture facture = db.Factures.Find(id);
            db.Factures.Remove(facture);
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
