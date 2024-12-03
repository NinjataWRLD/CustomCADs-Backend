namespace CustomCADs.Auth.Endpoints.SignIn.Login;

public sealed record LoginRequest(
    string Username,
    string Password,
    bool? RememberMe = default
);
