﻿// <auto-generated />
using System;
using JelloBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JelloBackend.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("JelloBackend.Models.Board", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("JelloBackend.Models.Column", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<int?>("Boardid")
                        .HasColumnType("integer");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("Boardid");

                    b.ToTable("Columns");
                });

            modelBuilder.Entity("JelloBackend.Models.Element", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<int?>("Columnid")
                        .HasColumnType("integer");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("Columnid");

                    b.ToTable("Elements");
                });

            modelBuilder.Entity("JelloBackend.Models.Column", b =>
                {
                    b.HasOne("JelloBackend.Models.Board", null)
                        .WithMany("columns")
                        .HasForeignKey("Boardid");
                });

            modelBuilder.Entity("JelloBackend.Models.Element", b =>
                {
                    b.HasOne("JelloBackend.Models.Column", null)
                        .WithMany("elements")
                        .HasForeignKey("Columnid");
                });

            modelBuilder.Entity("JelloBackend.Models.Board", b =>
                {
                    b.Navigation("columns");
                });

            modelBuilder.Entity("JelloBackend.Models.Column", b =>
                {
                    b.Navigation("elements");
                });
#pragma warning restore 612, 618
        }
    }
}
