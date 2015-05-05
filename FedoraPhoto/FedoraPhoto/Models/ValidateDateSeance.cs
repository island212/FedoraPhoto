using FedoraPhoto.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
                DateTime dateDebut = DateTime.Now.AddDays(1);
                if (date < dateDebut)
                    return new ValidationResult("La date doit être au minimum 1 jours après la demande");

                DateTime dateFin = DateTime.Now.AddDays(15);
                if (date > dateFin)
                    return new ValidationResult("La date doit être au maximum 15 jours après la demande.");

                UnitOfWork uow = new UnitOfWork();
                Seance seance = (Seance)validationContext.ObjectInstance;

                int heureSeance = (int)seance.HeureRDV * 60 + (int)seance.MinuteRDV;
                foreach (var item in uow.SeanceRepository.ObtenirSeancesByPhotographeId(seance.PhotographeID))
                {
                    int tempHeureSeance = (int)item.HeureRDV * 60 + (int)item.MinuteRDV;

                    int debutHeure = tempHeureSeance - (60 * 4);
                    int finHeure = tempHeureSeance + (60 * 4);

                    if (item.DateSeance.Value != null && debutHeure <= heureSeance && finHeure >= heureSeance)
                        return new ValidationResult("Le photographe a déja un rendez à ce moment de la journée.");
                }
            }

            return ValidationResult.Success;
        }
    }
}