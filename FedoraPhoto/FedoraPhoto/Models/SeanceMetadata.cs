using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FedoraPhoto.Models
{

    [MetadataType(typeof(SeanceMetaData))]
    public partial class Seance
    {
        internal sealed class SeanceMetaData
        {
            [Display(Name="Séance")]
            [Required]
            public int SeanceID { get; set; }

            [Display(Name = "Agent")]
            [Required]
            public int AgentID { get; set; }

            [Display(Name = "Photographe")]
            [Required]
            public int PhotographeID { get; set; }

            [Display(Name = "Date de la seance")]
            [ValidateDateSeance]
            [DataType(DataType.Date)]
            public DateTime DateSeance { get; set; }

            [StringLength(200)]
            public string Adresse { get; set; }

            [Display(Name = "Téléphone 1")]
            [RegularExpression("^\\(?([0-9]{3})\\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage="Le numéro de téléphone doit être valide")]
            [DataType(DataType.PhoneNumber)]
            [StringLength(20)]
            public string Telephone1 { get; set; }

            [Display(Name = "Téléphone 2")]
            [RegularExpression("^\\(?([0-9]{3})\\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage= "Le numéro de téléphone doit être valide")]
            [StringLength(20)]
            public string Telephone2 { get; set; }

            [Display(Name = "Téléphone 3")]
            [RegularExpression("^\\(?([0-9]{3})\\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage= "Le numéro de téléphone doit être valide")]
            [StringLength(20)]
            public string Telephone3 { get; set; }

            [Display(Name = "Nom du propriétaire")]
            [StringLength(50)]
            [Required]
            public string Nom { get; set; }

            [Display(Name = "Prénom du propriétaire")]
            [StringLength(50)]
            [Required]
            public string Prenom { get; set; }
        }
    }
}