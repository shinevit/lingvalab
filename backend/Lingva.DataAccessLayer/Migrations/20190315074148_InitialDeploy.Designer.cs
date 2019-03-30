﻿// <auto-generated />
using System;
using Lingva.DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Lingva.DataAccessLayer.Migrations
{
    [DbContext(typeof(DictionaryContext))]
    [Migration("20190315074148_InitialDeploy")]
    partial class InitialDeploy
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Lingva.DataAccessLayer.Entities.DictionaryRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Context")
                        .HasMaxLength(200);

                    b.Property<string>("LanguageName")
                        .IsRequired()
                        .HasMaxLength(3);

                    b.Property<string>("Picture");

                    b.Property<string>("Translation")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int>("UserId");

                    b.Property<string>("WordName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("LanguageName");

                    b.HasIndex("UserId");

                    b.HasIndex("WordName");

                    b.ToTable("Dictionary");
                });

            modelBuilder.Entity("Lingva.DataAccessLayer.Entities.Film", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("film");
                });

            modelBuilder.Entity("Lingva.DataAccessLayer.Entities.Language", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(3);

                    b.HasKey("Name");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("Lingva.DataAccessLayer.Entities.Subtitles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FilmId")
                        .HasColumnName("film_id");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("FilmId");

                    b.ToTable("subtitles");
                });

            modelBuilder.Entity("Lingva.DataAccessLayer.Entities.SubtitlesRow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnName("end_time");

                    b.Property<int>("LineNumber")
                        .HasColumnName("line_number");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnName("start_time");

                    b.Property<int>("SubtitlesId")
                        .HasColumnName("subtitles_id");

                    b.Property<string>("Value")
                        .HasColumnName("value");

                    b.HasKey("Id");

                    b.HasIndex("SubtitlesId");

                    b.ToTable("subtitles_row");
                });

            modelBuilder.Entity("Lingva.DataAccessLayer.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName");

                    b.Property<byte[]>("PasswordHash")
                        .HasMaxLength(16);

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Lingva.DataAccessLayer.Entities.Word", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100);

                    b.Property<string>("LanguageName")
                        .IsRequired()
                        .HasMaxLength(3);

                    b.HasKey("Name");

                    b.HasIndex("LanguageName");

                    b.ToTable("Words");
                });

            modelBuilder.Entity("Lingva.DataAccessLayer.Entities.DictionaryRecord", b =>
                {
                    b.HasOne("Lingva.DataAccessLayer.Entities.Language", "Language")
                        .WithMany("UserDictionaryRecords")
                        .HasForeignKey("LanguageName")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Lingva.DataAccessLayer.Entities.User", "User")
                        .WithMany("UserDictionaryRecords")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Lingva.DataAccessLayer.Entities.Word", "Word")
                        .WithMany("UserDictionaryRecords")
                        .HasForeignKey("WordName")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Lingva.DataAccessLayer.Entities.Subtitles", b =>
                {
                    b.HasOne("Lingva.DataAccessLayer.Entities.Film", "Film")
                        .WithMany("Subtitles")
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Lingva.DataAccessLayer.Entities.SubtitlesRow", b =>
                {
                    b.HasOne("Lingva.DataAccessLayer.Entities.Subtitles", "Subtitles")
                        .WithMany("SubtitlesRow")
                        .HasForeignKey("SubtitlesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Lingva.DataAccessLayer.Entities.Word", b =>
                {
                    b.HasOne("Lingva.DataAccessLayer.Entities.Language", "Language")
                        .WithMany("Words")
                        .HasForeignKey("LanguageName")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
