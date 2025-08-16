using CustomCADs.Speedy.Http.Enums;

namespace CustomCADs.Speedy.Core.Services.Print.Models;

public record PrintVoucherModel(
	string[] ShipmentIds,
	string? PrinterName,
	PaperFormat? Format,
	Dpi? Dpi
);
