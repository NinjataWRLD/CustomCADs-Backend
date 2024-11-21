using CustomCADs.Shared.Speedy.Models.Calculation.Content;
using CustomCADs.Shared.Speedy.Models.Calculation.Recipient;
using CustomCADs.Shared.Speedy.Models.Calculation.Sender;
using CustomCADs.Shared.Speedy.Models.Calculation.Service;
using CustomCADs.Shared.Speedy.Models.Shipment.Payment;

namespace CustomCADs.Shared.Speedy.Services.Calculation.Models;

public record CalculationModel(
    CalculationRecipientModel Recipient,
    CalculationServiceModel Service,
    CalculationContentModel Content,
    ShipmentPaymentModel Payment,
    CalculationSenderModel? Sender
);
