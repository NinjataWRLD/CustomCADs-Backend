namespace CustomCADs.Auth.Infrastructure.Dtos;

public class JwtSettings
{
    public required string SecretKey { get; set; }
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
}
