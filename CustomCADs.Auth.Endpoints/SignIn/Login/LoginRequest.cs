namespace CustomCADs.Auth.Endpoints.SignIn.Login;

public record LoginRequest(
    string Username, 
    string Password, 
    bool? RememberMe = default
);
