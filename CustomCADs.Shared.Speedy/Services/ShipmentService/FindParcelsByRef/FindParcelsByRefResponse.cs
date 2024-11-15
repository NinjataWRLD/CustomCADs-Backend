namespace CustomCADs.Shared.Speedy.Services.ShipmentService.FindParcelsByRef;

using Dtos.Errors;

public record FindParcelsByRefResponse(
    string[] Barcodes,
    ErrorDto? Error
);
