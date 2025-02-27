using CustomCADs.Carts.Domain.ActiveCarts;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Carts.Persistence.ActiveCarts.Configurations;

public class ActiveCartConfigurations : IEntityTypeConfiguration<ActiveCart>
{
    public void Configure(EntityTypeBuilder<ActiveCart> builder)
        => builder
            .SetPrimaryKey()
            .SetForeignKeys()
            .SetStronglyTypedIds()
            .SetValidations();
}

public static class ActiveCartItemConfigurUitls
{
    public static EntityTypeBuilder<ActiveCart> SetPrimaryKey(this EntityTypeBuilder<ActiveCart> builder)
    {
        builder.HasKey(x => x.Id);

        return builder;
    }

    public static EntityTypeBuilder<ActiveCart> SetForeignKeys(this EntityTypeBuilder<ActiveCart> builder)
    {
        builder
            .HasMany(x => x.Items)
            .WithOne(x => x.Cart)
            .HasForeignKey(x => x.CartId)
            .OnDelete(DeleteBehavior.Cascade);

        return builder;
    }

    public static EntityTypeBuilder<ActiveCart> SetStronglyTypedIds(this EntityTypeBuilder<ActiveCart> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasConversion(
                x => x.Value,
                v => ActiveCartId.New(v)
            );

        builder.Property(x => x.BuyerId)
            .HasConversion(
                x => x.Value,
                v => AccountId.New(v)
            );

        return builder;
    }

    public static EntityTypeBuilder<ActiveCart> SetValidations(this EntityTypeBuilder<ActiveCart> builder)
    {
        builder.Property(x => x.BuyerId)
            .IsRequired()
            .HasColumnName("BuyerId");

        return builder;
    }
}
