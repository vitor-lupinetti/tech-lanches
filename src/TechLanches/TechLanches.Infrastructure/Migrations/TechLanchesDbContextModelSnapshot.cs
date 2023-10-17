﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TechLanches.Infrastructure;

#nullable disable

namespace TechLanches.Infrastructure.Migrations
{
    [DbContext(typeof(TechLanchesDbContext))]
    partial class TechLanchesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TechLanches.Domain.Aggregates.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Produtos", (string)null);
                });

            modelBuilder.Entity("TechLanches.Domain.Entities.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Clientes", (string)null);
                });

            modelBuilder.Entity("TechLanches.Domain.Entities.Cliente", b =>
                {
                    b.OwnsOne("TechLanches.Domain.ValueObjects.Cpf", "CPF", b1 =>
                        {
                            b1.Property<int>("ClienteId")
                                .HasColumnType("int");

                            b1.Property<string>("Numero")
                                .IsRequired()
                                .HasMaxLength(11)
                                .HasColumnType("nvarchar(11)")
                                .HasColumnName("Cpf");

                            b1.HasKey("ClienteId");

                            b1.HasIndex("Numero")
                                .IsUnique();

                            b1.ToTable("Clientes");

                            b1.WithOwner()
                                .HasForeignKey("ClienteId");
                        });

                    b.OwnsOne("TechLanches.Domain.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<int>("ClienteId")
                                .HasColumnType("int");

                            b1.Property<string>("EnderecoEmail")
                                .IsRequired()
                                .HasColumnType("nvarchar(450)")
                                .HasColumnName("Email");

                            b1.HasKey("ClienteId");

                            b1.HasIndex("EnderecoEmail");

                            b1.ToTable("Clientes");

                            b1.WithOwner()
                                .HasForeignKey("ClienteId");
                        });

                    b.Navigation("CPF")
                        .IsRequired();

                    b.Navigation("Email")
                        .IsRequired();
                });

            modelBuilder.Entity("TechLanches.Domain.Aggregates.Produto", b =>
                {
                    b.OwnsOne("TechLanches.Domain.ValueObjects.CategoriaProduto", "Categoria", b1 =>
                        {
                            b1.Property<int>("ProdutoId")
                                .HasColumnType("int");

                            b1.Property<int>("Id")
                                .HasColumnType("int")
                                .HasColumnName("Categoria_Id");

                            b1.HasKey("ProdutoId");

                            b1.ToTable("Produtos");

                            b1.WithOwner()
                                .HasForeignKey("ProdutoId");
                        });

                    b.Navigation("Categoria")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
