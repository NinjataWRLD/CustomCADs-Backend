using CustomCADs.Accounts.Domain.Accounts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Accounts.Persistence.Configurations.Accounts;
public class Configurations : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder
            .SetPrimaryKey()
            .SetStronglyTypedIds()
            .SetIndexes()
            .SetValidations()
            .SetSeeding();
    }
}
