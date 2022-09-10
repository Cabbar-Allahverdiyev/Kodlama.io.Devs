using Core.Security.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public DbSet<ProgramingLanguage> ProgramingLanguages { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserSocialMediaAddress> UserSocialMediaAddresses { get; set; }

        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //    if (!optionsBuilder.IsConfigured)
            //        base.OnConfiguring(
            //            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("KodlamaIODevsConnectionString")));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProgramingLanguage>(a =>
            {
                a.ToTable("ProgramingLanguages").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");

                a.HasMany(p => p.Technologies);
            });

            modelBuilder.Entity<Technology>(a =>
            {
                a.ToTable("Technologies").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.ProgramingLanguageId).HasColumnName("ProgramingLanguageId");
                a.Property(p => p.Name).HasColumnName("Name");

                a.HasOne(p => p.ProgramingLanguage);
            });

            modelBuilder.Entity<User>(a =>
            {
                a.ToTable("Users").HasKey(k => k.Id);
                a.Property(u => u.Id).HasColumnName("Id");
                a.Property(u => u.FirstName).HasColumnName("FirstName");
                a.Property(u => u.LastName).HasColumnName("LastName");
                a.Property(u => u.Email).HasColumnName("Email");
                a.Property(u => u.PasswordSalt).HasColumnName("PasswordSalt");
                a.Property(u => u.PasswordHash).HasColumnName("PasswordHash");
                a.Property(u => u.Status).HasColumnName("Status");

                a.HasMany(u => u.UserOperationClaims);
                // a.HasMany(u => u.RefreshTokens);
            });

            modelBuilder.Entity<OperationClaim>(a =>
            {
                a.ToTable("OperationClaims").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");


                //  a.HasMany(p => p.);
            });

            modelBuilder.Entity<UserOperationClaim>(a =>
            {
                a.ToTable("UserOperationClaims").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.UserId).HasColumnName("UserId");
                a.Property(p => p.OperationClaimId).HasColumnName("OperationClaimId");


                a.HasOne(p => p.OperationClaim);
                a.HasOne(p => p.User);
            });

            modelBuilder.Entity<UserSocialMediaAddress>(a =>
            {
                a.ToTable("UserSocialMediaAddresses").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.UserId).HasColumnName("UserId");
                a.Property(p => p.GithubUrl).HasColumnName("GithubUrl");

                a.HasOne(p => p.User);
            });

            ProgramingLanguage[] programingLanguageEntitySeeds = {new (1,"C#"),
                                                                  new (2,"Java") };
            modelBuilder.Entity<ProgramingLanguage>().HasData(programingLanguageEntitySeeds);

            Technology[] technologyEntitySeeds = { new(1, 2, "Spring"),
                                                   new(2,2,"JSP"),
                                                   new(3,1,"ASP.NET")};
            modelBuilder.Entity<Technology>().HasData(technologyEntitySeeds);
        }
    }
}
