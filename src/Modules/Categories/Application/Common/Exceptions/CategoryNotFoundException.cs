using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Categories.Application.Common.Exceptions;

using static Constants.ExceptionMessages;

public class CategoryNotFoundException : BaseException
{
    private CategoryNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static CategoryNotFoundException General(Exception? inner = default)
        => new(string.Format(NotFound, "Category"), inner);

    public static CategoryNotFoundException ById(CategoryId id, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "Category", nameof(id), id), inner);

    public static CategoryNotFoundException ByName(string name, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "Category", nameof(name), name), inner);

    public static CategoryNotFoundException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
