﻿// <auto-generated />
using System;
using Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Api.Data.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20240523030004_UserMigration")]
    partial class UserMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Api.Domain.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("User", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("a2afb00e-6d1d-405f-9745-03e7267040f9"),
                            CreateAt = new DateTime(2024, 5, 23, 0, 0, 3, 795, DateTimeKind.Local).AddTicks(3162),
                            Email = "dev.caiosampaio@outlook.com",
                            Name = "Administrador",
                            UpdateAt = new DateTime(2024, 5, 23, 0, 0, 3, 795, DateTimeKind.Local).AddTicks(3182)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
