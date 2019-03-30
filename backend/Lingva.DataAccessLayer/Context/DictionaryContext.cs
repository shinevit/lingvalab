using Lingva.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lingva.DataAccessLayer.Context
{
    public class DictionaryContext : DbContext
    {
        public DictionaryContext(DbContextOptions options)
            : base(options)
        {
            // Database.EnsureDeleted();
            // Database.EnsureCreated();
        }

        public DbSet<DictionaryRecord> Dictionary { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Subtitles> Subtitles { get; set; }
        public DbSet<SubtitleRow> SubtitleRows { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<ParserWord> ParserWords { get; set; }
        public DbSet<SimpleEnWord> SimpleEnWords { get; set; }
        public DbSet<DictionaryEnWord> DictionaryEnWords { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Film>().ToTable("Films");
            modelBuilder.Entity<Subtitles>().ToTable("Subtitles");
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

            modelBuilder.Entity<Subtitles>()
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
        }
    }
}