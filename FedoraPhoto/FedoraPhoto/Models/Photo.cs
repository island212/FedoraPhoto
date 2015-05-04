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

        [Column("Photo")]
        [StringLength(200)]
        public string Photo1 { get; set; }

        [StringLength(50)]
        public string PhotoName { get; set; }

        public virtual Seance Seance { get; set; }
    }
}
