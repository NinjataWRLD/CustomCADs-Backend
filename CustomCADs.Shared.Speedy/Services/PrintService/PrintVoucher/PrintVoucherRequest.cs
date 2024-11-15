namespace CustomCADs.Shared.Speedy.Services.PrintService.PrintVoucher;

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
