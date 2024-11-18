namespace CustomCADs.Account.Endpoints.Users.GetAll;

public record GetUsersResponse(int Count, UserResponse[] Users);
