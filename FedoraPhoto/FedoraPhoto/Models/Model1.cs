namespace FedoraPhoto.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model18")
        {
        }

        public virtual DbSet<Agent> Agents { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Photographe> Photographes { get; set; }
        public virtual DbSet<Forfait> Forfaits { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Seance> Seances { get; set; }
        public virtual DbSet<uvwInfoSeance> uvwInfoSeances { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agent>()
                .Property(e => e.Nom)
                .IsUnicode(false);

            modelBuilder.Entity<Agent>()
                .Property(e => e.Prenom)
                .IsUnicode(false);

            modelBuilder.Entity<Agent>()
                .Property(e => e.Telephone)
                .IsUnicode(false);

            modelBuilder.Entity<Agent>()
                .Property(e => e.Courriel)
                .IsUnicode(false);

            modelBuilder.Entity<Agent>()
                .HasMany(e => e.Seances)
                .WithRequired(e => e.Agent)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Photographe>()
                .Property(e => e.Nom)
                .IsUnicode(false);

            modelBuilder.Entity<Photographe>()
                .Property(e => e.Prenom)
                .IsUnicode(false);

            modelBuilder.Entity<Photographe>()
                .HasMany(e => e.Seances)
                .WithRequired(e => e.Photographe)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Forfait>()
                .Property(e => e.NomForfait)
                .IsUnicode(false);

            modelBuilder.Entity<Forfait>()
                .Property(e => e.PrixForfait)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Photo>()
                .Property(e => e.PhotoName)
                .IsUnicode(false);

            modelBuilder.Entity<Seance>()
                .Property(e => e.Adresse)
                .IsUnicode(false);

            modelBuilder.Entity<Seance>()
                .Property(e => e.Telephone1)
                .IsUnicode(false);

            modelBuilder.Entity<Seance>()
                .Property(e => e.Telephone2)
                .IsUnicode(false);

            modelBuilder.Entity<Seance>()
                .Property(e => e.Telephone3)
                .IsUnicode(false);

            modelBuilder.Entity<Seance>()
                .Property(e => e.Nom)
                .IsUnicode(false);

            modelBuilder.Entity<Seance>()
                .Property(e => e.Prenom)
                .IsUnicode(false);

            modelBuilder.Entity<Seance>()
                .HasOptional(e => e.Photo)
                .WithRequired(e => e.Seance);

            modelBuilder.Entity<uvwInfoSeance>()
                .Property(e => e.PhotoName)
                .IsUnicode(false);

            modelBuilder.Entity<uvwInfoSeance>()
                .Property(e => e.Nom)
                .IsUnicode(false);

            modelBuilder.Entity<uvwInfoSeance>()
                .Property(e => e.Prenom)
                .IsUnicode(false);

            modelBuilder.Entity<uvwInfoSeance>()
                .Property(e => e.Courriel)
                .IsUnicode(false);

            modelBuilder.Entity<uvwInfoSeance>()
                .Property(e => e.NomForfait)
                .IsUnicode(false);

            modelBuilder.Entity<uvwInfoSeance>()
                .Property(e => e.PrixForfait)
                .HasPrecision(10, 2);

            modelBuilder.Entity<uvwInfoSeance>()
                .Property(e => e.Adresse)
                .IsUnicode(false);
        }
    }
}
