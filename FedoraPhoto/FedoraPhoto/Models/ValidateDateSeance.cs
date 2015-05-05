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
                int photographeId = ((Seance)validationContext.ObjectInstance).PhotographeID;
                foreach (var item in uow.SeanceRepository.ObtenirSeancesByPhotographeId(photographeId))
                    if (item.DateSeance.Value != null && item.DateSeance.Value.AddHours(4) > date && item.DateSeance.Value.AddHours(-4) < date)
                        return new ValidationResult("Le photographe a déja un rendez à ce moment de la journée.");
            }

            return ValidationResult.Success;
        }
    }
}