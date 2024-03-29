﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FedoraPhoto.Models;
using System.IO;
using System.Security.AccessControl;
using System.Configuration;
using System.Security.Principal;
using FedoraPhoto.DAL;

namespace FedoraPhoto.Controllers
{
    public class PhotosController : Controller
    {
        //private Model1 db = new Model1();
        UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Photos
        public ActionResult Index()
        {
            //var photos = db.Photos.Include(p => p.Seance);
            var photos = unitOfWork.PhotoRepository.Get(includeProperties:"Seance");
            return View(photos.ToList());
        }

        // GET: Photos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Photo photo = db.Photos.Find(id);
            Photo photo = unitOfWork.PhotoRepository.GetByID(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // GET: Photos/Create
        public ActionResult Create()
        {
           // ViewBag.SeanceID = new SelectList(db.Seances, "SeanceID", "Adresse");
            ViewBag.SeanceID = new SelectList(unitOfWork.SeanceRepository.Get(), "SeanceID", "Adresse");
            return View();
        }

        // POST: Photos/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PhotoID,SeanceID")] Photo photo, IEnumerable<HttpPostedFileBase> imageFiles)
        {
            if (ModelState.IsValid)
            {
                string pathDirectory = AppDomain.CurrentDomain.BaseDirectory + "Images\\" + photo.SeanceID + "\\";
                if (!Directory.Exists(pathDirectory))
                {
                    DirectorySecurity securityRules = new DirectorySecurity();
                    SecurityIdentifier everyone = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
                    securityRules.AddAccessRule(new FileSystemAccessRule(everyone, FileSystemRights.FullControl, AccessControlType.Allow));
                    var repertoire = Directory.CreateDirectory(pathDirectory, securityRules);
                }

                DateTime currentTime = DateTime.Now;
                foreach (var imageFile in imageFiles)
                {
                    Photo imagePhoto = new Photo();
                    imagePhoto.SeanceID = photo.SeanceID;
                    imagePhoto.Seance = photo.Seance;
                    photo.Seance.DateDispo = currentTime;

                    string pathFile = pathDirectory + imageFile.FileName;
                    imagePhoto.PhotoPath = "Images\\" + photo.SeanceID + "\\" + imageFile.FileName;
                    imagePhoto.PhotoType = imageFile.ContentType;
                    imagePhoto.PhotoName = imageFile.FileName;
                    imageFile.SaveAs(pathFile);

                   // db.Photos.Add(imagePhoto);
                    unitOfWork.PhotoRepository.Insert(imagePhoto);
                }

                //db.SaveChanges();
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            ViewBag.SeanceID = new SelectList(unitOfWork.SeanceRepository.Get(), "SeanceID", "Adresse", photo.SeanceID);
            return View(photo);
        }

        // GET: Photos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           // Photo photo = db.Photos.Find(id);
            Photo photo = unitOfWork.PhotoRepository.GetByID(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            
         //   ViewBag.SeanceID = new SelectList(db.Seances, "SeanceID", "Adresse", photo.SeanceID);
            ViewBag.SeanceID = new SelectList(unitOfWork.SeanceRepository.Get(), "SeanceID", "Adresse", photo.SeanceID);
            return View(photo);
        }

        // POST: Photos/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PhotoID,SeanceID,PhotoName,PhotoPath,PhotoType")] Photo photo)
        {
            if (ModelState.IsValid)
            {
               // db.Entry(photo).State = EntityState.Modified;
               // db.SaveChanges();
                unitOfWork.PhotoRepository.Update(photo);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
           // ViewBag.SeanceID = new SelectList(db.Seances, "SeanceID", "Adresse", photo.SeanceID);
            ViewBag.SeanceID = new SelectList(unitOfWork.SeanceRepository.Get(), "SeanceID", "Adresse", photo.SeanceID);
            return View(photo);
        }

        // GET: Photos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           // Photo photo = db.Photos.Find(id);
            Photo photo = unitOfWork.PhotoRepository.GetByID(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: Photos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           // Photo photo = db.Photos.Find(id);
            Photo photo = unitOfWork.PhotoRepository.GetByID(id);
            System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + photo.PhotoPath);
           // db.Photos.Remove(photo);
           // db.SaveChanges();
            unitOfWork.PhotoRepository.Delete(photo);
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
    }
}
