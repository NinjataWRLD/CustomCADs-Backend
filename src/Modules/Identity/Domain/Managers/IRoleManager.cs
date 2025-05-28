namespace CustomCADs.Identity.Domain.Managers;

public interface IRoleManager
{
	Task CreateAsync(string name);
	Task<bool> DeleteAsync(string name);
}
