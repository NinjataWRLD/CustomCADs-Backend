namespace CustomCADs.Account.Endpoints.Users.GetUsers;

public class GetUsersResponse
{
    public int Count { get; set; }
    public UserResponseDto[] Users { get; set; } = [];
}
