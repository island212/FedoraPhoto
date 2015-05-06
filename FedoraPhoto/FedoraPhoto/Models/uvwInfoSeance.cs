namespace FedoraPhoto.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Seances.uvwInfoSeances")]
    public partial class uvwInfoSeance
    {
        [StringLength(50)]
        public string PhotoName { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string PhotoType { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(200)]
        public string PhotoPath { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string Nom { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string Prenom { get; set; }

        [StringLength(50)]
        public string Courriel { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(50)]
        public string NomForfait { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(200)]
        public string DescriptionForfait { get; set; }

        [Key]
        [Column(Order = 6)]
        public decimal PrixForfait { get; set; }

        [StringLength(200)]
        public string Adresse { get; set; }
    }
}
