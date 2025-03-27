using CustomCADs.Shared.Core.Common.Exceptions.Domain;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.UnitTests.Accounts.Domain.Roles.Create.WithId.Data;

namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Create.WithId;

public class RoleCreateWithIdUnitTests : RolesBaseUnitTests
{
    [Theory]
    [ClassData(typeof(RoleCreateWithIdValidData))]
    public void CreateWithId_ShouldNotThrowException_WhenRoleIsValid(RoleId id, string name, string description)
    {
        CreateRoleWithId(id, name, description);
    }

    [Theory]
    [ClassData(typeof(RoleCreateWithIdValidData))]
    public void CreateWithId_ShouldPopulatePropertiesProperly_WhenRoleIsValid(RoleId id, string name, string description)
    {
        var role = CreateRoleWithId(id, name, description);

        Assert.Multiple(() =>
        {
            Assert.Equal(role.Name, name);
            Assert.Equal(role.Description, description);
        });
    }

    [Theory]
    [ClassData(typeof(RoleCreateWithIdInvalidNameData))]
    [ClassData(typeof(RoleCreateWithIdInvalidWithIdDescriptionData))]
    public void CreateWithId_ShouldThrowException_WhenCategoryIsInvalid(RoleId id, string name, string description)
    {
        Assert.Throws<CustomValidationException<Role>>(() =>
        {
            CreateRoleWithId(id, name, description);
        });
    }
}
