﻿// <auto-generated />
using System;
using Buns.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Buns.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.6");

            modelBuilder.Entity("Buns.Domain.Entities.Buns.Bun", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("BakeTime")
                        .HasColumnType("TEXT");

                    b.Property<long?>("BunTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("ControlSaleTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("StartPrice")
                        .HasColumnType("REAL");

                    b.Property<DateTimeOffset>("TimeToSell")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BunTypeId");

                    b.ToTable("Buns");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Bun");
                });

            modelBuilder.Entity("Buns.Domain.Entities.Buns.BunType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("BunTypes");
                });

            modelBuilder.Entity("Buns.Domain.Entities.Buns.PretzelBun", b =>
                {
                    b.HasBaseType("Buns.Domain.Entities.Buns.Bun");

                    b.HasDiscriminator().HasValue("PretzelBun");
                });

            modelBuilder.Entity("Buns.Domain.Entities.Buns.SourCreamBun", b =>
                {
                    b.HasBaseType("Buns.Domain.Entities.Buns.Bun");

                    b.HasDiscriminator().HasValue("SourCreamBun");
                });

            modelBuilder.Entity("Buns.Domain.Entities.Buns.Bun", b =>
                {
                    b.HasOne("Buns.Domain.Entities.Buns.BunType", "BunType")
                        .WithMany()
                        .HasForeignKey("BunTypeId");

                    b.Navigation("BunType");
                });
#pragma warning restore 612, 618
        }
    }
}
