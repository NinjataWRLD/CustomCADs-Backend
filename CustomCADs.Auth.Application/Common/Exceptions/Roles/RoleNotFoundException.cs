using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Common.Exceptions;

namespace CustomCADs.Auth.Application.Common.Exceptions.Roles;

using static Constants.ExceptionMessages;

public class RoleNotFoundException : BaseException
{
    private RoleNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static RoleNotFoundException General(Exception? inner = default)
        => new(string.Format(NotFound, "Role"), inner);

    public static RoleNotFoundException ById(int id, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "Role", nameof(id), id), inner);

    public static RoleNotFoundException ByName(string name, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "Role", nameof(name), name), inner);

    public static RoleNotFoundException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
