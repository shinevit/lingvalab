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
        public DbSet<Film> Films { get; set; }
        public DbSet<Subtitles> Subtitles { get; set; }
        public DbSet<SubtitlesRow> SubtitlesRows { get; set; }

        public DictionaryContext(DbContextOptions<DictionaryContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DictionaryRecord>()
                   .HasOne(c => c.Language)
                   .WithMany(t => t.UserDictionaryRecords)                  
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
