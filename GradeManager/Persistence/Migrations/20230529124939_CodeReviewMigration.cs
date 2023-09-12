using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class CodeReviewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GradeKinds_GradeKeys_GradeKeyId",
                table: "GradeKinds");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolClasses_Teachers_TeacherId",
                table: "SchoolClasses");

            migrationBuilder.DropIndex(
                name: "IX_SchoolClasses_TeacherId",
                table: "SchoolClasses");

            migrationBuilder.DropIndex(
                name: "IX_GradeKinds_GradeKeyId",
                table: "GradeKinds");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "SchoolClasses");

            migrationBuilder.DropColumn(
                name: "GradeKeyId",
                table: "GradeKinds");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SchoolYear",
                table: "SchoolClasses",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "GradeKeys",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "GradeKeyGradeKind",
                columns: table => new
                {
                    GradeKeysId = table.Column<int>(type: "integer", nullable: false),
                    UsedKindsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeKeyGradeKind", x => new { x.GradeKeysId, x.UsedKindsId });
                    table.ForeignKey(
                        name: "FK_GradeKeyGradeKind_GradeKeys_GradeKeysId",
                        column: x => x.GradeKeysId,
                        principalTable: "GradeKeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GradeKeyGradeKind_GradeKinds_UsedKindsId",
                        column: x => x.UsedKindsId,
                        principalTable: "GradeKinds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchoolClassTeacher",
                columns: table => new
                {
                    SchoolClassesId = table.Column<int>(type: "integer", nullable: false),
                    TeachersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolClassTeacher", x => new { x.SchoolClassesId, x.TeachersId });
                    table.ForeignKey(
                        name: "FK_SchoolClassTeacher_SchoolClasses_SchoolClassesId",
                        column: x => x.SchoolClassesId,
                        principalTable: "SchoolClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchoolClassTeacher_Teachers_TeachersId",
                        column: x => x.TeachersId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GradeKeyGradeKind_UsedKindsId",
                table: "GradeKeyGradeKind",
                column: "UsedKindsId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolClassTeacher_TeachersId",
                table: "SchoolClassTeacher",
                column: "TeachersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GradeKeyGradeKind");

            migrationBuilder.DropTable(
                name: "SchoolClassTeacher");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SchoolYear",
                table: "SchoolClasses",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "SchoolClasses",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GradeKeyId",
                table: "GradeKinds",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "GradeKeys",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolClasses_TeacherId",
                table: "SchoolClasses",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_GradeKinds_GradeKeyId",
                table: "GradeKinds",
                column: "GradeKeyId");

            migrationBuilder.AddForeignKey(
                name: "FK_GradeKinds_GradeKeys_GradeKeyId",
                table: "GradeKinds",
                column: "GradeKeyId",
                principalTable: "GradeKeys",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolClasses_Teachers_TeacherId",
                table: "SchoolClasses",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");
        }
    }
}
