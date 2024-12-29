using CustomCADs.Accounts.Domain.Common.Exceptions.Roles;

namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Properties;

using static RoleConstants;

public class RoleDescriptionUnitTests : RolesBaseUnitTests
{
    [Fact]
    public void SetDescription_ShouldNotThrowException_WhenDescriptionIsValid()
    {
        string name = new('a', NameMinLength);
        string description = new('a', DescriptionMinLength);

        var role = Role.Create(name, description);

        role.SetDescription(description);
    }

    [Fact]
    public void SetDescription_SetsDescription_WhenDescriptionIsValid()
    {
        string name = new('a', NameMinLength);
        string description = new('a', DescriptionMinLength);

        var role = Role.Create(name, description);
        role.SetDescription(description);

        Assert.Equal(role.Description, description);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(DescriptionMinLength - 1)]
    [InlineData(DescriptionMaxLength + 1)]
    public void SetDescription_ThrowsException_WhenDescriptionIsInvalid(int descriptionLength)
    {
        string name = new('a', NameMinLength);
        string description = new('a', DescriptionMinLength);

        var role = Role.Create(name, description);

        Assert.Throws<RoleValidationException>(() =>
        {
            description = new('a', descriptionLength);
            role.SetDescription(description);
        });
    }
}
