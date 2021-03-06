﻿// <auto-generated />
using System;
using CoolBytes.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CoolBytes.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20181014080905_AddedAuthorIdToAuthorProfile")]
    partial class AddedAuthorIdToAuthorProfile
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CoolBytes.Core.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("CoolBytes.Core.Models.AuthorProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("About")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<int>("AuthorId");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("ImageId");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("ResumeUri")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("AuthorId")
                        .IsUnique();

                    b.HasIndex("ImageId");

                    b.ToTable("AuthorsProfile");
                });

            modelBuilder.Entity("CoolBytes.Core.Models.BlogPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuthorId");

                    b.Property<DateTime>("Date");

                    b.Property<int?>("ImageId");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ImageId");

                    b.ToTable("BlogPosts");
                });

            modelBuilder.Entity("CoolBytes.Core.Models.BlogPostTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BlogPostId")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("BlogPostId");

                    b.ToTable("BlogPostTags");
                });

            modelBuilder.Entity("CoolBytes.Core.Models.Experience", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AuthorProfileId")
                        .IsRequired();

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("CHAR(6)");

                    b.Property<int>("ImageId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("AuthorProfileId");

                    b.HasIndex("ImageId");

                    b.ToTable("Experiences");
                });

            modelBuilder.Entity("CoolBytes.Core.Models.ExternalLink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BlogPostId")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("BlogPostId");

                    b.ToTable("ExternalLinks");
                });

            modelBuilder.Entity("CoolBytes.Core.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<long>("Length");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("UriPath")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("CoolBytes.Core.Models.ResumeEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuthorId");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("ResumeEvents");
                });

            modelBuilder.Entity("CoolBytes.Core.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Identifier")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CoolBytes.Data.Models.MailProvider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DailyThreshold");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("MailProviders");
                });

            modelBuilder.Entity("CoolBytes.Data.Models.MailStat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<int>("MailproviderId");

                    b.Property<int>("Sent");

                    b.HasKey("Id");

                    b.HasIndex("MailproviderId");

                    b.ToTable("MailStats");
                });

            modelBuilder.Entity("CoolBytes.Core.Models.Author", b =>
                {
                    b.HasOne("CoolBytes.Core.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CoolBytes.Core.Models.AuthorProfile", b =>
                {
                    b.HasOne("CoolBytes.Core.Models.Author", "Author")
                        .WithOne("AuthorProfile")
                        .HasForeignKey("CoolBytes.Core.Models.AuthorProfile", "AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CoolBytes.Core.Models.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId");

                    b.OwnsOne("CoolBytes.Core.Models.SocialHandles", "SocialHandles", b1 =>
                        {
                            b1.Property<int?>("AuthorProfileId")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("GitHub")
                                .HasMaxLength(255);

                            b1.Property<string>("LinkedIn")
                                .HasMaxLength(255);

                            b1.ToTable("AuthorsProfile");

                            b1.HasOne("CoolBytes.Core.Models.AuthorProfile")
                                .WithOne("SocialHandles")
                                .HasForeignKey("CoolBytes.Core.Models.SocialHandles", "AuthorProfileId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("CoolBytes.Core.Models.BlogPost", b =>
                {
                    b.HasOne("CoolBytes.Core.Models.Author", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CoolBytes.Core.Models.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId");

                    b.OwnsOne("CoolBytes.Core.Models.BlogPostContent", "Content", b1 =>
                        {
                            b1.Property<int?>("BlogPostId")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Content")
                                .IsRequired()
                                .HasMaxLength(8000);

                            b1.Property<string>("ContentIntro")
                                .IsRequired()
                                .HasMaxLength(120);

                            b1.Property<string>("Subject")
                                .IsRequired()
                                .HasMaxLength(100);

                            b1.Property<string>("SubjectUrl")
                                .IsRequired()
                                .HasMaxLength(100);

                            b1.Property<DateTime?>("Updated");

                            b1.ToTable("BlogPosts");

                            b1.HasOne("CoolBytes.Core.Models.BlogPost")
                                .WithOne("Content")
                                .HasForeignKey("CoolBytes.Core.Models.BlogPostContent", "BlogPostId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("CoolBytes.Core.Models.BlogPostTag", b =>
                {
                    b.HasOne("CoolBytes.Core.Models.BlogPost", "BlogPost")
                        .WithMany("Tags")
                        .HasForeignKey("BlogPostId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CoolBytes.Core.Models.Experience", b =>
                {
                    b.HasOne("CoolBytes.Core.Models.AuthorProfile", "AuthorProfile")
                        .WithMany("Experiences")
                        .HasForeignKey("AuthorProfileId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CoolBytes.Core.Models.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CoolBytes.Core.Models.ExternalLink", b =>
                {
                    b.HasOne("CoolBytes.Core.Models.BlogPost", "BlogPost")
                        .WithMany("ExternalLinks")
                        .HasForeignKey("BlogPostId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CoolBytes.Core.Models.ResumeEvent", b =>
                {
                    b.HasOne("CoolBytes.Core.Models.Author", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("CoolBytes.Core.Models.DateRange", "DateRange", b1 =>
                        {
                            b1.Property<int?>("ResumeEventId")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<DateTime>("EndDate");

                            b1.Property<DateTime>("StartDate");

                            b1.ToTable("ResumeEvents");

                            b1.HasOne("CoolBytes.Core.Models.ResumeEvent")
                                .WithOne("DateRange")
                                .HasForeignKey("CoolBytes.Core.Models.DateRange", "ResumeEventId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("CoolBytes.Data.Models.MailStat", b =>
                {
                    b.HasOne("CoolBytes.Data.Models.MailProvider", "MailProvider")
                        .WithMany()
                        .HasForeignKey("MailproviderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
