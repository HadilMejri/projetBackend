using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Models
{
    public class ApplicationDbContext : DbContext
    {
      //  private readonly IConfiguration _configuration;

        public ApplicationDbContext (DbContextOptions options) : base(options)
        {
           // _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Utilisez la chaîne de connexion à partir de la configuration
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Libelle_marchandise>()
                .HasKey(lm => new { lm.designation, lm.marqueCode }); // Define composite primary key
            
            modelBuilder.Entity<Prestation>()
                .HasKey(p => new { p.numFacture, p.num }); // Define composite primary key

            modelBuilder.Entity<Destination_Navire>().HasNoKey();

            
            modelBuilder.Entity<Navire>()
                .HasKey(n => new { n.nomNavire });

            modelBuilder.Entity<Marque>()
                .HasKey(m => new { m.marqueCode });
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Destination_Navire>()
            .HasOne(d => d.Navire)
            .WithMany()
            .HasForeignKey(d => d.nomNavire);

            // Utilisez la propriété NomNavire de Destination_Navire au lieu de nom_navire
            modelBuilder.Entity<Facture>()
            .HasOne(f => f.Navire)
            .WithMany(n => n.Factures)
            .HasForeignKey(f => f.nomNavire)
            .IsRequired(false);

            modelBuilder.Entity<Destination_Navire>()
            .Property(dn => dn.nomNavire)
            .HasColumnType("nvarchar(450)");

        }
        

        // DbSet properties...
        public DbSet<Client> Client { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Dossier> Dossier { get; set; }
        public DbSet<Facture> Facture { get; set; }
        public DbSet<Libelle_marchandise> Libelle_marchandise { get; set; }
        public DbSet<Marque> Marque { get; set; }
        public DbSet<Navire> Navire { get; set; }
        public DbSet<Destination_Navire> Destination_Navire { get; set; }
        public DbSet<Prestation> Prestation { get; set; }
        public DbSet<Tache> Tache { get; set; }
        public DbSet<Utilisateur> Utilisateur { get; set; }
        public DbSet<FactureDetails> FactureDetails { get; set; }

    }
}
