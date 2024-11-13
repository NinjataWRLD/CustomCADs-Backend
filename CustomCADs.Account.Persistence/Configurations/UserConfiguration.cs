using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Account.Persistence.Configurations;

using static UserConstants;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .SetPrimaryKey()
            .SetForeignKeys()
            .SetStronglyTypedIds()
            .SetValueObjects()
            .SetValidations();
    }
}

static class UserConfigUtils
{
    public static EntityTypeBuilder<User> SetPrimaryKey(this EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        return builder;
    }

    public static EntityTypeBuilder<User> SetForeignKeys(this EntityTypeBuilder<User> builder)
    {
        builder
            .HasOne(x => x.Role)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.RoleName)
            .OnDelete(DeleteBehavior.Restrict);

        return builder;
    }

    public static EntityTypeBuilder<User> SetStronglyTypedIds(this EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasConversion(
                x => x.Value,
                v => new(v)
            );
        
        return builder;
    }

    public static EntityTypeBuilder<User> SetValueObjects(this EntityTypeBuilder<User> builder)
    {
        builder.ComplexProperty(x => x.Names, a =>
        {
            a.Property(x => x.FirstName)
                .HasMaxLength(NameMaxLength)
                .HasColumnName("FirstName");

            a.Property(x => x.LastName)
                .HasMaxLength(NameMaxLength)
                .HasColumnName("LastName");
        });

        return builder;
    }

    public static EntityTypeBuilder<User> SetValidations(this EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Username)
            .IsRequired()
            .HasMaxLength(NameMaxLength)
            .HasColumnName("Username");

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(EmailMaxLength)
            .HasColumnName("Email");

        builder.Property(x => x.RoleName)
            .IsRequired()
            .HasColumnName("RoleName");

        return builder;
    }

}
