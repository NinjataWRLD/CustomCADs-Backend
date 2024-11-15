namespace CustomCADs.Shared.Speedy.Dtos.ParcelToPrint;

using Enums;

public record ParcelToPrintAdditionalBarcodeDto(
    string Value,
    Format Format,
    string? Label
);