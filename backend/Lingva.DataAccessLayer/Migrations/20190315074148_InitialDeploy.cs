using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lingva.DataAccessLayer.Migrations
{
    public partial class InitialDeploy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "film",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_film", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Name = table.Column<string>(maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(maxLength: 16, nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "subtitles",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true),
                    film_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subtitles", x => x.id);
                    table.ForeignKey(
                        name: "FK_subtitles_film_film_id",
                        column: x => x.film_id,
                        principalTable: "film",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    LanguageName = table.Column<string>(maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Words_Languages_LanguageName",
                        column: x => x.LanguageName,
                        principalTable: "Languages",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subtitles_row",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    subtitles_id = table.Column<int>(nullable: false),
                    line_number = table.Column<int>(nullable: false),
                    value = table.Column<string>(nullable: true),
                    start_time = table.Column<TimeSpan>(nullable: false),
                    end_time = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subtitles_row", x => x.id);
                    table.ForeignKey(
                        name: "FK_subtitles_row_subtitles_subtitles_id",
                        column: x => x.subtitles_id,
                        principalTable: "subtitles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dictionary",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    WordName = table.Column<string>(nullable: false),
                    Translation = table.Column<string>(maxLength: 200, nullable: false),
                    LanguageName = table.Column<string>(maxLength: 3, nullable: false),
                    Context = table.Column<string>(maxLength: 200, nullable: true),
                    Picture = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dictionary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dictionary_Languages_LanguageName",
                        column: x => x.LanguageName,
                        principalTable: "Languages",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dictionary_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dictionary_Words_WordName",
                        column: x => x.WordName,
                        principalTable: "Words",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dictionary_LanguageName",
                table: "Dictionary",
                column: "LanguageName");

            migrationBuilder.CreateIndex(
                name: "IX_Dictionary_UserId",
                table: "Dictionary",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Dictionary_WordName",
                table: "Dictionary",
                column: "WordName");

            migrationBuilder.CreateIndex(
                name: "IX_subtitles_film_id",
                table: "subtitles",
                column: "film_id");

            migrationBuilder.CreateIndex(
                name: "IX_subtitles_row_subtitles_id",
                table: "subtitles_row",
                column: "subtitles_id");

            migrationBuilder.CreateIndex(
                name: "IX_Words_LanguageName",
                table: "Words",
                column: "LanguageName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dictionary");

            migrationBuilder.DropTable(
                name: "subtitles_row");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Words");

            migrationBuilder.DropTable(
                name: "subtitles");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "film");
        }
    }
}
