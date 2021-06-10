﻿// <auto-generated />
using System;
using Kwetter.Services.Tweet.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Kwetter.Services.Tweet.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Kwetter.Services.Tweet.Domain.Entities.Like", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("PostTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("TweetMessageId")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("TweetMessageId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("Kwetter.Services.Tweet.Domain.Entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Kwetter.Services.Tweet.Domain.Entities.TweetMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("CreatorId")
                        .HasColumnType("text");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(160)
                        .IsUnicode(true)
                        .HasColumnType("character varying(160)");

                    b.Property<DateTime>("PostTime")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("TweetMessages");
                });

            modelBuilder.Entity("TagTweetMessage", b =>
                {
                    b.Property<int>("TagsId")
                        .HasColumnType("integer");

                    b.Property<int>("TweetsId")
                        .HasColumnType("integer");

                    b.HasKey("TagsId", "TweetsId");

                    b.HasIndex("TweetsId");

                    b.ToTable("TagTweetMessage");
                });

            modelBuilder.Entity("Kwetter.Services.Tweet.Domain.Entities.Like", b =>
                {
                    b.HasOne("Kwetter.Services.Tweet.Domain.Entities.TweetMessage", "TweetMessage")
                        .WithMany("Likes")
                        .HasForeignKey("TweetMessageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TweetMessage");
                });

            modelBuilder.Entity("TagTweetMessage", b =>
                {
                    b.HasOne("Kwetter.Services.Tweet.Domain.Entities.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kwetter.Services.Tweet.Domain.Entities.TweetMessage", null)
                        .WithMany()
                        .HasForeignKey("TweetsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Kwetter.Services.Tweet.Domain.Entities.TweetMessage", b =>
                {
                    b.Navigation("Likes");
                });
#pragma warning restore 612, 618
        }
    }
}
