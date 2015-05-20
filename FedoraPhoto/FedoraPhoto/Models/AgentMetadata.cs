using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace FedoraPhoto.Models
{

    [MetadataType(typeof(AgentMetadata))]
    public partial class Agent
    {
        internal sealed class AgentMetadata
        {

            [Display(Name = "Nom Agent")]
            public string Nom { get; set; }

            [Display(Name = "Prénom Agent")]
            public string Prenom { get; set; }

            [Display(Name = "Téléphone Agent")]
            public string Telephone { get; set; }
        }
    }
}