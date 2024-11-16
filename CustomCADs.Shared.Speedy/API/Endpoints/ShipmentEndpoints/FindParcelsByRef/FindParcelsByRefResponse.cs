namespace CustomCADs.Shared.Speedy.API.Endpoints.ShipmentEndpoints.FindParcelsByRef;

public record FindParcelsByRefResponse(
    string[] Barcodes,
    ErrorDto? Error
);
