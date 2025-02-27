namespace CustomCADs.Identity.Infrastructure.Dtos;

public record ClientUrlSettings(string All, string Preferred)
{
    public ClientUrlSettings() : this("", "") { }
}
