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
                    DateTime dateDebut = DateTime.Now.AddDays(1);
                    dateDebut.AddHours(seance.HeureRDV.Value);
                    dateDebut.AddMinutes(seance.MinuteRDV.Value);
                    if (date < dateDebut)
                        return new ValidationResult("La date doit être au minimum 1 jours après la demande");

                    DateTime dateFin = DateTime.Now.AddDays(15);
                    dateFin.AddHours(seance.HeureRDV.Value);
                    dateFin.AddMinutes(seance.MinuteRDV.Value);
                    if (date > dateFin)
                        return new ValidationResult("La date doit être au maximum 15 jours après la demande.");


                    int heureSeance = seance.HeureRDV.Value * 60 + seance.MinuteRDV.Value;
                    foreach (var item in uow.SeanceRepository.ObtenirSeancesByPhotographeId(seance.PhotographeID))
                    {
                        if (item.HeureRDV != null && item.MinuteRDV != null)
                        {
                            int tempHeureSeance = item.HeureRDV.Value * 60 + item.MinuteRDV.Value;

                            int debutHeure = tempHeureSeance - (60 * 4);
                            int finHeure = tempHeureSeance + (60 * 4);

                            if (item.DateSeance.Value != null && date == item.DateSeance && debutHeure <= heureSeance && finHeure >= heureSeance)
                                return new ValidationResult("Le photographe a déja un rendez à ce moment de la journée.");
                        }
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}