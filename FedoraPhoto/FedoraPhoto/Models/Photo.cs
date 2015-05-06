namespace FedoraPhoto.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Seances.Photo")]
    public partial class Photo
    {
        public int PhotoID { get; set; }

        public int SeanceID { get; set; }

        [StringLength(50)]
        public string PhotoName { get; set; }

        [Required]
        [StringLength(200)]
        public string PhotoPath { get; set; }

        [Required]
        [StringLength(20)]
        public string PhotoType { get; set; }

        public virtual Seance Seance { get; set; }
    }
}
