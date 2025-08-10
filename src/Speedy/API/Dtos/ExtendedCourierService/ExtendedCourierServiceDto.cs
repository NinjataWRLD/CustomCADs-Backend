namespace CustomCADs.Speedy.API.Dtos.ExtendedCourierService;

using CourierService;

public record ExtendedCourierServiceDto(
	string Deadline,

	// Copied from CourierServiceDto
	int Id,
	string Name,
	string NameEn,
	CargoType CargoType,
	bool RequireParcelWeight,
	bool RequireParcelSize,
	AdditionalCourierServicesDto AdditionalServices
);
