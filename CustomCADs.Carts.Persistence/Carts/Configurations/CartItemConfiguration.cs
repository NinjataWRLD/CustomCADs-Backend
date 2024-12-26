using CustomCADs.Carts.Domain.Carts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Carts.Persistence.Carts.Configurations;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
        => builder
            .SetPrimaryKey()
            .SetForeignKeys()
            .SetStronglyTypedIds()
            .SetValidations();
}

public static class CartItemItemConfigUtils
{
    public static EntityTypeBuilder<CartItem> SetPrimaryKey(this EntityTypeBuilder<CartItem> builder)
    {
        builder.HasKey(x => x.Id);

        return builder;
    }

    public static EntityTypeBuilder<CartItem> SetForeignKeys(this EntityTypeBuilder<CartItem> builder)
    {
        builder
            .HasOne(x => x.Cart)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.CartId)
            .OnDelete(DeleteBehavior.Cascade);

        return builder;
    }

    public static EntityTypeBuilder<CartItem> SetStronglyTypedIds(this EntityTypeBuilder<CartItem> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasConversion(
                x => x.Value,
                v => new(v)
            );

        builder.Property(x => x.CartId)
            .HasConversion(
                x => x.Value,
                v => new(v)
            );

        builder.Property(x => x.ProductId)
            .HasConversion(
                x => x.Value,
                v => new(v)
            );

        builder.Property(x => x.CadId)
            .HasConversion<Guid?>(
                x => x == null ? null : x.Value.Value,
                v => v == null ? null : new(v.Value)
            );

        return builder;
    }

    public static EntityTypeBuilder<CartItem> SetValidations(this EntityTypeBuilder<CartItem> builder)
    {
        builder.Property(x => x.Quantity)
            .IsRequired()
            .HasColumnName("Quantity");

        builder.Property(x => x.Weight)
            .IsRequired()
            .HasPrecision(6, 2)
            .HasColumnName("Weight");

        builder.Property(x => x.Price)
            .IsRequired()
            .HasPrecision(10, 2)
            .HasColumnName("Price");

        builder.Property(x => x.Delivery)
            .IsRequired()
            .HasColumnName("Delivery");

        builder.Property(x => x.Delivery)
            .IsRequired()
            .HasColumnName("Delivery");

        builder.Property(x => x.ProductId)
            .IsRequired()
            .HasColumnName("ProductId");

        builder.Property(x => x.CartId)
            .IsRequired()
            .HasColumnName("CartId");

        builder.Property(x => x.CadId)
            .HasColumnName("CadId");

        return builder;
    }
}
