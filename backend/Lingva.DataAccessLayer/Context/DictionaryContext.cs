﻿using System;
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
        public DbSet<Group> Groups { get; set; }
        public DbSet<Event> Events { get; set; }

        public DictionaryContext(DbContextOptions<DictionaryContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
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
            modelBuilder.Entity<UserGroup>().ToTable("UserGroups");

            modelBuilder.Entity<UserGroup>()
                .HasOne<Group>(ug => ug.Group)
                .WithMany(g => g.UserGroups)
                .HasForeignKey(ug => ug.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserGroup>()
               .HasOne<User>(ug => ug.User)
               .WithMany(g => g.UserGroups)
               .HasForeignKey(ug => ug.UserId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Word>()
                  .HasOne(c => c.Language)
                  .WithMany(t => t.Words)
                  .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
