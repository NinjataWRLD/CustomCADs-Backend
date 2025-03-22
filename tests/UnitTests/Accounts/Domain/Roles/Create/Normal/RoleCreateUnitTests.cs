using CustomCADs.Accounts.Domain.Roles.Exceptions;
using CustomCADs.UnitTests.Accounts.Domain.Roles.Create.Normal.Data;

namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Create.Normal;

public class RoleCreateUnitTests : RolesBaseUnitTests
{
    [Theory]
    [ClassData(typeof(RoleCreateValidData))]
    public void Create_ShouldNotThrowException_WhenRoleIsValid(string name, string description)
    {
        CreateRole(name, description);
    }

    [Theory]
    [ClassData(typeof(RoleCreateValidData))]
    public void Create_ShouldPopulatePropertiesProperly_WhenRoleIsValid(string name, string description)
    {
        var role = CreateRole(name, description);

        Assert.Multiple(() =>
        {
            Assert.Equal(role.Name, name);
            Assert.Equal(role.Description, description);
        });
    }

    [Theory]
    [ClassData(typeof(RoleCreateInvalidNameData))]
    public void Create_ShouldThrowException_WhenNameIsInvalid(string name, string description)
    {
        Assert.Throws<RoleValidationException>(() =>
        {
            CreateRole(name, description);
        });
    }

    [Theory]
    [ClassData(typeof(RoleCreateInvalidDescriptionData))]
    public void Create_ShouldThrowException_WhenDescriptionIsInvalid(string name, string description)
    {
        Assert.Throws<RoleValidationException>(() =>
        {
            CreateRole(name, description);
        });
    }
}
