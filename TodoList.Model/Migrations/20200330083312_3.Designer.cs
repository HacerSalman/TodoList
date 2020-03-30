﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TodoList.Model.Context;

namespace TodoList.Model.Migrations
{
    [DbContext(typeof(TodoListDBContext))]
    [Migration("20200330083312_3")]
    partial class _3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TodoList.Model.Entities.List", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint");

                    b.Property<long>("CreatedDate")
                        .HasColumnName("created_date")
                        .HasColumnType("bigint");

                    b.Property<long?>("DeletedDate")
                        .HasColumnName("deleted_date")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4")
                        .HasMaxLength(5000);

                    b.Property<long?>("EndsAt")
                        .HasColumnName("ends_at")
                        .HasColumnType("bigint");

                    b.Property<string>("ModifierBy")
                        .HasColumnName("modifier_by")
                        .HasColumnType("varchar(80) CHARACTER SET utf8mb4")
                        .HasMaxLength(80);

                    b.Property<string>("OwnerBy")
                        .HasColumnName("owner_by")
                        .HasColumnType("varchar(80) CHARACTER SET utf8mb4")
                        .HasMaxLength(80);

                    b.Property<byte?>("Priority")
                        .HasColumnName("priority")
                        .HasColumnType("tinyint unsigned");

                    b.Property<long>("StartsAt")
                        .HasColumnName("starts_at")
                        .HasColumnType("bigint");

                    b.Property<byte?>("Status")
                        .HasColumnName("status")
                        .HasColumnType("tinyint unsigned");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("title")
                        .HasColumnType("varchar(200) CHARACTER SET utf8mb4")
                        .HasMaxLength(200);

                    b.Property<int>("Type")
                        .HasColumnName("type")
                        .HasColumnType("int");

                    b.Property<long>("UpdatedDate")
                        .HasColumnName("updated_date")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CreatedDate");

                    b.HasIndex("Type");

                    b.HasIndex("UpdatedDate");

                    b.ToTable("list");
                });

            modelBuilder.Entity("TodoList.Model.Entities.ListType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int");

                    b.Property<long>("CreatedDate")
                        .HasColumnName("created_date")
                        .HasColumnType("bigint");

                    b.Property<long?>("DeletedDate")
                        .HasColumnName("deleted_date")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("varchar(500) CHARACTER SET utf8mb4")
                        .HasMaxLength(500);

                    b.Property<string>("ModifierBy")
                        .HasColumnName("modifier_by")
                        .HasColumnType("varchar(80) CHARACTER SET utf8mb4")
                        .HasMaxLength(80);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(200) CHARACTER SET utf8mb4")
                        .HasMaxLength(200);

                    b.Property<string>("OwnerBy")
                        .HasColumnName("owner_by")
                        .HasColumnType("varchar(80) CHARACTER SET utf8mb4")
                        .HasMaxLength(80);

                    b.Property<byte?>("Status")
                        .HasColumnName("status")
                        .HasColumnType("tinyint unsigned");

                    b.Property<long>("UpdatedDate")
                        .HasColumnName("updated_date")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CreatedDate");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("UpdatedDate");

                    b.ToTable("list_type");
                });

            modelBuilder.Entity("TodoList.Model.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint");

                    b.Property<long>("CreatedDate")
                        .HasColumnName("created_date")
                        .HasColumnType("bigint");

                    b.Property<long?>("DeletedDate")
                        .HasColumnName("deleted_date")
                        .HasColumnType("bigint");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnName("full_name")
                        .HasColumnType("varchar(80) CHARACTER SET utf8mb4")
                        .HasMaxLength(80);

                    b.Property<string>("ModifierBy")
                        .HasColumnName("modifier_by")
                        .HasColumnType("varchar(80) CHARACTER SET utf8mb4")
                        .HasMaxLength(80);

                    b.Property<string>("OwnerBy")
                        .HasColumnName("owner_by")
                        .HasColumnType("varchar(80) CHARACTER SET utf8mb4")
                        .HasMaxLength(80);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("password")
                        .HasColumnType("varchar(80) CHARACTER SET utf8mb4")
                        .HasMaxLength(80);

                    b.Property<byte?>("Status")
                        .HasColumnName("status")
                        .HasColumnType("tinyint unsigned");

                    b.Property<long>("UpdatedDate")
                        .HasColumnName("updated_date")
                        .HasColumnType("bigint");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnName("user_name")
                        .HasColumnType("varchar(80) CHARACTER SET utf8mb4")
                        .HasMaxLength(80);

                    b.HasKey("Id");

                    b.HasIndex("CreatedDate");

                    b.HasIndex("UpdatedDate");

                    b.ToTable("user");
                });

            modelBuilder.Entity("TodoList.Model.Entities.List", b =>
                {
                    b.HasOne("TodoList.Model.Entities.ListType", "ListType")
                        .WithMany()
                        .HasForeignKey("Type")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
