namespace CustomCADs.Shared.Speedy.Services.Payment.Models;

public record PayoutDetailsModel(
    int LineNo,
    string ShipmentId,
    DateOnly PickupDate,
    DateOnly PrimaryShipmentPickupDate,
    DateOnly DeliveryDate,
    string Sender,
    string Recipient,
    string Note,
    string Ref1,
    string Ref2,
    string Currency,
    long Order,
    double Amount
);
