using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FedoraPhoto.Models;
using FedoraPhoto.DAL;

namespace FedoraPhoto.Controllers
{
    public class PhotographesController : Controller
    {
       // private Model1 db = new Model1();
        UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Photographes
        public ActionResult Index()
        {
           // return View(db.Photographes.ToList());
            return View(unitOfWork.PhotographeRepository.Get());
        }

        // GET: Photographes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Photographe photographe = db.Photographes.Find(id);
            Photographe photographe = unitOfWork.PhotographeRepository.GetByID(id);
            if (photographe == null)
            {
                return HttpNotFound();
            }
            return View(photographe);
        }

        // GET: Photographes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Photographes/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PhotographeID,Nom,Prenom")] Photographe photographe)
        {
            if (ModelState.IsValid)
            {
                //db.Photographes.Add(photographe);
               // db.SaveChanges();
                unitOfWork.PhotographeRepository.Insert(photographe);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(photographe);
        }

        // GET: Photographes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Photographe photographe = db.Photographes.Find(id);
            Photographe photographe = unitOfWork.PhotographeRepository.GetByID(id);
            if (photographe == null)
            {
                return HttpNotFound();
            }
            return View(photographe);
        }

        // POST: Photographes/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PhotographeID,Nom,Prenom")] Photographe photographe)
        {
            if (ModelState.IsValid)
            {
              //  db.Entry(photographe).State = EntityState.Modified;
               // db.SaveChanges();
                unitOfWork.PhotographeRepository.Update(photographe);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(photographe);
        }

        // GET: Photographes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           // Photographe photographe = db.Photographes.Find(id);
            Photographe photographe = unitOfWork.PhotographeRepository.GetByID(id);
            if (photographe == null)
            {
                return HttpNotFound();
            }
            return View(photographe);
        }

        // POST: Photographes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           // Photographe photographe = db.Photographes.Find(id);
           // db.Photographes.Remove(photographe);
           // db.SaveChanges();
            Photographe photographe = unitOfWork.PhotographeRepository.GetByID(id);
            unitOfWork.PhotographeRepository.Delete(photographe);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
               // db.Dispose();
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
