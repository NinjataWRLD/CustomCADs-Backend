namespace CustomCADs.Account.Application.Roles.Common.Exceptions;

public class RoleNotFoundException : Exception
{
    public RoleNotFoundException() : base("The requested Role does not exist.") { }
    public RoleNotFoundException(int id) : base($"The Role with id: {id} does not exist.") { }
    public RoleNotFoundException(string name, object? overloadUtility = default) : base($"The Role with name: {name} does not exist.") { }
    public RoleNotFoundException(string message) : base(message) { }
    public RoleNotFoundException(string message, Exception inner) : base(message, inner) { }
}
