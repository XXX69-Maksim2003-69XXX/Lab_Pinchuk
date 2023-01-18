using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PinchuckLab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinchuckLab
{
    public class MailContext: DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<MailBranch> MailBranches { get; set; } 
        public DbSet<Parcel> Parcels { get; set; }
        
        
        public MailContext() {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsetings.json")
           .Build();

            optionsBuilder.UseSqlServer(configuration
                .GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Використання підходу TPT для наслідування
            modelBuilder.Entity<Client>().ToTable("Clients");
            modelBuilder.Entity<Employee>().ToTable("Employees");
            
            // встановлюю поле Passport у класі Client як унікальне
            modelBuilder.Entity<Client>()
                .HasAlternateKey(x => x.Passport);

            // Не обов'язкове з'єднання таблиці Client та таблиці Payment через поле Passport
            modelBuilder.Entity<Client>()
                .HasOne(x => x.Payment)
                .WithOne(x => x.Client)
                .HasForeignKey<Payment>(x => x.Passport)
                .HasPrincipalKey<Client>(x => x.Passport);
            
            
            modelBuilder.Entity<Employee>()
                .Property(x => x.Position).HasMaxLength(50);
            modelBuilder.Entity<Employee>()
                .Property(x => x.PhoneNumber).HasMaxLength(50);
            

            modelBuilder.Entity<MailBranch>()
                .HasKey(x => x.BranchId);

            modelBuilder.Entity<MailBranch>()
                .HasData(new MailBranch[]
                {
                    new MailBranch
                    {
                        BranchId = 1, BranchName = "Main Branch", City="Antwerpen", Address = "Main Street 1", PhoneNumber = "123456789"
                    },
                    new MailBranch
                    {
                        BranchId = 2, BranchName = "Second Branch", City="Paris", Address = "Second Street 2", PhoneNumber = "12345_6789"
                    },
                    new MailBranch
                    {
                        BranchId = 3, BranchName = "Third Branch", City="London", Address = "Third Street 3", PhoneNumber = "12345678_9"
                    },
                });

        }
    }
}
