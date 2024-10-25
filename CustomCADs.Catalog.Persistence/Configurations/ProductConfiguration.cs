using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Catalog.Domain.Products.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static CustomCADs.Catalog.Domain.Products.ProductConstants;

namespace CustomCADs.Catalog.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .SetPrimaryKey()
            .SetForeignKeys()
            .SetValueObjects()
            .SetValidations();
    }
}

static class CadConfigUtils
{
    public static EntityTypeBuilder<Product> SetPrimaryKey(this EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        return builder;
    }

    public static EntityTypeBuilder<Product> SetForeignKeys(this EntityTypeBuilder<Product> builder)
    {
        builder
            .HasOne(c => c.Category).WithMany()
            .HasForeignKey(c => c.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);

        return builder;
    }

    public static EntityTypeBuilder<Product> SetValueObjects(this EntityTypeBuilder<Product> builder)
    {
        builder.OwnsOne(p => p.Cad, pb =>
        {
            pb.Property(c => c.Path).IsRequired().HasColumnName("CadPath");

            pb.OwnsOne(c => c.CamCoordinates, cb =>
            {
                cb.Property(c => c.X).IsRequired().HasColumnName("CamCoordX");
                cb.Property(c => c.Y).IsRequired().HasColumnName("CamCoordY");
                cb.Property(c => c.Z).IsRequired().HasColumnName("CamCoordZ");
            });

            pb.OwnsOne(c => c.PanCoordinates, cb =>
            {
                cb.Property(c => c.X).IsRequired().HasColumnName("PanCoordX");
                cb.Property(c => c.Y).IsRequired().HasColumnName("PanCoordY");
                cb.Property(c => c.Z).IsRequired().HasColumnName("PanCoordZ");
            });
        });

        return builder;
    }

    public static EntityTypeBuilder<Product> SetValidations(this EntityTypeBuilder<Product> builder)
    {
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(NameMaxLength);

        builder.Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(DescriptionMaxLength);

        builder.Property(c => c.Status)
            .IsRequired()
            .HasConversion(
                e => e.ToString(),
                s => Enum.Parse<ProductStatus>(s)
            );

        builder.Property(c => c.Cost)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(c => c.UploadDate)
            .IsRequired();

        builder.Property(c => c.CategoryId)
            .IsRequired();

        builder.Property(c => c.CreatorId)
            .IsRequired();

        return builder;
    }
}
