﻿// <auto-generated />
using System;
using Gradebook.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Gradebook.Infrastructure.Migrations
{
    [DbContext(typeof(GradebookDbContext))]
    partial class GradebookDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("gradebook")
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Gradebook.Domain.Entities.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2(0)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("StreetNumber")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2(0)");

                    b.HasKey("Id");

                    b.HasIndex("StudentId")
                        .IsUnique()
                        .HasFilter("[StudentId] IS NOT NULL");

                    b.ToTable("Addresses", "gradebook");
                });

            modelBuilder.Entity("Gradebook.Domain.Entities.Grade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2(0)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2(0)");

                    b.Property<decimal>("Value")
                        .HasPrecision(2, 1)
                        .HasColumnType("decimal(2,1)");

                    b.HasKey("Id");

                    b.HasIndex("Name", "Value")
                        .IsUnique();

                    b.ToTable("Grades", "gradebook");
                });

            modelBuilder.Entity("Gradebook.Domain.Entities.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2(0)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("GradeId")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2(0)");

                    b.Property<int>("YearEnrolled")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("GradeId");

                    b.ToTable("Students", "gradebook");
                });

            modelBuilder.Entity("Gradebook.Domain.Entities.Address", b =>
                {
                    b.HasOne("Gradebook.Domain.Entities.Student", "Student")
                        .WithOne("Address")
                        .HasForeignKey("Gradebook.Domain.Entities.Address", "StudentId");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Gradebook.Domain.Entities.Student", b =>
                {
                    b.HasOne("Gradebook.Domain.Entities.Grade", "Grade")
                        .WithMany("Students")
                        .HasForeignKey("GradeId");

                    b.Navigation("Grade");
                });

            modelBuilder.Entity("Gradebook.Domain.Entities.Grade", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("Gradebook.Domain.Entities.Student", b =>
                {
                    b.Navigation("Address");
                });
#pragma warning restore 612, 618
        }
    }
}
