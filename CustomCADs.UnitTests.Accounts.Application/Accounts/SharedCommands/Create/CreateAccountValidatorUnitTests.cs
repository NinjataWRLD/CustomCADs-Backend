using CustomCADs.Accounts.Application.Accounts.SharedCommandHandlers;
using CustomCADs.Shared.UseCases.Accounts.Commands;
using CustomCADs.UnitTests.Accounts.Application.Accounts.SharedCommands.Create.Data;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.SharedCommands.Create;

public class CreateAccountValidatorUnitTests : AccountsBaseUnitTests
{
    private readonly CreateAccountValidator validator = new();

    [Theory]
    [ClassData(typeof(CreateAccountHandlerValidData))]
    public void Validate_ShouldBeValid_WhenAccountIsValid(string role, string username, string email, string timeZone, string? firstName, string? lastName)
    {
        // Arrange
        CreateAccountCommand command = new(
            Role: role,
            Username: username,
            Email: email,
            TimeZone: timeZone,
            FirstName: firstName,
            LastName: lastName
        );

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(CreateAccountHandlerInvalidUsernameData))]
    [ClassData(typeof(CreateAccountHandlerInvalidEmailData))]
    [ClassData(typeof(CreateAccountHandlerInvalidTimeZoneData))]
    [ClassData(typeof(CreateAccountHandlerInvalidFirstNameData))]
    [ClassData(typeof(CreateAccountHandlerInvalidLastNameData))]
    public void Validate_ShouldBeInvalid_WhenAccountIsNotValid(string role, string username, string email, string timeZone, string? firstName, string? lastName)
    {
        // Arrange
        CreateAccountCommand command = new(
            Role: role,
            Username: username,
            Email: email,
            TimeZone: timeZone,
            FirstName: firstName,
            LastName: lastName
        );

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(CreateAccountHandlerInvalidUsernameData))]
    public void Validate_ShouldReturnProperErrors_WhenUsernameIsNotValid(string role, string username, string email, string timeZone, string? firstName, string? lastName)
    {
        // Arrange
        CreateAccountCommand command = new(
            Role: role,
            Username: username,
            Email: email,
            TimeZone: timeZone,
            FirstName: firstName,
            LastName: lastName
        );

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Username);
    }

    [Theory]
    [ClassData(typeof(CreateAccountHandlerInvalidEmailData))]
    public void Validate_ShouldReturnProperErrors_WhenEmailIsNotValid(string role, string username, string email, string timeZone, string? firstName, string? lastName)
    {
        // Arrange
        CreateAccountCommand command = new(
            Role: role,
            Username: username,
            Email: email,
            TimeZone: timeZone,
            FirstName: firstName,
            LastName: lastName
        );

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Theory]
    [ClassData(typeof(CreateAccountHandlerInvalidTimeZoneData))]
    public void Validate_ShouldReturnProperErrors_WhenTimeZoneIsNotValid(string role, string username, string email, string timeZone, string? firstName, string? lastName)
    {
        // Arrange
        CreateAccountCommand command = new(
            Role: role,
            Username: username,
            Email: email,
            TimeZone: timeZone,
            FirstName: firstName,
            LastName: lastName
        );

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.TimeZone);
    }

    [Theory]
    [ClassData(typeof(CreateAccountHandlerInvalidFirstNameData))]
    public void Validate_ShouldReturnProperErrors_WhenFirstNameIsNotValid(string role, string username, string email, string timeZone, string? firstName, string? lastName)
    {
        // Arrange
        CreateAccountCommand command = new(
            Role: role,
            Username: username,
            Email: email,
            TimeZone: timeZone,
            FirstName: firstName,
            LastName: lastName
        );

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    [Theory]
    [ClassData(typeof(CreateAccountHandlerInvalidLastNameData))]
    public void Validate_ShouldReturnProperErrors_WhenLastNameIsNotValid(string role, string username, string email, string timeZone, string? firstName, string? lastName)
    {
        // Arrange
        CreateAccountCommand command = new(
            Role: role,
            Username: username,
            Email: email,
            TimeZone: timeZone,
            FirstName: firstName,
            LastName: lastName
        );

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.LastName);
    }
}
