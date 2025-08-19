namespace CustomCADs.Speedy.Http.Dtos.CourierService;

/// <param name="Id">
///     Courier service id.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Name">
///     Courier service name (local language).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="NameEn">
///     Courier service name (English).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="CargoType">
///     Cargo type for the courier service.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="RequireParcelWeight">
///     Flag indicating if parcel weight is required.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="RequireParcelSize">
///     Flag indicating if parcel size is required.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="AdditionalServices">
///     Additional services for the courier service.
///     <br />
///     Required: Yes. See AdditionalCourierServicesDto for details.
/// </param>
internal record CourierServiceDto(
	int Id,
	string Name,
	string NameEn,
	CargoType CargoType,
	bool RequireParcelWeight,
	bool RequireParcelSize,
	AdditionalCourierServicesDto AdditionalServices
);
