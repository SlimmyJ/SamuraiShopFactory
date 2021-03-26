using IntroToEF.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace IntroToEF.Data
{
    // Context is absolutely essential in EF -> MUST inherit from DBContext
    public class SamuraiContext : DbContext
    {
        // Each entity that needs a table needs to be defined here
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Horse> Horses { get; set; }
        public DbSet<Battle> Battles { get; set; }

        private const string CONNECTION = @"Server=.\SQLEXPRESS;Database=SamuraiDB;Trusted_Connection=True;";

        // Override the OnConfigure to dictate which database is being used and the type of said DB
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(CONNECTION)
                .LogTo(Console.WriteLine);
        }
    }
}