namespace CustomCADs.Account.Endpoints.Users.GetUsers;

public record GetUsersRequest(string? Name = default, string? Sorting = default, int Page = 1, int Limit = 50);
