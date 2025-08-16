namespace CustomCADs.Speedy.Http.Endpoints.PrintEndpoints.PrintVoucher;

using Enums;

internal record PrintVoucherRequest(
	string UserName,
	string Password,
	string[] ShipmentIds,
	string? Language,
	long? ClientSystemId,
	string? PrinterName,
	PaperFormat? Format,
	Dpi? Dpi
);
