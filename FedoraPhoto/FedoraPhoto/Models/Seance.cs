namespace FedoraPhoto.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Seances.Seance")]
    public partial class Seance
    {
        public Seance()
        {
            Photos = new HashSet<Photo>();
        }

        public int SeanceID { get; set; }

        public int AgentID { get; set; }

        public int PhotographeID { get; set; }

        public string Adresse { get; set; }

        public string Telephone1 { get; set; }

        public string Telephone2 { get; set; }

        public string Telephone3 { get; set; }

        public DateTime? DateSeance { get; set; }

        public int? HeureRDV { get; set; }

        public int? MinuteRDV { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public int? ForfaitID { get; set; }

        public string Statut { get; set; }

        public DateTime? DateDispo { get; set; }

        public DateTime? DateFacture { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] rowVersionSeance { get; set; }

        public virtual Agent Agent { get; set; }

        public virtual Photographe Photographe { get; set; }

        public virtual Forfait Forfait { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
    }
}
