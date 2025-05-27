namespace CustomCADs.Shared.Speedy.API.Endpoints.PrintEndpoints.Print;

using Dtos.ParcelToPrint;
using Enums;

public record PrintRequest(
	string UserName,
	string Password,
	string? Language,
	long? ClientSystemId,
	PaperFormat? Format,
	PaperSize? PaperSize,
	ParcelToPrintDto[] Parcels,
	string? PrinterName,
	Dpi? Dpi,
	AdditionalWaybillSenderCopy? AdditionalWaybillSenderCopy
);
