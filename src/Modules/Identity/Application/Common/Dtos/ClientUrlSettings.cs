namespace CustomCADs.Identity.Application.Common.Dtos;

public record ClientUrlSettings(string All, string Preferred)
{
    public ClientUrlSettings() : this("", "") { }
}
