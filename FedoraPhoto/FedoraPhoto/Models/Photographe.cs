namespace FedoraPhoto.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Photographes.Photographe")]
    public partial class Photographe
    {
        public Photographe()
        {
            Seances = new HashSet<Seance>();
        }

        public int PhotographeID { get; set; }

        [Required]
        [StringLength(50)]
        public string Nom { get; set; }

        [Required]
        [StringLength(50)]
        public string Prenom { get; set; }

        public virtual ICollection<Seance> Seances { get; set; }
    }
}
