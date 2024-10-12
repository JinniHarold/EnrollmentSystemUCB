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
    [Migration("20241012131754_second partial migration")]
    partial class secondpartialmigration
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

                    b.Property<int>("SubjectCourse")
                        .HasColumnType("int");

                    b.Property<string>("SubjectCurrYear")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubjectDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubjectOffering")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubjectUnits")
                        .HasColumnType("int");

                    b.HasKey("SubjectCode");

                    b.ToTable("Subjects");
                });
#pragma warning restore 612, 618
        }
    }
}
