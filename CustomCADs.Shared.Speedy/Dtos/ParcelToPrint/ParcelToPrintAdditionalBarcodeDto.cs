namespace CustomCADs.Shared.Speedy.Dtos.ParcelToPrint;

public record ParcelToPrintAdditionalBarcodeDto(
    string Value,
    Format Format,
    string? Label
);