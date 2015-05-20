namespace FedoraPhoto.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Seances.Facture")]
    public partial class Facture
    {
        [Key]
        public int FactureID { get; set; }

        public int FraisDeplacement { get; set; }

        public int FraisVisiteVirtuelle { get; set; }

        public int FraisTVQ { get; set; }

        public int FraisTPS { get; set; }

        public int SeanceID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? Total { get; set; }

        public virtual Seance Seance { get; set; }
    }
}
