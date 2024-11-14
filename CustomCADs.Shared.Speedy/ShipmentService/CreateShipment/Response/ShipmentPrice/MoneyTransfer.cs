using CustomCADs.Shared.Speedy.ShipmentService.CreateShipment.Request.ShipmentPayment;

namespace CustomCADs.Shared.Speedy.ShipmentService.CreateShipment.Response.ShipmentPrice;

public record MoneyTransfer(
    double Amount,
    double AmountLocal,
    Payer Payer
);
