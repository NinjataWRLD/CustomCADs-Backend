using CustomCADs.Accounts.Domain.Common.Exceptions.Roles;

namespace CustomCADs.Accounts.UnitTests.Domain.Roles.Properties;

using static RoleConstants;

public class RoleNameUnitTests : RolesBaseUnitTests
{
    [Test]
    public void SetName_ShouldNotThrowException_WhenNameIsValid()
    {
        string name = new('a', NameMinLength);
        string description = new('a', DescriptionMinLength);

        var role = Role.Create(name, description);

        Assert.DoesNotThrow(() =>
        {
            role.SetName(name);
        });
    }

    [Test]
    public void SetName_SetsName_WhenNameIsValid()
    {
        string name = new('a', NameMinLength);
        string description = new('a', DescriptionMinLength);

        var role = Role.Create(name, description);
        role.SetName(name);

        Assert.That(role.Name, Is.EqualTo(name));
    }

    [Test]
    [TestCase(0)]
    [TestCase(NameMinLength - 1)]
    [TestCase(NameMaxLength + 1)]
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
