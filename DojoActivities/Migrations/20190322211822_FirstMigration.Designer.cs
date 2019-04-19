﻿// <auto-generated />
using System;
using DojoActivities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DojoActivities.Migrations
{
    [DbContext(typeof(DojoActivityContext))]
    [Migration("20190322211822_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DojoActivities.Models.Activ", b =>
                {
                    b.Property<int>("ActivId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ActivDate");

                    b.Property<string>("ActivOne")
                        .IsRequired();

                    b.Property<string>("ActivTwo")
                        .IsRequired();

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("DuratUnit")
                        .IsRequired();

                    b.Property<int?>("Duration")
                        .IsRequired();

                    b.Property<string>("State")
                        .IsRequired();

                    b.Property<string>("Street")
                        .IsRequired();

                    b.Property<DateTime?>("Time")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<int>("UserId");

                    b.Property<string>("Zip")
                        .IsRequired();

                    b.HasKey("ActivId");

                    b.HasIndex("UserId");

                    b.ToTable("Activs");
                });

            modelBuilder.Entity("DojoActivities.Models.Join", b =>
                {
                    b.Property<int>("JoinId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActivId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<int>("UserId");

                    b.HasKey("JoinId");

                    b.HasIndex("ActivId");

                    b.HasIndex("UserId");

                    b.ToTable("Joins");
                });

            modelBuilder.Entity("DojoActivities.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DojoActivities.Models.Activ", b =>
                {
                    b.HasOne("DojoActivities.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DojoActivities.Models.Join", b =>
                {
                    b.HasOne("DojoActivities.Models.Activ", "Activ")
                        .WithMany("Joiners")
                        .HasForeignKey("ActivId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DojoActivities.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
