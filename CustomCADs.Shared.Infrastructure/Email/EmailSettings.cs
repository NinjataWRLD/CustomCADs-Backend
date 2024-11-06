namespace CustomCADs.Shared.Infrastructure.Email;

public record EmailSettings(string Password, int Port)
{
    public EmailSettings() : this(string.Empty, 0) { }
}
