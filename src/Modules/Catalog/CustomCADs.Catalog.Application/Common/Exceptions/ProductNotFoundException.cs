using CustomCADs.Shared.Core.Bases.Exceptions;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.Catalog.Application.Common.Exceptions;

using static Constants.ExceptionMessages;

public class ProductNotFoundException : BaseException
{
    private ProductNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static ProductNotFoundException General(Exception? inner = default)
        => new("The requested Product does not exist.", inner);

    public static ProductNotFoundException ById(ProductId id, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "Product", nameof(id), id), inner);

    public static ProductNotFoundException CategoryId(CategoryId id, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "Category", nameof(id), id), inner);

    public static ProductNotFoundException DesignerId(AccountId id, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "Account", nameof(id), id), inner);

    public static ProductNotFoundException CreatorId(AccountId id, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "Account", nameof(id), id), inner);

    public static ProductNotFoundException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
