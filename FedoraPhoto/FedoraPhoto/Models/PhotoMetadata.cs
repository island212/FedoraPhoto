using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

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

            [Display(Name="Photo")]
            [Column("Photo")]
            [StringLength(200)]
            public string Photo1 { get; set; }

            [StringLength(50)]
            public string PhotoName { get; set; }
        }
    }
}