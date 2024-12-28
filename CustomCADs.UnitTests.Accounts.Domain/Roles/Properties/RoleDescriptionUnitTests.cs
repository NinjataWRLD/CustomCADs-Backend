using CustomCADs.Accounts.Domain.Common.Exceptions.Roles;

namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Properties;

using static RoleConstants;

public class RoleDescriptionUnitTests : RolesBaseUnitTests
{
    [Test]
    public void SetDescription_ShouldNotThrowException_WhenDescriptionIsValid()
    {
        string name = new('a', NameMinLength);
        string description = new('a', DescriptionMinLength);

        var role = Role.Create(name, description);

        Assert.DoesNotThrow(() =>
        {
            role.SetDescription(description);
        });
    }

    [Test]
    public void SetDescription_SetsDescription_WhenDescriptionIsValid()
    {
        string name = new('a', NameMinLength);
        string description = new('a', DescriptionMinLength);

        var role = Role.Create(name, description);
        role.SetDescription(description);

        Assert.That(role.Description, Is.EqualTo(description));
    }

    [Test]
    [TestCase(0)]
    [TestCase(DescriptionMinLength - 1)]
    [TestCase(DescriptionMaxLength + 1)]
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
