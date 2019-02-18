using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lingva.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lingva.DataAccessLayer.Context
{
    public class DictionaryContext : DbContext
    {
        public DbSet<DictionaryRecord> Dictionary { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Word> Words { get; set; }

        public DictionaryContext(DbContextOptions<DictionaryContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Language>()
            //       //.HasOne(m => m.TranslationDictionary)
            //       .HasMany(t => t.TranslationDictionary)
            //       .WithOne(c => c.LanguageName)
            //       .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
