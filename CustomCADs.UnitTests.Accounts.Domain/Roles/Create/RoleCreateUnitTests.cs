using CustomCADs.Accounts.Domain.Common.Exceptions.Roles;
using CustomCADs.Shared.Core;

namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Create;

using static Constants.Roles;
using static RoleConstants;

public class RoleCreateUnitTests : RolesBaseUnitTests
{
    [Test]
    [TestCase(Client, ClientDescription)]
    [TestCase(Contributor, ContributorDescription)]
    [TestCase(Designer, DesignerDescription)]
    [TestCase(Admin, AdminDescription)]
    public void Create_ShouldNotThrowException_WhenRoleIsValid(string name, string description)
    {
        Assert.DoesNotThrow(() =>
        {
            Role.Create(name, description);
        });
    }

    [Test]
    [TestCase(Client, ClientDescription)]
    [TestCase(Contributor, ContributorDescription)]
    [TestCase(Designer, DesignerDescription)]
    [TestCase(Admin, AdminDescription)]
    public void Create_ShouldPopulatePropertiesProperly_WhenRoleIsValid(string name, string description)
    {
        var role = Role.Create(name, description);

        Assert.Multiple(() =>
        {
            Assert.That(role.Name, Is.EqualTo(name));
            Assert.That(role.Description, Is.EqualTo(description));
        });
    }

    [Test]
    [TestCase(0)]
    [TestCase(NameMinLength - 1)]
    [TestCase(NameMaxLength + 1)]
    public void Create_ShouldThrowException_WhenNameIsInvalid(int nameLength)
    {
        string name = new('a', nameLength);
        string description = new('a', DescriptionMaxLength);

        Assert.Throws<RoleValidationException>(() =>
        {
            Role.Create(name, description);
        });
    }

    [Test]
    [TestCase(0)]
    [TestCase(DescriptionMinLength - 1)]
    [TestCase(DescriptionMaxLength + 1)]
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
