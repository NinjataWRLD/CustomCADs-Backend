using CustomCADs.Shared.Core.Bases.Exceptions;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.Files.Application.Common.Exceptions;

using static Constants.ExceptionMessages;

public class CadNotFoundException : BaseException
{
    private CadNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static CadNotFoundException General(Exception? inner = default)
        => new(string.Format(NotFound, "Cad"), inner);

    public static CadNotFoundException ById(CadId id, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "Cad", nameof(id), id), inner);

    public static CadNotFoundException Custom(string message, Exception? inner = default)
        => new(message, inner);
}