﻿// <auto-generated />
using System;
using FactorioProductionCells.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FactorioProductionCells.Infrastructure.Migrations
{
    [DbContext(typeof(FactorioProductionCellsDbContext))]
    [Migration("20200619215118_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:PostgresExtension:uuid-ossp", ",,")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("FactorioProductionCells.Domain.Entities.Dependency", b =>
                {
                    b.Property<Guid>("ReleaseId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("DependentModId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AddedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("DependencyComparisonTypeId")
                        .HasColumnType("integer");

                    b.Property<int>("DependencyTypeId")
                        .HasColumnType("integer");

                    b.Property<string>("DependentModName")
                        .IsRequired()
                        .HasColumnType("character varying(200)")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uuid");

                    b.HasKey("ReleaseId", "DependentModId");

                    b.HasIndex("AddedBy");

                    b.HasIndex("DependencyComparisonTypeId");

                    b.HasIndex("DependencyTypeId");

                    b.HasIndex("DependentModId");

                    b.HasIndex("LastModifiedBy");

                    b.ToTable("Dependencies");
                });

            modelBuilder.Entity("FactorioProductionCells.Domain.Entities.DependencyComparisonType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying(25)")
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.ToTable("DependencyComparisonTypes");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Name = "LessThan"
                        },
                        new
                        {
                            Id = 1,
                            Name = "LessThanOrEqualTo"
                        },
                        new
                        {
                            Id = 2,
                            Name = "EqualTo"
                        },
                        new
                        {
                            Id = 3,
                            Name = "GreaterThan"
                        },
                        new
                        {
                            Id = 4,
                            Name = "GreaterThanOrEqualTo"
                        });
                });

            modelBuilder.Entity("FactorioProductionCells.Domain.Entities.DependencyType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("DependencyTypes");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Name = "Required"
                        },
                        new
                        {
                            Id = 1,
                            Name = "Incompatibility"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Optional"
                        },
                        new
                        {
                            Id = 3,
                            Name = "HiddenOptional"
                        });
                });

            modelBuilder.Entity("FactorioProductionCells.Domain.Entities.Language", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<string>("EnglishName")
                        .IsRequired()
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<bool>("IsDefault")
                        .HasColumnType("boolean");

                    b.Property<string>("LanguageTag")
                        .IsRequired()
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("IsDefault")
                        .IsUnique()
                        .HasFilter("\"IsDefault\" = true");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("FactorioProductionCells.Domain.Entities.Mod", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<Guid>("AddedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying(200)")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("AddedBy");

                    b.HasIndex("LastModifiedBy");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Mods");
                });

            modelBuilder.Entity("FactorioProductionCells.Domain.Entities.ModTitle", b =>
                {
                    b.Property<Guid>("ModId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("LanguageId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AddedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("character varying(200)")
                        .HasMaxLength(200);

                    b.HasKey("ModId", "LanguageId");

                    b.HasIndex("AddedBy");

                    b.HasIndex("LanguageId");

                    b.HasIndex("LastModifiedBy");

                    b.ToTable("ModTitles");
                });

            modelBuilder.Entity("FactorioProductionCells.Domain.Entities.Release", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<Guid>("AddedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ModId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ReleasedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Sha1")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("AddedBy");

                    b.HasIndex("LastModifiedBy");

                    b.HasIndex("ModId");

                    b.ToTable("Releases");
                });

            modelBuilder.Entity("FactorioProductionCells.Infrastructure.Identity.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<Guid>("PreferredLanguageId")
                        .HasColumnType("uuid");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.HasIndex("PreferredLanguageId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("FactorioProductionCells.Domain.Entities.Dependency", b =>
                {
                    b.HasOne("FactorioProductionCells.Infrastructure.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("AddedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FactorioProductionCells.Domain.Entities.DependencyComparisonType", "DependencyComparisonType")
                        .WithMany()
                        .HasForeignKey("DependencyComparisonTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FactorioProductionCells.Domain.Entities.DependencyType", "DependencyType")
                        .WithMany()
                        .HasForeignKey("DependencyTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FactorioProductionCells.Domain.Entities.Mod", "DependentMod")
                        .WithMany()
                        .HasForeignKey("DependentModId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FactorioProductionCells.Infrastructure.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("LastModifiedBy");

                    b.HasOne("FactorioProductionCells.Domain.Entities.Release", "Release")
                        .WithMany("Dependencies")
                        .HasForeignKey("ReleaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("FactorioProductionCells.Domain.ValueObjects.ModVersion", "DependentModVersion", b1 =>
                        {
                            b1.Property<Guid>("DependencyReleaseId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("DependencyDependentModId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Major")
                                .HasColumnType("integer");

                            b1.Property<int>("Minor")
                                .HasColumnType("integer");

                            b1.Property<int>("Patch")
                                .HasColumnType("integer");

                            b1.HasKey("DependencyReleaseId", "DependencyDependentModId");

                            b1.ToTable("Dependencies");

                            b1.WithOwner()
                                .HasForeignKey("DependencyReleaseId", "DependencyDependentModId");
                        });
                });

            modelBuilder.Entity("FactorioProductionCells.Domain.Entities.Mod", b =>
                {
                    b.HasOne("FactorioProductionCells.Infrastructure.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("AddedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FactorioProductionCells.Infrastructure.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("LastModifiedBy");
                });

            modelBuilder.Entity("FactorioProductionCells.Domain.Entities.ModTitle", b =>
                {
                    b.HasOne("FactorioProductionCells.Infrastructure.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("AddedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FactorioProductionCells.Domain.Entities.Language", "Language")
                        .WithMany("ModTitles")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FactorioProductionCells.Infrastructure.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("LastModifiedBy");

                    b.HasOne("FactorioProductionCells.Domain.Entities.Mod", "Mod")
                        .WithMany("Titles")
                        .HasForeignKey("ModId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FactorioProductionCells.Domain.Entities.Release", b =>
                {
                    b.HasOne("FactorioProductionCells.Infrastructure.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("AddedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FactorioProductionCells.Infrastructure.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("LastModifiedBy");

                    b.HasOne("FactorioProductionCells.Domain.Entities.Mod", "Mod")
                        .WithMany("Releases")
                        .HasForeignKey("ModId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("FactorioProductionCells.Domain.ValueObjects.FactorioVersion", "FactorioVersion", b1 =>
                        {
                            b1.Property<Guid>("ReleaseId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uuid");

                            b1.Property<int>("Major")
                                .HasColumnType("integer");

                            b1.Property<int>("Minor")
                                .HasColumnType("integer");

                            b1.HasKey("ReleaseId");

                            b1.ToTable("Releases");

                            b1.WithOwner()
                                .HasForeignKey("ReleaseId");
                        });

                    b.OwnsOne("FactorioProductionCells.Domain.ValueObjects.ModVersion", "Version", b1 =>
                        {
                            b1.Property<Guid>("ReleaseId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uuid");

                            b1.Property<int>("Major")
                                .HasColumnType("integer");

                            b1.Property<int>("Minor")
                                .HasColumnType("integer");

                            b1.Property<int>("Patch")
                                .HasColumnType("integer");

                            b1.HasKey("ReleaseId");

                            b1.ToTable("Releases");

                            b1.WithOwner()
                                .HasForeignKey("ReleaseId");
                        });

                    b.OwnsOne("FactorioProductionCells.Domain.ValueObjects.ReleaseDownloadUrl", "DownloadUrl", b1 =>
                        {
                            b1.Property<Guid>("ReleaseId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uuid");

                            b1.Property<string>("ModName")
                                .HasColumnType("character varying(200)")
                                .HasMaxLength(200);

                            b1.Property<string>("ReleaseToken")
                                .HasColumnType("character varying(24)")
                                .HasMaxLength(24);

                            b1.HasKey("ReleaseId");

                            b1.ToTable("Releases");

                            b1.WithOwner()
                                .HasForeignKey("ReleaseId");
                        });

                    b.OwnsOne("FactorioProductionCells.Domain.ValueObjects.ReleaseFileName", "ReleaseFileName", b1 =>
                        {
                            b1.Property<Guid>("ReleaseId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uuid");

                            b1.Property<string>("ModName")
                                .HasColumnType("character varying(200)")
                                .HasMaxLength(200);

                            b1.HasKey("ReleaseId");

                            b1.ToTable("Releases");

                            b1.WithOwner()
                                .HasForeignKey("ReleaseId");

                            b1.OwnsOne("FactorioProductionCells.Domain.ValueObjects.ModVersion", "Version", b2 =>
                                {
                                    b2.Property<Guid>("ReleaseFileNameReleaseId")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Major")
                                        .HasColumnType("integer");

                                    b2.Property<int>("Minor")
                                        .HasColumnType("integer");

                                    b2.Property<int>("Patch")
                                        .HasColumnType("integer");

                                    b2.HasKey("ReleaseFileNameReleaseId");

                                    b2.ToTable("Releases");

                                    b2.WithOwner()
                                        .HasForeignKey("ReleaseFileNameReleaseId");
                                });
                        });
                });

            modelBuilder.Entity("FactorioProductionCells.Infrastructure.Identity.User", b =>
                {
                    b.HasOne("FactorioProductionCells.Domain.Entities.Language", "PreferredLanguage")
                        .WithMany()
                        .HasForeignKey("PreferredLanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("FactorioProductionCells.Infrastructure.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("FactorioProductionCells.Infrastructure.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("FactorioProductionCells.Infrastructure.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
