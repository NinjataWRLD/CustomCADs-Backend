namespace CustomCADs.Shared.Speedy.API.Endpoints.PrintEndpoints.LabelInfo;

using Dtos.ShipmentParcels;

public record LabelInfoRequest(
	string UserName,
	string Password,
	ShipmentParcelRefDto[] Parcels,
	string? Language,
	long? ClientSystemId
);
