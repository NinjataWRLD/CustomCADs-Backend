namespace CustomCADs.Account.Endpoints.Users.Get.All;

public record GetUsersResponse(int Count, UserResponse[] Users);
