﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OcsDewor.Models;

#nullable disable

namespace OcsDewor.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.2.24128.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("OcsDewor.Models.Application", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<bool>("IsUnSubmitted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("PlanApplication")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<int?>("TypeActivityId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("OcsDewor.Models.TypeActivity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TypeActivities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Доклад, 35-45 минут",
                            Name = "Report"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Мастеркласс, 1-2 часа",
                            Name = "Masterclass"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Дискуссия / круглый стол, 40-50 минут",
                            Name = "Discussion"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
