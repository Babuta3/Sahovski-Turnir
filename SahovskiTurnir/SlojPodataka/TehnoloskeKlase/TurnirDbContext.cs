using Microsoft.EntityFrameworkCore;
using SlojPodataka.KlasePodataka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.TehnoloskeKlase
{
    public class TurnirDbContext : DbContext
    {
        public TurnirDbContext(DbContextOptions<TurnirDbContext> opcije) : base(opcije) { }

        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Igrac> Igraci { get; set; }
        public DbSet<Turnir> Turniri { get; set; }
        public DbSet<PlasmanIgraca> PlasmaniIgraca { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlasmanIgraca>()    
                .HasOne(p => p.Turnir)
                .WithMany(t => t.Plasmani)
                .HasForeignKey(p => p.TurnirID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PlasmanIgraca>()
                .HasOne(p => p.Igrac)
                .WithMany(i => i.Plasmani)
                .HasForeignKey(p => p.IgracID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Turnir>()
                .Property(t => t.NagradniFond)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<PlasmanIgraca>()
                .Property(p => p.Bodovi)
                .HasColumnType("decimal(4,1)");

            modelBuilder.Entity<PlasmanIgraca>()
                .Property(p => p.Nagrada)
                .HasColumnType("decimal(10,2)");
        }
    }
}
