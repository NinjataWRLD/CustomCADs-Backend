using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Customizations.Application.Common.Exceptions;

using static Constants.ExceptionMessages;

public class MaterialNotFoundException : BaseException
{
    private MaterialNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static MaterialNotFoundException General(Exception? inner = default)
        => new(string.Format(NotFound, "Material"), inner);

    public static MaterialNotFoundException ById(MaterialId id, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "Material", nameof(id), id), inner);

    public static MaterialNotFoundException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
