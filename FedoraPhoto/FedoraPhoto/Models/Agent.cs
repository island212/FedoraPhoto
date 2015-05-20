namespace FedoraPhoto.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Agents.Agent")]
    public partial class Agent
    {
        public Agent()
        {
            Seances = new HashSet<Seance>();
        }

        public int AgentID { get; set; }

        [Required]
        [StringLength(50)]
        public string Nom { get; set; }

        [Required]
        [StringLength(50)]
        public string Prenom { get; set; }

        [Required]
        [StringLength(20)]
        public string Telephone { get; set; }

        [StringLength(50)]
        public string Courriel { get; set; }

        public virtual ICollection<Seance> Seances { get; set; }
    }
}