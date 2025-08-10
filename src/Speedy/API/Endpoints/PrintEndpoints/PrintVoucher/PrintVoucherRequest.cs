namespace CustomCADs.Speedy.API.Endpoints.PrintEndpoints.PrintVoucher;

using Enums;

public record PrintVoucherRequest(
	string UserName,
	string Password,
	string[] ShipmentIds,
	string? Language,
	long? ClientSystemId,
	string? PrinterName,
	PaperFormat? Format,
	Dpi? Dpi
);
