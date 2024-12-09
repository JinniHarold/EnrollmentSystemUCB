﻿// <auto-generated />
using System;
using EnrollmentSystemUCB.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EnrollmentSystemUCB.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241023024725_EDP No Identity")]
    partial class EDPNoIdentity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EnrollmentSystemUCB.Models.Entities.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("StudCourse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudFName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudLName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudMInitial")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudYear")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("EnrollmentSystemUCB.Models.Entities.Subject", b =>
                {
                    b.Property<string>("SubjectCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SubjectCategory")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubjectCo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubjectCourse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubjectCurrYear")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubjectDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubjectOffering")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubjectPre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubjectUnits")
                        .HasColumnType("int");

                    b.HasKey("SubjectCode");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("EnrollmentSystemUCB.Models.Entities.SubjectSchedule", b =>
                {
                    b.Property<int>("SubjectEDPCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubjectEDPCode"));

                    b.Property<string>("SUbjectDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubjectCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SubjectDays")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubjectSY")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubjectSection")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("SubjectTimeEnd")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("SubjectTimeStart")
                        .HasColumnType("time");

                    b.HasKey("SubjectEDPCode");

                    b.HasIndex("SubjectCode");

                    b.ToTable("SubjectSchedules");
                });

            modelBuilder.Entity("EnrollmentSystemUCB.Models.Entities.SubjectSchedule", b =>
                {
                    b.HasOne("EnrollmentSystemUCB.Models.Entities.Subject", "Subject")
                        .WithMany("SubjectSchedules")
                        .HasForeignKey("SubjectCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("EnrollmentSystemUCB.Models.Entities.Subject", b =>
                {
                    b.Navigation("SubjectSchedules");
                });
#pragma warning restore 612, 618
        }
    }
}
