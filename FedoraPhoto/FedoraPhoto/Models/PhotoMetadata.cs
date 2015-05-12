using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FedoraPhoto.Models
{
    [MetadataType(typeof(PhotoMetadata))]
    public partial class Photo
    {
        internal sealed class PhotoMetadata
        {
            [Required]
            public int PhotoID { get; set; }

            [Required]
            public int SeanceID { get; set; }

            //[StringLength(50)]
            //public string PhotoName { get; set; }

            //[Required]
            //[StringLength(200)]
            //public string PhotoPath { get; set; }

            //[Required]
            //[StringLength(20)]
            //public string PhotoType { get; set; }
        }
    }
}