namespace CustomCADs.Account.Endpoints.Users.PostUser;

public class PostUserRequest
{
    public required string Role { get; set; } 
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
