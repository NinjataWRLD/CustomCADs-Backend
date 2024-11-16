namespace CustomCADs.Shared.Speedy.Services.ServicesService.DestinationServices;

using Dtos.CalculationRecipient;
using Dtos.CalculationSender;

public record DestinationServicesRequest(
    string UserName,
    string Password,
    CalculationRecipientDto Recipient,
    CalculationSenderDto? Sender,
    string? Language,
    long? ClientSystemId,
    string? Date
);
