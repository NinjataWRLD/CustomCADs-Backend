namespace CustomCADs.Shared.Speedy.Services.Shipment.Models;

public record FindParcelModel(
    string Ref,
    int SearchInRef,
    bool? ShipmentsOnly,
    bool? IncludeReturns,
    string? FromDateTime,
    string? ToDateTime
);