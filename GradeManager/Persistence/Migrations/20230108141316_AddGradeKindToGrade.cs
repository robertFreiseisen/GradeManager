using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistence.Migrations
{
    public partial class AddGradeKindToGrade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_GradeKeys_GradeKeyId",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_GradeKeyId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "GradeKeyId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "Kind",
                table: "Grades");

            migrationBuilder.AddColumn<int>(
                name: "GradeKeyId",
                table: "SchoolClasses",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GradeKindId",
                table: "Grades",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "GradeKeys",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GradeKinds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    GradeKeyId = table.Column<int>(type: "integer", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeKinds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GradeKinds_GradeKeys_GradeKeyId",
                        column: x => x.GradeKeyId,
                        principalTable: "GradeKeys",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SchoolClasses_GradeKeyId",
                table: "SchoolClasses",
                column: "GradeKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_GradeKindId",
                table: "Grades",
                column: "GradeKindId");

            migrationBuilder.CreateIndex(
                name: "IX_GradeKeys_SubjectId",
                table: "GradeKeys",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_GradeKinds_GradeKeyId",
                table: "GradeKinds",
                column: "GradeKeyId");

            migrationBuilder.AddForeignKey(
                name: "FK_GradeKeys_Subjects_SubjectId",
                table: "GradeKeys",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_GradeKinds_GradeKindId",
                table: "Grades",
                column: "GradeKindId",
                principalTable: "GradeKinds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolClasses_GradeKeys_GradeKeyId",
                table: "SchoolClasses",
                column: "GradeKeyId",
                principalTable: "GradeKeys",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GradeKeys_Subjects_SubjectId",
                table: "GradeKeys");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_GradeKinds_GradeKindId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolClasses_GradeKeys_GradeKeyId",
                table: "SchoolClasses");

            migrationBuilder.DropTable(
                name: "GradeKinds");

            migrationBuilder.DropIndex(
                name: "IX_SchoolClasses_GradeKeyId",
                table: "SchoolClasses");

            migrationBuilder.DropIndex(
                name: "IX_Grades_GradeKindId",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_GradeKeys_SubjectId",
                table: "GradeKeys");

            migrationBuilder.DropColumn(
                name: "GradeKeyId",
                table: "SchoolClasses");

            migrationBuilder.DropColumn(
                name: "GradeKindId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "GradeKeys");

            migrationBuilder.AddColumn<int>(
                name: "GradeKeyId",
                table: "Grades",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Kind",
                table: "Grades",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Grades_GradeKeyId",
                table: "Grades",
                column: "GradeKeyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_GradeKeys_GradeKeyId",
                table: "Grades",
                column: "GradeKeyId",
                principalTable: "GradeKeys",
                principalColumn: "Id");
        }
    }
}
