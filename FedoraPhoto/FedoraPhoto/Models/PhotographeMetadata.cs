using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FedoraPhoto.Models
{
    [MetadataType(typeof(PhotographeMetadata))]
    public partial class Photographe
    {
        internal sealed class PhotographeMetadata
        {
            [Display(Name = "Nom Photographe")]
            public string Nom { get; set; }

            [Display(Name = "Prénom Photographe")]
            public string Prenom { get; set; }
        }
    }
}