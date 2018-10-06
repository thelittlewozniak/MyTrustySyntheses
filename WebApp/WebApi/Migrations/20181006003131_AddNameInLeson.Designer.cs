﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi.Models;

namespace WebApi.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20181006003131_AddNameInLeson")]
    partial class AddNameInLeson
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SharedCode.Categorie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Intitule");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("SharedCode.File", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Body");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int?>("CreatorId");

                    b.Property<int?>("LessonId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("LessonId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("SharedCode.Grade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<int?>("CreatorId");

                    b.Property<int?>("FileId");

                    b.Property<int>("TrustLvl");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("FileId");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("SharedCode.Lesson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategorieId");

                    b.Property<string>("Name");

                    b.Property<string>("School");

                    b.Property<string>("Year");

                    b.HasKey("Id");

                    b.HasIndex("CategorieId");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("SharedCode.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AccessToken");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Email");

                    b.Property<int>("Experience");

                    b.Property<string>("Firstname");

                    b.Property<DateTime>("LastLogin");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<bool>("StuPro");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SharedCode.File", b =>
                {
                    b.HasOne("SharedCode.User", "Creator")
                        .WithMany("Files")
                        .HasForeignKey("CreatorId");

                    b.HasOne("SharedCode.Lesson", "Lesson")
                        .WithMany()
                        .HasForeignKey("LessonId");
                });

            modelBuilder.Entity("SharedCode.Grade", b =>
                {
                    b.HasOne("SharedCode.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");

                    b.HasOne("SharedCode.File", "File")
                        .WithMany("Grades")
                        .HasForeignKey("FileId");
                });

            modelBuilder.Entity("SharedCode.Lesson", b =>
                {
                    b.HasOne("SharedCode.Categorie", "Categorie")
                        .WithMany()
                        .HasForeignKey("CategorieId");
                });

            modelBuilder.Entity("SharedCode.User", b =>
                {
                    b.HasOne("SharedCode.User")
                        .WithMany("Trusters")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
