namespace CustomCADs.Account.Domain.Users.ValueObjects;

public class NameInfo
{
    private NameInfo() { }

    private NameInfo(string? firstName, string? lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";

    public static NameInfo Create(string? firstName = default, string? lastName = default)
    {
        return new(firstName, lastName);
    }
}
