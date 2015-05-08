namespace FedoraPhoto.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Seances.Forfait")]
    public partial class Forfait
    {
        public Forfait()
        {
            Seances = new HashSet<Seance>();
        }

        public int ForfaitID { get; set; }

        [Required]
        [StringLength(50)]
        public string NomForfait { get; set; }

        [Required]
        [StringLength(200)]
        public string DescriptionForfait { get; set; }

        public decimal PrixForfait { get; set; }

        public int NbPhotos { get; set; }

        public int Temps { get; set; }

        public virtual ICollection<Seance> Seances { get; set; }
    }
}
