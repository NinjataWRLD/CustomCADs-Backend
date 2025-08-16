namespace CustomCADs.Speedy.Http.Dtos.CourierService;

internal record CourierServiceDto(
	int Id,
	string Name,
	string NameEn,
	CargoType CargoType,
	bool RequireParcelWeight,
	bool RequireParcelSize,
	AdditionalCourierServicesDto AdditionalServices
);
