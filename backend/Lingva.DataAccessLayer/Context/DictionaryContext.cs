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
        public DbSet<Subtitle> Subtitles { get; set; }
        public DbSet<SubtitleRow> SubtitleRows { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<ParserWord> ParserWords { get; set; }
        public DbSet<SimpleEnWord> SimpleEnWords { get; set; }
        public DbSet<DictionaryEnWord> DictionaryEnWords { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Event> Events { get; set; }

        public DictionaryContext(DbContextOptions<DictionaryContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Film>().ToTable("Films");
            modelBuilder.Entity<Subtitle>().ToTable("Subtitles");
            modelBuilder.Entity<SubtitleRow>().ToTable("SubtitleRows");
            modelBuilder.Entity<ParserWord>().ToTable("ParserWords");
            modelBuilder.Entity<Word>().ToTable("Words");
            modelBuilder.Entity<DictionaryEnWord>().ToTable("DictionaryEnWords");
            modelBuilder.Entity<DictionaryRecord>().ToTable("DictionaryRecords");
            modelBuilder.Entity<SimpleEnWord>().ToTable("SimpleEnWords");
            modelBuilder.Entity<Event>().ToTable("Events");
            modelBuilder.Entity<Genre>().ToTable("Genres");
            modelBuilder.Entity<Group>().ToTable("Groups");
            modelBuilder.Entity<Language>().ToTable("Languages");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");

            modelBuilder.Entity<Film>()
                .HasMany(s => s.Subtitles)
                .WithOne(f => f.Film)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Film>()
                .HasOne(s => s.Language)
                .WithMany(f => f.Films)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Subtitle>()
                  .HasMany(c => c.SubtitlesRow)
                  .WithOne(t => t.Subtitles)
                  .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SubtitleRow>()
                  .HasOne(c => c.Subtitles)
                  .WithMany(t => t.SubtitlesRow)
                  .OnDelete(DeleteBehavior.Restrict);
           
            modelBuilder.Entity<Word>()
                  .HasOne(c => c.Language)
                  .WithMany(t => t.Words)
                  .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Language>().HasData(
            //      new Language() { Name = "en" },
            //      new Language() { Name = "sp" },
            //      new Language() { Name = "fr" });

            //modelBuilder.Entity<Film>().HasData(
            //     new Film() { Id = 1, Name = "The Game Of Thrones", LanguageName = "en" },
            //     new Film() { Id = 2, Name = "Friends", LanguageName = "en" },
            //     new Film() { Id = 3, Name = "Sherlock", LanguageName = "en" });

            //modelBuilder.Entity<Subtitle>().HasData(
            //                new Subtitle() { Id = 1, Path = @"C:\MySubtitles\Sherlock.rtf", LanguageName = "en" },
            //                new Subtitle() { Id = 2, Path = @"D:\Dods\Friends.rtf", LanguageName = "en" },
            //                new Subtitle() { Id = 3, Path = @"E:\Films\Game.rtf", LanguageName = "en" });

            //modelBuilder.Entity<SubtitleRow>().HasData(
            //                new SubtitleRow() { Id = 1, Value = @"Hello, my friend!", LanguageName = "en" },
            //                new SubtitleRow() { Id = 2, Value = @"Much time has passed since.", LanguageName = "en" },
            //                new SubtitleRow() { Id = 3, Value = @"I will have to leave anyway.", LanguageName = "en" });

            //modelBuilder.Entity<ParserWord>().HasData(
            //                new ParserWord() { Name = "cold", LanguageName = "en" },
            //                new ParserWord() { Name = "hot", LanguageName = "en" },
            //                new ParserWord() { Name = "light", LanguageName = "en" },
            //                new ParserWord() { Name = "happy", LanguageName = "en" });

            base.OnModelCreating(modelBuilder);
        }
    }
}
