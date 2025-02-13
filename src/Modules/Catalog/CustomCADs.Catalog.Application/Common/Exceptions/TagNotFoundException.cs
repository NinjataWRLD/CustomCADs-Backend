using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Catalog.Application.Common.Exceptions;

using static Constants.ExceptionMessages;

public class TagNotFoundException : BaseException
{
    private TagNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static TagNotFoundException General(Exception? inner = default)
        => new("The requested Tag does not exist.", inner);

    public static TagNotFoundException ById(TagId id, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "Tag", nameof(id), id), inner);

    public static TagNotFoundException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
