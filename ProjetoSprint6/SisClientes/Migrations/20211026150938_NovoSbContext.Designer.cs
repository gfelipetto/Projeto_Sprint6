﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SisClientes.Data;

namespace SisClientes.Migrations
{
    [DbContext(typeof(SisClientesDbContext))]
    [Migration("20211026150938_NovoSbContext")]
    partial class NovoSbContext
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SisClientes.Models.Cidades", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Estado");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Nome");

                    b.HasKey("Id");

                    b.ToTable("Cidades");
                });

            modelBuilder.Entity("SisClientes.Models.Clientes", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<Guid>("CidadeId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CidadeId");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataNascimento");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Nome");

                    b.Property<Guid?>("ResidenteDeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ResidenteDeId");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("SisClientes.Models.Enderecos", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Bairro");

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Cep");

                    b.Property<Guid>("ClienteId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ClienteId");

                    b.Property<Guid?>("ClientesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Complemento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Complemento");

                    b.Property<bool>("EhPrincipal")
                        .HasColumnType("bit")
                        .HasColumnName("EhPrincipal");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Logradouro");

                    b.Property<string>("NumeroCasa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("NumeroCasa");

                    b.HasKey("Id");

                    b.HasIndex("ClientesId");

                    b.ToTable("Endereco");
                });

            modelBuilder.Entity("SisClientes.Models.Clientes", b =>
                {
                    b.HasOne("SisClientes.Models.Cidades", "ResidenteDe")
                        .WithMany("ClientesQueHabitam")
                        .HasForeignKey("ResidenteDeId");

                    b.Navigation("ResidenteDe");
                });

            modelBuilder.Entity("SisClientes.Models.Enderecos", b =>
                {
                    b.HasOne("SisClientes.Models.Clientes", null)
                        .WithMany("Enderecos")
                        .HasForeignKey("ClientesId");
                });

            modelBuilder.Entity("SisClientes.Models.Cidades", b =>
                {
                    b.Navigation("ClientesQueHabitam");
                });

            modelBuilder.Entity("SisClientes.Models.Clientes", b =>
                {
                    b.Navigation("Enderecos");
                });
#pragma warning restore 612, 618
        }
    }
}
