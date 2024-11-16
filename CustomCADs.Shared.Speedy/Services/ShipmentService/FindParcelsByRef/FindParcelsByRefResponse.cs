namespace CustomCADs.Shared.Speedy.Services.ShipmentService.FindParcelsByRef;

public record FindParcelsByRefResponse(
    string[] Barcodes,
    ErrorDto? Error
);
