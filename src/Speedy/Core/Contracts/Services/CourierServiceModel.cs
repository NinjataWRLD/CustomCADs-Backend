namespace CustomCADs.Speedy.Core.Contracts.Services;

public record CourierServiceModel(
	int Id,
	string Name,
	string NameEn,
	CargoType CargoType,
	bool RequireParcelWeight,
	bool RequireParcelSize,
	AdditionalCourierServicesModel AdditionalServices
);
