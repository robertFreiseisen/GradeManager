﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Persistence;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GradeKeyGradeKind", b =>
                {
                    b.Property<int>("GradeKeysId")
                        .HasColumnType("integer");

                    b.Property<int>("UsedKindsId")
                        .HasColumnType("integer");

                    b.HasKey("GradeKeysId", "UsedKindsId");

                    b.HasIndex("UsedKindsId");

                    b.ToTable("GradeKeyGradeKind");
                });

            modelBuilder.Entity("SchoolClassTeacher", b =>
                {
                    b.Property<int>("SchoolClassesId")
                        .HasColumnType("integer");

                    b.Property<int>("TeachersId")
                        .HasColumnType("integer");

                    b.HasKey("SchoolClassesId", "TeachersId");

                    b.HasIndex("TeachersId");

                    b.ToTable("SchoolClassTeacher");
                });

            modelBuilder.Entity("Shared.Entities.Grade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("GradeKindId")
                        .HasColumnType("integer");

                    b.Property<int>("Graduate")
                        .HasColumnType("integer");

                    b.Property<string>("Note")
                        .HasColumnType("text");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.Property<int>("StudentId")
                        .HasColumnType("integer");

                    b.Property<int>("SubjectId")
                        .HasColumnType("integer");

                    b.Property<int>("TeacherId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GradeKindId");

                    b.HasIndex("StudentId");

                    b.HasIndex("SubjectId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("Shared.Entities.GradeKey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Calculation")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.Property<int>("ScriptType")
                        .HasColumnType("integer");

                    b.Property<int>("SubjectId")
                        .HasColumnType("integer");

                    b.Property<int>("TeacherId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.HasIndex("TeacherId");

                    b.ToTable("GradeKeys");
                });

            modelBuilder.Entity("Shared.Entities.GradeKind", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.ToTable("GradeKinds");
                });

            modelBuilder.Entity("Shared.Entities.SchoolClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("GradeKeyId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.Property<int>("SchoolLevel")
                        .HasColumnType("integer");

                    b.Property<DateTime>("SchoolYear")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("GradeKeyId");

                    b.ToTable("SchoolClasses");
                });

            modelBuilder.Entity("Shared.Entities.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.Property<int>("SchoolClassId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SchoolClassId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Shared.Entities.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.Property<int?>("TeacherId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("Shared.Entities.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("GradeKeyGradeKind", b =>
                {
                    b.HasOne("Shared.Entities.GradeKey", null)
                        .WithMany()
                        .HasForeignKey("GradeKeysId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shared.Entities.GradeKind", null)
                        .WithMany()
                        .HasForeignKey("UsedKindsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SchoolClassTeacher", b =>
                {
                    b.HasOne("Shared.Entities.SchoolClass", null)
                        .WithMany()
                        .HasForeignKey("SchoolClassesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shared.Entities.Teacher", null)
                        .WithMany()
                        .HasForeignKey("TeachersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Shared.Entities.Grade", b =>
                {
                    b.HasOne("Shared.Entities.GradeKind", "GradeKind")
                        .WithMany()
                        .HasForeignKey("GradeKindId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shared.Entities.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shared.Entities.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shared.Entities.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GradeKind");

                    b.Navigation("Student");

                    b.Navigation("Subject");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("Shared.Entities.GradeKey", b =>
                {
                    b.HasOne("Shared.Entities.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shared.Entities.Teacher", "Teacher")
                        .WithMany("GradeKeys")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subject");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("Shared.Entities.SchoolClass", b =>
                {
                    b.HasOne("Shared.Entities.GradeKey", null)
                        .WithMany("SchoolClasses")
                        .HasForeignKey("GradeKeyId");
                });

            modelBuilder.Entity("Shared.Entities.Student", b =>
                {
                    b.HasOne("Shared.Entities.SchoolClass", "SchoolClass")
                        .WithMany("Students")
                        .HasForeignKey("SchoolClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SchoolClass");
                });

            modelBuilder.Entity("Shared.Entities.Subject", b =>
                {
                    b.HasOne("Shared.Entities.Teacher", null)
                        .WithMany("Subjects")
                        .HasForeignKey("TeacherId");
                });

            modelBuilder.Entity("Shared.Entities.GradeKey", b =>
                {
                    b.Navigation("SchoolClasses");
                });

            modelBuilder.Entity("Shared.Entities.SchoolClass", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("Shared.Entities.Teacher", b =>
                {
                    b.Navigation("GradeKeys");

                    b.Navigation("Subjects");
                });
#pragma warning restore 612, 618
        }
    }
}
