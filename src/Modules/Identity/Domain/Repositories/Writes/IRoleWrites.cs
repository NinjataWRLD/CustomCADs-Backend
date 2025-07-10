namespace CustomCADs.Identity.Domain.Repositories.Writes;

public interface IRoleWrites
{
	Task CreateAsync(string name);
	Task<bool> DeleteAsync(string name);
}
