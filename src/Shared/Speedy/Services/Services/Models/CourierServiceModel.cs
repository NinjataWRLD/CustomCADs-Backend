namespace CustomCADs.Shared.Speedy.Services.Services.Models;

public record CourierServiceModel(
	int Id,
	string Name,
	string NameEn,
	CargoType CargoType,
	bool RequireParcelWeight,
	bool RequireParcelSize,
	AdditionalCourierServicesModel AdditionalServices
);
