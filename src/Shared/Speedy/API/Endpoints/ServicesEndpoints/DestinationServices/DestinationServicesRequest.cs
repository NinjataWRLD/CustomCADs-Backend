namespace CustomCADs.Shared.Speedy.API.Endpoints.ServicesEndpoints.DestinationServices;

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
