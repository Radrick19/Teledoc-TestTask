using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Teledoc.Domain.Enums;
using Teledoc.Domain.Models;

namespace Teledoc.Database
{
    public class TeledocContext : DbContext
    {
        public DbSet<Client> Client { get; set; }
        public DbSet<Founder> Founders { get; set; }
        public DbSet<ClientFounder> ClientFounders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientFounder>()
                .HasKey(li => new { li.FounderId, li.ClientId });

            modelBuilder.Entity<ClientFounder>()
                .HasOne(li => li.Client)
                .WithMany(le => le.Founders)
                .HasForeignKey(li => li.ClientId) ;

            modelBuilder.Entity<ClientFounder>()
                .HasOne(li => li.Founder)
                .WithMany(inc => inc.Clients)
                .HasForeignKey(li => li.FounderId);

            modelBuilder.Entity<Client>()
                .Property(ip => ip.ClientType)
                .HasMaxLength(16)
                .HasConversion(new EnumToStringConverter<ClientType>());

            modelBuilder.Entity<Client>()
                .Property(client => client.Inn)
                .HasMaxLength(12);

            modelBuilder.Entity<Founder>()
                .Property(founder=> founder.Inn)
                .HasMaxLength(12);

        }

        public TeledocContext(DbContextOptions<TeledocContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        public TeledocContext()
        {
            Database.Migrate();
        }
    }
}
