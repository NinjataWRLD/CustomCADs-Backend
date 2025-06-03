namespace CustomCADs.Identity.Endpoints.Identity.Get.MyAccount;

public record MyAccountResponse(
    Guid Id,
    string Role,
    string Username,
    string Email,
    DateTimeOffset CreatedAt
);