namespace CustomCADs.Speedy.API.Dtos.ParcelToPrint;

public record ParcelToPrintAdditionalBarcodeDto(
	string Value,
	Format Format,
	string? Label
);
