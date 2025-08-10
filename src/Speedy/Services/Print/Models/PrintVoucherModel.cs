using CustomCADs.Speedy.API.Endpoints.PrintEndpoints.Enums;

namespace CustomCADs.Speedy.Services.Print.Models;

public record PrintVoucherModel(
	string[] ShipmentIds,
	string? PrinterName,
	PaperFormat? Format,
	Dpi? Dpi
);
