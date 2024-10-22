namespace CustomCADs.Account.Domain.Users.ValueObjects;

public class NameInfo
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
}
