using CustomCADs.Shared.Speedy.API.Endpoints.PrintEndpoints.Enums;

namespace CustomCADs.Shared.Speedy.Services.Print.Models;

public record PrintModel(
    string? PrinterName,
    PaperFormat? Format,
    PaperSize? PaperSize,
    Dpi? Dpi,
    AdditionalWaybillSenderCopy? AdditionalWaybillSenderCopy,
    ParcelToPrintModel[] Parcels
);
