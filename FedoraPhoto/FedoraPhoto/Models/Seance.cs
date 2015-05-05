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
        public int SeanceID { get; set; }

        public int AgentID { get; set; }

        public int PhotographeID { get; set; }

        [StringLength(200)]
        public string Adresse { get; set; }

        [StringLength(20)]
        public string Telephone1 { get; set; }

        [StringLength(20)]
        public string Telephone2 { get; set; }

        [StringLength(20)]
        public string Telephone3 { get; set; }

        public DateTime? DateSeance { get; set; }

        public int? HeureRDV { get; set; }

        public int? MinuteRDV { get; set; }

        public virtual Agent Agent { get; set; }

        public virtual Photographe Photographe { get; set; }

        public virtual Photo Photo { get; set; }
    }
}
