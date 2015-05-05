namespace FedoraPhoto.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model15")
        {
        }

        public virtual DbSet<Agent> Agents { get; set; }
        public virtual DbSet<Photographe> Photographes { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Seance> Seances { get; set; }

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

            modelBuilder.Entity<Photo>()
                .Property(e => e.Photo1)
                .IsUnicode(false);

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
                .HasOptional(e => e.Photo)
                .WithRequired(e => e.Seance);
        }
    }
}
