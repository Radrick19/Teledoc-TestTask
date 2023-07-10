using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teledok.Domain.Infrastructure.Enums;
using Teledok.Domain.Models;
using Teledok.Domain.Models.Base;
using Teledok.Domain.Models.Clients;

namespace Teledok.Domain
{
    public class TeledokContext : DbContext
    {
        public DbSet<IndividualPerson> IndividualPersons { get; set; }
        public DbSet<LegalEntity> LegalEntities { get; set; }
        public DbSet<Incorporator> Incorporators { get; set; }
        public DbSet<LegalEntityIncorporator> LegalEntitiesIncorporators { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LegalEntityIncorporator>()
                .HasKey(li => new { li.IncorporatorId, li.LegalEntityId });

            modelBuilder.Entity<LegalEntityIncorporator>()
                .HasOne(li => li.LegalEntity)
                .WithMany(le => le.Incorporators)
                .HasForeignKey(li => li.LegalEntityId);

            modelBuilder.Entity<LegalEntityIncorporator>()
                .HasOne(li => li.Incorporator)
                .WithMany(inc => inc.LegalEntities)
                .HasForeignKey(li => li.IncorporatorId);

            modelBuilder.Entity<IndividualPerson>()
                .Property(ip => ip.ClientType)
                .HasConversion(new EnumToStringConverter<ClientType>());


            modelBuilder.Entity<LegalEntity>()
                .Property(ip => ip.ClientType)
                .HasConversion(new EnumToStringConverter<ClientType>());
        }

        public TeledokContext(DbContextOptions<TeledokContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public TeledokContext()
        {
            Database.EnsureCreated();
        }
    }
}
