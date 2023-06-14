﻿// <auto-generated />
using System;
using Education_Software.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Education_Software.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230614094555_DateTimeAdded")]
    partial class DateTimeAdded
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Education_Software.Models.GradesModel", b =>
                {
                    b.Property<string>("username")
                        .HasMaxLength(6)
                        .HasColumnType("character varying(6)");

                    b.Property<string>("sub_id")
                        .HasMaxLength(10)
                        .HasColumnType("VARCHAR");

                    b.Property<int?>("grade")
                        .HasColumnType("integer");

                    b.HasKey("username", "sub_id");

                    b.HasIndex("sub_id");

                    b.ToTable("grades");
                });

            modelBuilder.Entity("Education_Software.Models.ProgressModel", b =>
                {
                    b.Property<string>("username")
                        .HasMaxLength(6)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("test_id")
                        .HasMaxLength(20)
                        .HasColumnType("VARCHAR");

                    b.Property<decimal>("score")
                        .HasColumnType("NUMERIC");

                    b.Property<string>("sub_id")
                        .HasMaxLength(10)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("test_type")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("CHAR");

                    b.Property<string>("time")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("username", "test_id");

                    b.ToTable("progress");
                });

            modelBuilder.Entity("Education_Software.Models.QuestionModel", b =>
                {
                    b.Property<string>("q_id")
                        .HasMaxLength(40)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("answer")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("chapter")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("possible_answers")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("q_type")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("question")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("sub_id")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("VARCHAR");

                    b.HasKey("q_id");

                    b.ToTable("questions");
                });

            modelBuilder.Entity("Education_Software.Models.QuestionnaireModel", b =>
                {
                    b.Property<string>("q_id")
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("possible_answers")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("question")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("q_id");

                    b.ToTable("questionnaire");
                });

            modelBuilder.Entity("Education_Software.Models.RecommendationModel", b =>
                {
                    b.Property<string>("username")
                        .HasMaxLength(6)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("recommendation")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("username");

                    b.ToTable("recommendations");
                });

            modelBuilder.Entity("Education_Software.Models.StatisticsModel", b =>
                {
                    b.Property<string>("username")
                        .HasMaxLength(6)
                        .HasColumnType("VARCHAR");

                    b.Property<decimal>("completion_score")
                        .HasColumnType("NUMERIC");

                    b.Property<decimal>("description_score")
                        .HasColumnType("NUMERIC");

                    b.Property<decimal>("learning_outcomes_score")
                        .HasColumnType("NUMERIC");

                    b.Property<decimal>("matching_score")
                        .HasColumnType("NUMERIC");

                    b.Property<decimal>("multiple_choice_score")
                        .HasColumnType("NUMERIC");

                    b.Property<decimal>("skills_acquired_score")
                        .HasColumnType("NUMERIC");

                    b.Property<decimal>("specialization_link_score")
                        .HasColumnType("NUMERIC");

                    b.Property<decimal>("true_false_score")
                        .HasColumnType("NUMERIC");

                    b.HasKey("username");

                    b.ToTable("statistics");
                });

            modelBuilder.Entity("Education_Software.Models.SubjectModel", b =>
                {
                    b.Property<string>("sub_id")
                        .HasMaxLength(10)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("learning_outcomes")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("reading")
                        .HasColumnType("INT");

                    b.Property<decimal>("semester")
                        .HasColumnType("NUMERIC");

                    b.Property<string>("skills_acquired")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("specialization_link")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("sub_type")
                        .IsRequired()
                        .HasColumnType("CHAR");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("sub_id");

                    b.ToTable("subjects");
                });

            modelBuilder.Entity("Education_Software.Models.TestModel", b =>
                {
                    b.Property<string>("username")
                        .HasMaxLength(6)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("test_id")
                        .HasMaxLength(20)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("q_id")
                        .HasMaxLength(40)
                        .HasColumnType("VARCHAR");

                    b.Property<bool>("score")
                        .HasColumnType("BOOLEAN");

                    b.Property<string>("test_type")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("CHAR");

                    b.HasKey("username", "test_id", "q_id");

                    b.ToTable("tests");
                });

            modelBuilder.Entity("Education_Software.Models.UserModel", b =>
                {
                    b.Property<string>("username")
                        .HasColumnType("text");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("first_name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("last_name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("pass")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("phone_number")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("student_state")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("character varying(1)");

                    b.HasKey("username");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Education_Software.Models.GradesModel", b =>
                {
                    b.HasOne("Education_Software.Models.SubjectModel", null)
                        .WithMany("Grades")
                        .HasForeignKey("sub_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Education_Software.Models.UserModel", null)
                        .WithMany("Grades")
                        .HasForeignKey("username")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Education_Software.Models.SubjectModel", b =>
                {
                    b.Navigation("Grades");
                });

            modelBuilder.Entity("Education_Software.Models.UserModel", b =>
                {
                    b.Navigation("Grades");
                });
#pragma warning restore 612, 618
        }
    }
}
