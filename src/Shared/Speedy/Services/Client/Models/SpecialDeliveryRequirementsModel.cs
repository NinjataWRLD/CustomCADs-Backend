namespace CustomCADs.Shared.Speedy.Services.Client.Models;

public record SpecialDeliveryRequirementsModel(
	bool RequiredForAllShipments,
	(int Id, string Text)[] Requirements
);
