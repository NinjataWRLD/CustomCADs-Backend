using CustomCADs.Accounts.Domain.Common.Exceptions.Roles;

namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Properties;

using static RoleConstants;

public class RoleNameUnitTests : RolesBaseUnitTests
{
    [Fact]
    public void SetName_ShouldNotThrowException_WhenNameIsValid()
    {
        string name = new('a', NameMinLength);
        string description = new('a', DescriptionMinLength);

        var role = Role.Create(name, description);

        role.SetName(name);
    }

    [Fact]
    public void SetName_SetsName_WhenNameIsValid()
    {
        string name = new('a', NameMinLength);
        string description = new('a', DescriptionMinLength);

        var role = Role.Create(name, description);
        role.SetName(name);

        Assert.Equal(role.Name, name);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(NameMinLength - 1)]
    [InlineData(NameMaxLength + 1)]
    public void SetName_ThrowsException_WhenNameIsInvalid(int nameLength)
    {
        string name = new('a', NameMinLength);
        string description = new('a', DescriptionMinLength);

        var role = Role.Create(name, description);

        Assert.Throws<RoleValidationException>(() =>
        {
            name = new('a', nameLength);
            role.SetName(name);
        });
    }
}
