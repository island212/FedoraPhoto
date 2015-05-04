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
            [Required]
            public int SeanceID { get; set; }

            [Required]
            public int AgentID { get; set; }

            [Required]
            public int PhotographeID { get; set; }

            [DataType(DataType.Date)]
            public DateTime DateSeance { get; set; }

            [StringLength(200)]
            public string Adresse { get; set; }

            [Phone]
            [DataType(DataType.PhoneNumber)]
            [StringLength(20)]
            public string Telephone1 { get; set; }

            [Phone]
            [StringLength(20)]
            public string Telephone2 { get; set; }

            [Phone]
            [StringLength(20)]
            public string Telephone3 { get; set; }
        }
    }
}