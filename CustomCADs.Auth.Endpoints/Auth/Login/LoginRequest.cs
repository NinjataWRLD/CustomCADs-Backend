namespace CustomCADs.Auth.Endpoints.Auth.Login;

public record LoginRequest(string Username, string Password, bool? RememberMe = default);
