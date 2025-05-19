namespace CustomCADs.Identity.Application.Users.Dtos;

public record ClientUrlSettings(string All, string Preferred)
{
	public ClientUrlSettings() : this("", "") { }
}
