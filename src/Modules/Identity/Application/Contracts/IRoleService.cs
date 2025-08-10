namespace CustomCADs.Identity.Application.Contracts;

public interface IRoleService
{
	Task CreateAsync(string name);
	Task<bool> DeleteAsync(string name);
}
