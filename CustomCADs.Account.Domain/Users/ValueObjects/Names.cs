namespace CustomCADs.Account.Domain.Users.ValueObjects;

public class Names
{
    private Names() { }

    private Names(string? firstName, string? lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";

    public static Names Create(string? firstName = default, string? lastName = default)
    {
        return new(firstName, lastName);
    }
}
