namespace CustomCADs.Shared.Speedy.API.Dtos.ParcelToPrint;

using Enums;

public record ParcelToPrintAdditionalBarcodeDto(
    string Value,
    Format Format,
    string? Label
);