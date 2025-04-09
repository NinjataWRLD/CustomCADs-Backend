using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Payment;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Purchase.Normal;

public sealed class PurchaseCustomHandler(ICustomReads reads, IUnitOfWork uow, IRequestSender sender, IPaymentService payment)
    : ICommandHandler<PurchaseCustomCommand, string>
{
    public async Task<string> Handle(PurchaseCustomCommand req, CancellationToken ct)
    {
        Custom custom = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Custom>.ById(req.Id);

        if (custom.BuyerId != req.BuyerId)
            throw CustomAuthorizationException<Custom>.ById(custom.Id);

        if (custom.ForDelivery)
            throw CustomException.Delivery<Custom>(custom.ForDelivery);

        if (custom.AcceptedCustom is null)
            throw CustomException.NullProp<Custom>(nameof(custom.AcceptedCustom));

        if (custom.FinishedCustom is null)
            throw CustomException.NullProp<Custom>(nameof(custom.FinishedCustom));

        string[] users = await Task.WhenAll(
            sender.SendQueryAsync(new GetUsernameByIdQuery(custom.BuyerId), ct),
            sender.SendQueryAsync(new GetUsernameByIdQuery(custom.AcceptedCustom.DesignerId), ct)
        ).ConfigureAwait(false);

        string buyer = users[0], seller = users[1];
        decimal total = custom.FinishedCustom.Price;

        string message = await payment.InitializePayment(
            paymentMethodId: req.PaymentMethodId,
            price: total,
            description: $"{buyer} bought {custom.Name} from {seller}.",
            ct
        ).ConfigureAwait(false);

        custom.Complete(customizationId: null);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return message;
    }
}
