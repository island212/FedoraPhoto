using FedoraPhoto.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace FedoraPhoto.Models
{
    public class ValidateDateSeance : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value == null)
                return ValidationResult.Success;

            DateTime date;
            if (DateTime.TryParse(value.ToString(), out date))
            {
                UnitOfWork uow = new UnitOfWork();
                Seance seance = (Seance)validationContext.ObjectInstance;

                if (seance.MinuteRDV != null && seance.HeureRDV != null)
                {
                    date = date.AddHours(seance.HeureRDV.Value).AddMinutes(seance.MinuteRDV.Value);

                    DateTime dateDebut = DateTime.Now.AddDays(1);
                    if (date < dateDebut)
                        return new ValidationResult("La date doit être au minimum 1 jours après la demande");

                    DateTime dateFin = DateTime.Now.AddDays(15);
                    if (date > dateFin)
                        return new ValidationResult("La date doit être au maximum 15 jours après la demande.");


                    DateTime dateDebutRDV = date.AddHours(-4);
                    DateTime dateFinRDV = date.AddHours(4);
                    foreach (var item in uow.SeanceRepository.ObtenirSeancesByPhotographeId(seance.PhotographeID))
                    {
                        if (item.HeureRDV != null && item.MinuteRDV != null && item.DateSeance != null)
                        {
                            DateTime datePhotographe = item.DateSeance.Value.AddHours(item.HeureRDV.Value).AddMinutes(item.MinuteRDV.Value);
                            if (seance.SeanceID != item.SeanceID && datePhotographe.Year == date.Year && date.Month == datePhotographe.Month &&
                                date.Day == datePhotographe.Day && dateDebutRDV <= datePhotographe && datePhotographe <= dateFinRDV)
                            return new ValidationResult("Le photographe a déja un rendez à ce moment de la journée.");
                        }
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}