using System;
using System.Collections.Generic;
using System.Text;

using Lingva.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lingva.DataAccessLayer.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<EventMembers> EventMembers { get; set; }

        public ApplicationContext()
            : base()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Lingvaappdb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<EventMembers>().HasData(
                new EventMembers[]
                {
                    new EventMembers{ ID = 111, UserID=1, EventID = 1001},
                    new EventMembers{ ID = 112, UserID=2, EventID = 1002},

                });
            base.OnModelCreating(modelBuilder);

        }
    }
}
