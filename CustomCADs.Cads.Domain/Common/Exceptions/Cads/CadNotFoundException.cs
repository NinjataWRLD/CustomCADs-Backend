using CustomCADs.Shared.Core.Common.Exceptions;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Cads;

namespace CustomCADs.Cads.Domain.Common.Exceptions.Cads;

public class CadNotFoundException : BaseException
{
    private CadNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static CadNotFoundException General(Exception? inner = default)
        => new("The requested Cad does not exist.", inner);

    public static CadNotFoundException ById(CadId id, Exception? inner = default)
        => new($"The Cad with id: {id} does not exist.", inner);

    public static CadNotFoundException Custom(string message, Exception? inner = default)
        => new(message, inner);
}