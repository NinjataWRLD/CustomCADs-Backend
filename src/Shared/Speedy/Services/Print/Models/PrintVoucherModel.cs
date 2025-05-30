using CustomCADs.Shared.Speedy.API.Endpoints.PrintEndpoints.Enums;

namespace CustomCADs.Shared.Speedy.Services.Print.Models;

public record PrintVoucherModel(
	string[] ShipmentIds,
	string? PrinterName,
	PaperFormat? Format,
	Dpi? Dpi
);
