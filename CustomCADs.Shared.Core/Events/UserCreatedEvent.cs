namespace CustomCADs.Shared.Core.Events;

public class UserCreatedEvent
{
    public required string Role { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}
