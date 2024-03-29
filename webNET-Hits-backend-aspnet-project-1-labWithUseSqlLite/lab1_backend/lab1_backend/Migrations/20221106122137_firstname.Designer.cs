﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using lab1_backend.Models;

#nullable disable

namespace lab1_backend.Migrations
{
    [DbContext(typeof(MovieDbContext))]
    [Migration("20221106122137_firstname")]
    partial class firstname
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.10");

            modelBuilder.Entity("GenresMovie", b =>
                {
                    b.Property<string>("Genresid")
                        .HasColumnType("TEXT");

                    b.Property<string>("MoviesId")
                        .HasColumnType("TEXT");

                    b.HasKey("Genresid", "MoviesId");

                    b.HasIndex("MoviesId");

                    b.ToTable("GenresMovie");
                });

            modelBuilder.Entity("lab1_backend.Models.FavoriteMovies", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("MovieId")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("FavoriteMovies");
                });

            modelBuilder.Entity("lab1_backend.Models.Genres", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("TEXT");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("lab1_backend.Models.Movie", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ReviewsId")
                        .HasColumnType("TEXT");

                    b.Property<int>("ageLimit")
                        .HasColumnType("INTEGER");

                    b.Property<int>("budget")
                        .HasColumnType("INTEGER");

                    b.Property<string>("country")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("director")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("fees")
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("poster")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("tagline")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("time")
                        .HasColumnType("INTEGER");

                    b.Property<int>("year")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ReviewsId");

                    b.ToTable("Movie");
                });

            modelBuilder.Entity("lab1_backend.Models.ReviewModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Authorid")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAnonymous")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Rating")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ReviewText")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("createDateTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Authorid");

                    b.ToTable("ReviewModel");
                });

            modelBuilder.Entity("lab1_backend.Models.UserShortModel", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("UserShortModel");
                });

            modelBuilder.Entity("GenresMovie", b =>
                {
                    b.HasOne("lab1_backend.Models.Genres", null)
                        .WithMany()
                        .HasForeignKey("Genresid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("lab1_backend.Models.Movie", null)
                        .WithMany()
                        .HasForeignKey("MoviesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("lab1_backend.Models.Movie", b =>
                {
                    b.HasOne("lab1_backend.Models.ReviewModel", "Reviews")
                        .WithMany()
                        .HasForeignKey("ReviewsId");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("lab1_backend.Models.ReviewModel", b =>
                {
                    b.HasOne("lab1_backend.Models.UserShortModel", "Author")
                        .WithMany()
                        .HasForeignKey("Authorid");

                    b.Navigation("Author");
                });
#pragma warning restore 612, 618
        }
    }
}
