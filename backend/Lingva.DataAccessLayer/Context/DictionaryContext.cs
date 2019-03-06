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

            modelBuilder.Entity<Film>().HasData(
                new Film[]
                {
                    new Film{ Id=1, Name="Focus"},
                    new Film{ Id=2, Name="Game"},

                });
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Language>()
            //       //.HasOne(m => m.TranslationDictionary)
            //       .HasMany(t => t.TranslationDictionary)
            //       .WithOne(c => c.LanguageName)
            //       .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
