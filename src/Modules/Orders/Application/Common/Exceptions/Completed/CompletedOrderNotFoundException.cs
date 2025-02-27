using CustomCADs.Shared.Core.Bases.Exceptions;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.Orders.Application.Common.Exceptions.Completed;

using static Constants.ExceptionMessages;

public class CompletedOrderNotFoundException : BaseException
{
    private CompletedOrderNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static CompletedOrderNotFoundException General(Exception? inner = default)
        => new(string.Format(NotFound, "Completed Order"), inner);

    public static CompletedOrderNotFoundException ById(CompletedOrderId id, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "Completed Order", nameof(id), id), inner);

    public static CompletedOrderNotFoundException BuyerId(AccountId id, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "Account", nameof(id), id), inner);

    public static CompletedOrderNotFoundException DesignerId(AccountId id, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "Account", nameof(id), id), inner);

    public static CompletedOrderNotFoundException CadId(CadId id, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "Cad", nameof(id), id), inner);

    public static CompletedOrderNotFoundException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
