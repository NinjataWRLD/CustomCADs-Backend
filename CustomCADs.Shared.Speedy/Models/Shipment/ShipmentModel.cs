using CustomCADs.Shared.Speedy.Models.Shipment.Content;
using CustomCADs.Shared.Speedy.Models.Shipment.Delivery;
using CustomCADs.Shared.Speedy.Models.Shipment.Payment;
using CustomCADs.Shared.Speedy.Models.Shipment.Primary;
using CustomCADs.Shared.Speedy.Models.Shipment.Recipient;
using CustomCADs.Shared.Speedy.Models.Shipment.Sender;
using CustomCADs.Shared.Speedy.Models.Shipment.Service;
using CustomCADs.Shared.Speedy.Services.Shipment.Models;

namespace CustomCADs.Shared.Speedy.Models.Shipment;

public record ShipmentModel(
    string Id,
    ShipmentSenderModel Sender,
    ShipmentRecipientModel Recipient,
    ShipmentServiceModel Service,
    ShipmentContentModel Content,
    ShipmentPaymentModel Payment,
    string ShipmentNote,
    string Ref1,
    string Ref2,
    ShipmentPriceModel Price,
    ShipmentDeliveryModel Delivery,
    PrimaryShipmentModel PrimaryShipment,
    string ReturnShipmentId,
    string RedirectShipmentId,
    bool PendingShipment
);