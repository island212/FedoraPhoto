﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FedoraPhoto.Models;
using PagedList;
using Ionic.Zip;
using System.IO;
using System.Data.Entity.Infrastructure;
using FedoraPhoto.DAL;

namespace FedoraPhoto.Controllers
{
    public class SeancesController : Controller
    {
       // private Model1 db = new Model1();
        UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Seances
        public ActionResult Index(string sortOrder, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "seanceID" : "";

           // var seances = db.Seances.Include(s => s.Agent).Include(s => s.Forfait).Include(s => s.Photos).Include(s => s.Photographe);
            var seances = unitOfWork.SeanceRepository.Get(includeProperties: "Forfait,Photos,Photographe");

            switch (sortOrder)
            {
                case "seanceID":
                    seances = seances.OrderByDescending(s => s.SeanceID);
                    break;
                case "date_desc":
                    seances = seances.OrderByDescending(s => s.DateSeance);
                    break;
                case "statut":
                    seances = unitOfWork.SeanceRepository.ObtenirSeancesTriesParStatut();
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
           // Seance seance = db.Seances.Find(id);
            Seance seance = unitOfWork.SeanceRepository.GetByID(id);
            if (seance == null)
            {
                return HttpNotFound();
            }
            return View(seance);
        }

        public ActionResult Download(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Seance seance = db.Seances.Find(id);
            Seance seance = unitOfWork.SeanceRepository.GetByID(id);
            if (seance == null)
            {
                return HttpNotFound();
            }

            string pathDirectory = AppDomain.CurrentDomain.BaseDirectory + "Images\\" + seance.SeanceID + "\\";
            string pathFile = AppDomain.CurrentDomain.BaseDirectory + "Temp\\" + DateTime.UtcNow.Ticks.ToString() + ".zip";

            if (Directory.Exists(pathDirectory))
            {
                using (ZipFile zfile = new ZipFile())
                {
                    zfile.AddDirectory(pathDirectory);
                    zfile.Save(pathFile);
                }

                Downloader.Download("Photos" + seance.SeanceID + ".zip", pathFile);
            }

            return View("Details", seance);
        }

        // GET: Seances/Create
        public ActionResult Create()
        {
          //  ViewBag.AgentID = new SelectList(db.Agents, "AgentID", "Nom");
          //  ViewBag.ForfaitID = new SelectList(db.Forfaits, "ForfaitID", "NomForfait");
          //  ViewBag.SeanceID = new SelectList(db.Photos, "PhotoID", "PhotoName");
          //  ViewBag.PhotographeID = new SelectList(db.Photographes, "PhotographeID", "Nom");
            ViewBag.AgentID = new SelectList(unitOfWork.AgentRepository.ObtenirAgents(), "AgentID", "Nom");
            ViewBag.ForfaitID = new SelectList(unitOfWork.ForfaitRepository.ObtenirForfaits(), "ForfaitID", "NomForfait");
            ViewBag.SeanceID = new SelectList(unitOfWork.PhotoRepository.Get(), "PhotoID", "PhotoName");
            ViewBag.PhotographeID = new SelectList(unitOfWork.PhotographeRepository.ObtenirPhotographes(), "PhotographeID", "Nom");
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
                //db.Seances.Add(seance);
                //db.SaveChanges();
                unitOfWork.SeanceRepository.InsererSeance(seance);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            //ViewBag.AgentID = new SelectList(db.Agents, "AgentID", "Nom", seance.AgentID);
            //ViewBag.ForfaitID = new SelectList(db.Forfaits, "ForfaitID", "NomForfait", seance.ForfaitID);
            //ViewBag.SeanceID = new SelectList(db.Photos, "PhotoID", "PhotoName", seance.SeanceID);
            //ViewBag.PhotographeID = new SelectList(db.Photographes, "PhotographeID", "Nom", seance.PhotographeID);
            ViewBag.AgentID = new SelectList(unitOfWork.AgentRepository.ObtenirAgents(), "AgentID", "Nom", seance.AgentID);
            ViewBag.ForfaitID = new SelectList(unitOfWork.ForfaitRepository.ObtenirForfaits(), "ForfaitID", "NomForfait", seance.ForfaitID);
            ViewBag.SeanceID = new SelectList(unitOfWork.PhotoRepository.Get(), "PhotoID", "PhotoName", seance.SeanceID);
            ViewBag.PhotographeID = new SelectList(unitOfWork.PhotographeRepository.ObtenirPhotographes(), "PhotographeID", "Nom", seance.PhotographeID);
            return View(seance);
        }

        // GET: Seances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Seance seance = db.Seances.Find(id);
            Seance seance = unitOfWork.SeanceRepository.ObtenirSeanceParID(id);
            if (seance == null)
            {
                return HttpNotFound();
            }
            //ViewBag.AgentID = new SelectList(db.Agents, "AgentID", "Nom", seance.AgentID);
            //ViewBag.ForfaitID = new SelectList(db.Forfaits, "ForfaitID", "NomForfait", seance.ForfaitID);
            //ViewBag.SeanceID = new SelectList(db.Photos, "PhotoID", "PhotoName", seance.SeanceID);
            //ViewBag.PhotographeID = new SelectList(db.Photographes, "PhotographeID", "Nom", seance.PhotographeID);

            ViewBag.AgentID = new SelectList(unitOfWork.AgentRepository.ObtenirAgents(), "AgentID", "Nom", seance.AgentID);
            ViewBag.ForfaitID = new SelectList(unitOfWork.ForfaitRepository.ObtenirForfaits(), "ForfaitID", "NomForfait", seance.ForfaitID);
            ViewBag.SeanceID = new SelectList(unitOfWork.PhotoRepository.Get(), "PhotoID", "PhotoName", seance.SeanceID);
            ViewBag.PhotographeID = new SelectList(unitOfWork.PhotographeRepository.ObtenirPhotographes(), "PhotographeID", "Nom", seance.PhotographeID);

            return View(seance);
        }

        // POST: Seances/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SeanceID,AgentID,PhotographeID,Adresse,Telephone1,Telephone2,Telephone3,DateSeance,HeureRDV,MinuteRDV,Nom,Prenom,ForfaitID,Statut,DateDispo,DateFacture,rowVersionSeance")] Seance seance)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    seance.Statut = "demandée";
                  //  db.Entry(seance).State = EntityState.Modified;
                  //  db.SaveChanges();
                    unitOfWork.SeanceRepository.InsererSeance(seance);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                RecupererErreurUpdate(ex);
            }
            //ViewBag.AgentID = new SelectList(db.Agents, "AgentID", "Nom", seance.AgentID);
            //ViewBag.ForfaitID = new SelectList(db.Forfaits, "ForfaitID", "NomForfait", seance.ForfaitID);
            //ViewBag.SeanceID = new SelectList(db.Photos, "PhotoID", "PhotoName", seance.SeanceID);
            //ViewBag.PhotographeID = new SelectList(db.Photographes, "PhotographeID", "Nom", seance.PhotographeID);

            ViewBag.AgentID = new SelectList(unitOfWork.AgentRepository.ObtenirAgents(), "AgentID", "Nom", seance.AgentID);
            ViewBag.ForfaitID = new SelectList(unitOfWork.ForfaitRepository.ObtenirForfaits(), "ForfaitID", "NomForfait", seance.ForfaitID);
            ViewBag.SeanceID = new SelectList(unitOfWork.PhotoRepository.Get(), "PhotoID", "PhotoName", seance.SeanceID);
            ViewBag.PhotographeID = new SelectList(unitOfWork.PhotographeRepository.ObtenirPhotographes(), "PhotographeID", "Nom", seance.PhotographeID);

            return View(seance);
        }

        // GET: Seances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           // Seance seance = db.Seances.Find(id);
            Seance seance = unitOfWork.SeanceRepository.ObtenirSeanceParID(id);
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
            //Seance seance = db.Seances.Find(id);
            //db.Seances.Remove(seance);
            //db.SaveChanges();
            Seance seance = unitOfWork.SeanceRepository.ObtenirSeanceParID(id);
            unitOfWork.SeanceRepository.Delete(seance);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }


        private void RecupererErreurUpdate(DbUpdateConcurrencyException ex)
        {
            ModelState.AddModelError("", "Il y a deja eu une modification sur cette séance pendant que vous le le changiez, les valeurs modifiés sont à coté les champs.");
            var entry = ex.Entries.Single();
            var clientValues = (Seance)entry.Entity;
            var databaseValues = (Seance)entry.GetDatabaseValues().ToObject();
            if (databaseValues == null)
            {
                ModelState.AddModelError(string.Empty, "On ne peut pas effectuer les changements sur la séance car un utilisateur a supprimé cette séance.");
            }
            else
            {

                if (databaseValues.Adresse != clientValues.Adresse)
                    ModelState.AddModelError("Adresse", "Valeur courante: " + databaseValues.Adresse);
                if (databaseValues.Agent != clientValues.Agent)
                    ModelState.AddModelError("Agent", "Valeur courante: " + databaseValues.Agent);
                if (databaseValues.DateDispo != clientValues.DateDispo)
                {
                    ModelState.AddModelError("DateDispo", "Valeur courante: " + databaseValues.DateDispo);

                }

                if (databaseValues.Nom != clientValues.Nom)
                    ModelState.AddModelError("Nom", "Valeur courante: " + databaseValues.Nom);
                if (databaseValues.Prenom != clientValues.Prenom)
                    ModelState.AddModelError("Prenom", "Valeur courante: " + databaseValues.Prenom);
                if (databaseValues.Telephone1 != clientValues.Telephone1)
                    ModelState.AddModelError("Prenom", "Valeur courante: " + databaseValues.Telephone1);

            }
        }
    }
}
