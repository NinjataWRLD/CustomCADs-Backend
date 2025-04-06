using CustomCADs.Customs.Domain.Customs.Events;
using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.Abstractions.Payment;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Cads.Queries;
using CustomCADs.Shared.UseCases.Customizations.Queries;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Purchase.WithDelivery;

public sealed class PurchaseCustomWithDeliveryHandler(ICustomReads reads, IUnitOfWork uow, IRequestSender sender, IPaymentService payment, IEventRaiser raiser)
    : ICommandHandler<PurchaseCustomWithDeliveryCommand, string>
{
    public async Task<string> Handle(PurchaseCustomWithDeliveryCommand req, CancellationToken ct)
    {
        Custom custom = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Custom>.ById(req.Id);

        if (custom.BuyerId != req.BuyerId)
            throw CustomAuthorizationException<Custom>.ById(custom.Id);

        if (!custom.ForDelivery)
            throw CustomException.Delivery<Custom>(custom.ForDelivery);

        if (custom.AcceptedCustom is null)
            throw CustomException.NullProp<Custom>(nameof(custom.AcceptedCustom.DesignerId));

        if (custom.FinishedCustom is null)
            throw CustomException.NullProp<Custom>(nameof(custom.FinishedCustom.CadId));

        GetUsernameByIdQuery buyerQuery = new(custom.BuyerId),
            sellerQuery = new(custom.AcceptedCustom.DesignerId);

        string[] users = await Task.WhenAll(
            sender.SendQueryAsync(buyerQuery, ct),
            sender.SendQueryAsync(sellerQuery, ct)
        ).ConfigureAwait(false);
        string buyer = users[0], seller = users[1];

        GetCustomizationCostByIdQuery costQuery = new(req.CustomizationId);
        decimal cost = await sender.SendQueryAsync(costQuery, ct).ConfigureAwait(false);
        decimal total = req.Count * (custom.FinishedCustom.Price + cost);

        string message = await payment.InitializePayment(
            paymentMethodId: req.PaymentMethodId,
            price: total,
            description: $"{buyer} bought {custom.Name} from {seller} for {total}$.",
            ct
        ).ConfigureAwait(false);

        GetCustomizationWeightByIdQuery weightQuery = new(req.CustomizationId);
        double weight = await sender.SendQueryAsync(weightQuery, ct).ConfigureAwait(false);

        GetCadExistsByIdQuery cadQuery = new(custom.FinishedCustom.CadId);
        bool cadExists = await sender.SendQueryAsync(cadQuery, ct).ConfigureAwait(false);
        if (!cadExists)
            throw CustomNotFoundException<Custom>.ById(custom.FinishedCustom.CadId, "Cad");

        GetCustomizationExistsByIdQuery customizationExistsQuery = new(
            Id: req.CustomizationId
        );

        bool customizationExists = await sender.SendQueryAsync(customizationExistsQuery, ct).ConfigureAwait(false);
        if (!customizationExists)
            throw CustomNotFoundException<Custom>.ById(req.CustomizationId.Value, "Customization");

        custom.Complete(customizationId: req.CustomizationId);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        await raiser.RaiseDomainEventAsync(new CustomDeliveryRequestedDomainEvent(
            Id: req.Id,
            ShipmentService: req.ShipmentService,
            Weight: weight * req.Count,
            Count: req.Count,
            Address: req.Address,
            Contact: req.Contact
        )).ConfigureAwait(false);

        return message;
    }
}
