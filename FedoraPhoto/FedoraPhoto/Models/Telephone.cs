namespace FedoraPhoto.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Seances.Telephone")]
    public partial class Telephone
    {
        public int TelephoneID { get; set; }

        public int SeanceID { get; set; }

        [StringLength(20)]
        public string NumTel { get; set; }

        public virtual Seance Seance { get; set; }
    }
}
