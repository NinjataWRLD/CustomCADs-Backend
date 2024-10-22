namespace CustomCADs.Account.Domain.Users.ValueObjects;

public class RefreshToken(string value, DateTime endDate)
{
    public RefreshToken() : this(string.Empty, DateTime.UtcNow) { }

    public string Value { get; set; } = value;
    public DateTime EndDate { get; set; } = endDate;
}
