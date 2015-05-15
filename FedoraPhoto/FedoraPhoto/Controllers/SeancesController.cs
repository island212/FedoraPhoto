using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FedoraPhoto.Models;
using PagedList;

namespace FedoraPhoto.Controllers
{
    public class SeancesController : Controller
    {
        private Model1 db = new Model1();

        // GET: Seances
        public ActionResult Index(string sortOrder, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "seanceID" : "";

            var seances = db.Seances.Include(s => s.Agent).Include(s => s.Forfait).Include(s => s.Photos).Include(s => s.Photographe);

            switch (sortOrder)
            {
                case "seanceID":
                    seances = seances.OrderByDescending(s => s.SeanceID);
                    break;
                case "date_desc":
                    seances = seances.OrderByDescending(s => s.DateSeance);
                    break;
                case "statut":
                    List<Seance> seancesTries = new List<Seance>();
                    var lst_StatutSeance = new Dictionary<string, List<Seance>>();

                    foreach (Seance seance in seances)
                    {
                        if(!lst_StatutSeance.ContainsKey(seance.Statut))
                        {
                            lst_StatutSeance[seance.Statut] = new List<Seance>();
                        }
                        lst_StatutSeance[seance.Statut].Add(seance);
                    }

                    //foreach (var dictionnaireItem in lst_StatutSeance)
                    //{
                    //    foreach (var seance in dictionnaireItem.Value)
                    //    {
                    //        seancesTries.Add(seance);
                    //    }
                    //}

                    if(lst_StatutSeance.ContainsKey("demandée"))
                    {
                        foreach (var seance in lst_StatutSeance["demandée"])
                        {
                            seancesTries.Add(seance);
                        }
                    }

                    if(lst_StatutSeance.ContainsKey("Confirmée"))
                    {
                        foreach (var seance in lst_StatutSeance["Confirmée"])
                        {
                            seancesTries.Add(seance);
                        }
                    }

                    if(lst_StatutSeance.ContainsKey("Reportée"))
                    {
                        foreach (var seance in lst_StatutSeance["Reportée"])
                        {
                            seancesTries.Add(seance);
                        }
                    }

                    if(lst_StatutSeance.ContainsKey("Réalisée"))
                    {
                        foreach (var seance in lst_StatutSeance["Réalisée"])
                        {
                            seancesTries.Add(seance);
                        }
                    }

                    if(lst_StatutSeance.ContainsKey("Livrée"))
                    {
                        foreach (var seance in lst_StatutSeance["Livrée"])
                        {
                            seancesTries.Add(seance);
                        }
                    }

                    if(lst_StatutSeance.ContainsKey("Facturée"))
                    {
                        foreach (var seance in lst_StatutSeance["Facturée"])
                        {
                            seancesTries.Add(seance);
                        }
                    }

                    seances = seancesTries.AsQueryable();
                    break;
                default:
                    seances = seances.OrderByDescending(s => s.SeanceID);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(seances.ToPagedList(pageNumber, pageSize));
        }

        // GET: Seances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seance seance = db.Seances.Find(id);
            if (seance == null)
            {
                return HttpNotFound();
            }
            return View(seance);
        }

        // GET: Seances/Create
        public ActionResult Create()
        {
            ViewBag.AgentID = new SelectList(db.Agents, "AgentID", "Nom");
            ViewBag.ForfaitID = new SelectList(db.Forfaits, "ForfaitID", "NomForfait");
            ViewBag.SeanceID = new SelectList(db.Photos, "PhotoID", "PhotoName");
            ViewBag.PhotographeID = new SelectList(db.Photographes, "PhotographeID", "Nom");
            return View();
        }

        // POST: Seances/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SeanceID,AgentID,PhotographeID,Adresse,Telephone1,Telephone2,Telephone3,DateSeance,HeureRDV,MinuteRDV,Nom,Prenom,ForfaitID,Statut,DateDispo,DateFacture")] Seance seance)
        {
            if (ModelState.IsValid)
            {
                seance.Statut = "demandée";
                db.Seances.Add(seance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AgentID = new SelectList(db.Agents, "AgentID", "Nom", seance.AgentID);
            ViewBag.ForfaitID = new SelectList(db.Forfaits, "ForfaitID", "NomForfait", seance.ForfaitID);
            ViewBag.SeanceID = new SelectList(db.Photos, "PhotoID", "PhotoName", seance.SeanceID);
            ViewBag.PhotographeID = new SelectList(db.Photographes, "PhotographeID", "Nom", seance.PhotographeID);
            return View(seance);
        }

        // GET: Seances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seance seance = db.Seances.Find(id);
            if (seance == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgentID = new SelectList(db.Agents, "AgentID", "Nom", seance.AgentID);
            ViewBag.ForfaitID = new SelectList(db.Forfaits, "ForfaitID", "NomForfait", seance.ForfaitID);
            ViewBag.SeanceID = new SelectList(db.Photos, "PhotoID", "PhotoName", seance.SeanceID);
            ViewBag.PhotographeID = new SelectList(db.Photographes, "PhotographeID", "Nom", seance.PhotographeID);
            return View(seance);
        }

        // POST: Seances/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SeanceID,AgentID,PhotographeID,Adresse,Telephone1,Telephone2,Telephone3,DateSeance,HeureRDV,MinuteRDV,Nom,Prenom,ForfaitID,Statut,DateDispo,DateFacture")] Seance seance)
        {
            if (ModelState.IsValid)
            {
                seance.Statut = "demandée";
                db.Entry(seance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AgentID = new SelectList(db.Agents, "AgentID", "Nom", seance.AgentID);
            ViewBag.ForfaitID = new SelectList(db.Forfaits, "ForfaitID", "NomForfait", seance.ForfaitID);
            ViewBag.SeanceID = new SelectList(db.Photos, "PhotoID", "PhotoName", seance.SeanceID);
            ViewBag.PhotographeID = new SelectList(db.Photographes, "PhotographeID", "Nom", seance.PhotographeID);
            return View(seance);
        }

        // GET: Seances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seance seance = db.Seances.Find(id);
            if (seance == null)
            {
                return HttpNotFound();
            }
            return View(seance);
        }

        // POST: Seances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Seance seance = db.Seances.Find(id);
            db.Seances.Remove(seance);
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
