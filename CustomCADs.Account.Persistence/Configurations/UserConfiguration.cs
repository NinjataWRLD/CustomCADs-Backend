﻿using CustomCADs.Account.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static CustomCADs.Account.Domain.Users.UserConstants;

namespace CustomCADs.Account.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .SetPrimaryKey()
            .SetForeignKeys()
            .SetValueObjects()
            .SetValidations();
    }
}

static class UserConfigUtils
{
    public static EntityTypeBuilder<User> SetPrimaryKey(this EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(r => r.Id).ValueGeneratedOnAdd();

        return builder;
    }

    public static EntityTypeBuilder<User> SetForeignKeys(this EntityTypeBuilder<User> builder)
    {
        builder
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleName)
            .OnDelete(DeleteBehavior.Restrict);

        return builder;
    }

    public static EntityTypeBuilder<User> SetValueObjects(this EntityTypeBuilder<User> builder)
    {
        builder.OwnsOne(u => u.NameInfo, cb =>
        {
            cb.Property(u => u.FirstName).HasMaxLength(NameMaxLength).HasColumnName("FirstName");
            cb.Property(u => u.LastName).HasMaxLength(NameMaxLength).HasColumnName("LastName");
        });

        return builder;
    }

    public static EntityTypeBuilder<User> SetValidations(this EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(NameMaxLength);

        builder.Property(u => u.Email)
            .IsRequired();

        builder.Property(u => u.RoleName)
            .IsRequired();

        return builder;
    }

}
