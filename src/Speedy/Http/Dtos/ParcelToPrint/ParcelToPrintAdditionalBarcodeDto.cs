namespace CustomCADs.Speedy.Http.Dtos.ParcelToPrint;

internal record ParcelToPrintAdditionalBarcodeDto(
	string Value,
	Format Format,
	string? Label
);
