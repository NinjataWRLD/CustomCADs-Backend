using CustomCADs.Accounts.Domain.Common.Exceptions.Roles;
using CustomCADs.Shared.Core;

namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Create;

using static Constants.Roles;
using static RoleConstants;

public class RoleCreateUnitTests : RolesBaseUnitTests
{
    [Theory]
    [InlineData(Client, ClientDescription)]
    [InlineData(Contributor, ContributorDescription)]
    [InlineData(Designer, DesignerDescription)]
    [InlineData(Admin, AdminDescription)]
    public void Create_ShouldNotThrowException_WhenRoleIsValid(string name, string description)
    {
        Role.Create(name, description);
    }

    [Theory]
    [InlineData(Client, ClientDescription)]
    [InlineData(Contributor, ContributorDescription)]
    [InlineData(Designer, DesignerDescription)]
    [InlineData(Admin, AdminDescription)]
    public void Create_ShouldPopulatePropertiesProperly_WhenRoleIsValid(string name, string description)
    {
        var role = Role.Create(name, description);

        Assert.Multiple(() =>
        {
            Assert.Equal(role.Name, name);
            Assert.Equal(role.Description, description);
        });
    }

    [Theory]
    [InlineData(0)]
    [InlineData(NameMinLength - 1)]
    [InlineData(NameMaxLength + 1)]
    public void Create_ShouldThrowException_WhenNameIsInvalid(int nameLength)
    {
        string name = new('a', nameLength);
        string description = new('a', DescriptionMaxLength);

        Assert.Throws<RoleValidationException>(() =>
        {
            Role.Create(name, description);
        });
    }

    [Theory]
    [InlineData(0)]
    [InlineData(DescriptionMinLength - 1)]
    [InlineData(DescriptionMaxLength + 1)]
    public void Create_ShouldThrowException_WhenDescriptionIsInvalid(int descriptionLength)
    {
        string name = new('a', NameMinLength);
        string description = new('a', descriptionLength);

        Assert.Throws<RoleValidationException>(() =>
        {
            Role.Create(name, description);
        });
    }
}
