namespace CustomCADs.Speedy.Http.Dtos.ExtendedCourierService;

using CourierService;

internal record ExtendedCourierServiceDto(
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
