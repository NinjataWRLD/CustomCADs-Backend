namespace CustomCADs.Shared.Infrastructure.Email;

public record EmailSettings(
    string Server,
    int Port,
    string From,
    string Password
)
{
    public EmailSettings() : this(
        Server: string.Empty,
        Port: 0,
        From: string.Empty,
        Password: string.Empty
    )
    { }
}
