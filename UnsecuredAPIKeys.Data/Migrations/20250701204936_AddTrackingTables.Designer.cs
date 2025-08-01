﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using UnsecuredAPIKeys.Data;

#nullable disable

namespace UnsecuredAPIKeys.Data.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20250701204936_AddTrackingTables")]
    partial class AddTrackingTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("UnsecuredAPIKeys.Data.Models.APIKey", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("ApiKey")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ApiType")
                        .HasColumnType("integer");

                    b.Property<DateTime>("FirstFoundUTC")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("LastCheckedUTC")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("LastFoundUTC")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("LastValidUTC")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("SearchProvider")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("TimesDisplayed")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ApiKey")
                        .HasDatabaseName("IX_APIKeys_ApiKey");

                    b.HasIndex("LastCheckedUTC")
                        .HasDatabaseName("IX_APIKeys_LastCheckedUTC");

                    b.HasIndex("Status", "ApiType")
                        .HasDatabaseName("IX_APIKeys_Status_ApiType");

                    b.ToTable("APIKeys");
                });

            modelBuilder.Entity("UnsecuredAPIKeys.Data.Models.ApplicationSetting", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Key");

                    b.ToTable("ApplicationSettings");
                });

            modelBuilder.Entity("UnsecuredAPIKeys.Data.Models.IssueSubmissionTracking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<long>("ApiKeyId")
                        .HasColumnType("bigint");

                    b.Property<string>("ApiType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("RepoUrl")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<DateTime>("SubmittedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserIP")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("IssueSubmissionTrackings");
                });

            modelBuilder.Entity("UnsecuredAPIKeys.Data.Models.IssueVerification", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("FirstCheckedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("GitHubAvatarUrl")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("GitHubDisplayName")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<long?>("GitHubIssueNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("GitHubIssueUrl")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<long?>("GitHubUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("GitHubUsername")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime?>("IssueClosedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("IssueCreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("IssueSubmissionTrackingId")
                        .HasColumnType("bigint");

                    b.Property<int?>("IssueSubmissionTrackingId1")
                        .HasColumnType("integer");

                    b.Property<string>("IssueTitle")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<DateTime>("LastCheckedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("RepoUrl")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("SubmitterIP")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("IssueSubmissionTrackingId1");

                    b.ToTable("IssueVerifications");
                });

            modelBuilder.Entity("UnsecuredAPIKeys.Data.Models.KeyInvalidation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ApiKeyId")
                        .HasColumnType("bigint");

                    b.Property<bool>("ConfirmedFixed")
                        .HasColumnType("boolean");

                    b.Property<int>("DaysActive")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("FixedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("HttpStatusCode")
                        .HasColumnType("text");

                    b.Property<DateTime>("InvalidatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("InvalidationReason")
                        .HasColumnType("text");

                    b.Property<string>("PreviousStatus")
                        .HasColumnType("text");

                    b.Property<bool>("WasValid")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("ApiKeyId")
                        .HasDatabaseName("IX_KeyInvalidations_ApiKeyId");

                    b.HasIndex("InvalidatedAt")
                        .HasDatabaseName("IX_KeyInvalidations_InvalidatedAt");

                    b.ToTable("KeyInvalidations");
                });

            modelBuilder.Entity("UnsecuredAPIKeys.Data.Models.KeyRotation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("NewKeyId")
                        .HasColumnType("bigint");

                    b.Property<int>("OldKeyDaysActive")
                        .HasColumnType("integer");

                    b.Property<long>("OldKeyId")
                        .HasColumnType("bigint");

                    b.Property<string>("RepoUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("RotatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("NewKeyId");

                    b.HasIndex("OldKeyId");

                    b.ToTable("KeyRotations");
                });

            modelBuilder.Entity("UnsecuredAPIKeys.Data.Models.PatternEffectiveness", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("FirstSeen")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("InvalidKeys")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("MostSuccessfulFileTypes")
                        .HasColumnType("text");

                    b.Property<string>("Pattern")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProviderName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TotalMatches")
                        .HasColumnType("integer");

                    b.Property<int>("ValidKeys")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProviderName")
                        .HasDatabaseName("IX_PatternEffectiveness_Provider");

                    b.ToTable("PatternEffectiveness");
                });

            modelBuilder.Entity("UnsecuredAPIKeys.Data.Models.Proxy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("LastUsedUTC")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ProxyUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Proxies");
                });

            modelBuilder.Entity("UnsecuredAPIKeys.Data.Models.RateLimitLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Endpoint")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("IpAddress")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("character varying(45)");

                    b.Property<DateTime>("RequestTimeUtc")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("RateLimitLogs");
                });

            modelBuilder.Entity("UnsecuredAPIKeys.Data.Models.RepoReference", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("APIKeyId")
                        .HasColumnType("bigint");

                    b.Property<string>("ApiContentUrl")
                        .HasColumnType("text");

                    b.Property<string>("Branch")
                        .HasColumnType("text");

                    b.Property<string>("CodeContext")
                        .HasColumnType("text");

                    b.Property<string>("FileName")
                        .HasColumnType("text");

                    b.Property<string>("FilePath")
                        .HasColumnType("text");

                    b.Property<string>("FileSHA")
                        .HasColumnType("text");

                    b.Property<string>("FileURL")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("FoundUTC")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("LineNumber")
                        .HasColumnType("integer");

                    b.Property<string>("Provider")
                        .HasColumnType("text");

                    b.Property<string>("RepoDescription")
                        .HasColumnType("text");

                    b.Property<long>("RepoId")
                        .HasColumnType("bigint");

                    b.Property<string>("RepoName")
                        .HasColumnType("text");

                    b.Property<string>("RepoOwner")
                        .HasColumnType("text");

                    b.Property<string>("RepoURL")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("SearchQueryId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("APIKeyId")
                        .HasDatabaseName("IX_RepoReferences_ApiKeyId");

                    b.ToTable("RepoReferences");
                });

            modelBuilder.Entity("UnsecuredAPIKeys.Data.Models.SearchProviderToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastUsedUTC")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("SearchProvider")
                        .HasColumnType("integer");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("SearchProviderTokens");
                });

            modelBuilder.Entity("UnsecuredAPIKeys.Data.Models.SearchQuery", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastSearchUTC")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Query")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SearchResultsCount")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("SearchQueries");
                });

            modelBuilder.Entity("UnsecuredAPIKeys.Data.Models.SnitchLeaderboard", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("ClosedIssuesSubmitted")
                        .HasColumnType("integer");

                    b.Property<int>("ConsecutiveDaysActive")
                        .HasColumnType("integer");

                    b.Property<string>("DisplayName")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("FavoriteApiType")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<DateTime>("FirstSubmissionAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("LastSubmissionAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("OpenIssuesSubmitted")
                        .HasColumnType("integer");

                    b.Property<double>("SnitchScore")
                        .HasColumnType("double precision");

                    b.Property<int>("TotalIssuesSubmitted")
                        .HasColumnType("integer");

                    b.Property<int>("TotalRepositoriesAffected")
                        .HasColumnType("integer");

                    b.Property<string>("UserIdentifier")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("SnitchLeaderboards");
                });

            modelBuilder.Entity("UnsecuredAPIKeys.Data.Models.IssueVerification", b =>
                {
                    b.HasOne("UnsecuredAPIKeys.Data.Models.IssueSubmissionTracking", "IssueSubmissionTracking")
                        .WithMany()
                        .HasForeignKey("IssueSubmissionTrackingId1");

                    b.Navigation("IssueSubmissionTracking");
                });

            modelBuilder.Entity("UnsecuredAPIKeys.Data.Models.KeyInvalidation", b =>
                {
                    b.HasOne("UnsecuredAPIKeys.Data.Models.APIKey", "ApiKey")
                        .WithMany()
                        .HasForeignKey("ApiKeyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApiKey");
                });

            modelBuilder.Entity("UnsecuredAPIKeys.Data.Models.KeyRotation", b =>
                {
                    b.HasOne("UnsecuredAPIKeys.Data.Models.APIKey", "NewKey")
                        .WithMany()
                        .HasForeignKey("NewKeyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("UnsecuredAPIKeys.Data.Models.APIKey", "OldKey")
                        .WithMany()
                        .HasForeignKey("OldKeyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("NewKey");

                    b.Navigation("OldKey");
                });

            modelBuilder.Entity("UnsecuredAPIKeys.Data.Models.RepoReference", b =>
                {
                    b.HasOne("UnsecuredAPIKeys.Data.Models.APIKey", null)
                        .WithMany("References")
                        .HasForeignKey("APIKeyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UnsecuredAPIKeys.Data.Models.APIKey", b =>
                {
                    b.Navigation("References");
                });
#pragma warning restore 612, 618
        }
    }
}
