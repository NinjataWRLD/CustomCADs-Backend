using CustomCADs.Accounts.Endpoints.Users;

namespace CustomCADs.Accounts.Endpoints.Users.Get.All;

public record GetAccountsResponse(int Count, AccountResponse[] Users);
