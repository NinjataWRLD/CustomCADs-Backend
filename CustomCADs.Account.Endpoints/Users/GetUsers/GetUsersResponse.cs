namespace CustomCADs.Account.Endpoints.Users.GetUsers;

public record GetUsersResponse(int Count, UserResponse[] Users);
