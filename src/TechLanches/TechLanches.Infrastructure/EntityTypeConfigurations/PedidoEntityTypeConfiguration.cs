﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Entities;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Infrastructure.EntityTypeConfigurations
{
    //public class PedidoEntityTypeConfiguration : IEntityTypeConfiguration<Pedido>
    //{
    //    public void Configure(EntityTypeBuilder<Pedido> builder)
    //    {
    //        builder.ToTable("Pedidos");

    //        builder.HasKey(x => x.Id);
    //        builder.Property(x => x.Id).ValueGeneratedOnAdd();

    //        builder.Property(x => x.Valor)
    //               .HasColumnName("Valor")
    //               .IsRequired();

    //        builder.Property<int>("StatusPedidoId")
    //               .IsRequired();

    //        builder.Property(x => x.ClienteId)
    //               .HasColumnName("ClienteId")
    //               .IsRequired();

    //        builder.Ignore(x => x.DomainEvents);

    //        var navigation = builder.Metadata.FindNavigation(nameof(Pedido.ItensPedido));
    //        navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);
    //    }
    //}
}