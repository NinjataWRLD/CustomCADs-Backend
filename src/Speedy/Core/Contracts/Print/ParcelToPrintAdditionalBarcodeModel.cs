namespace CustomCADs.Speedy.Core.Contracts.Print;

public record ParcelToPrintAdditionalBarcodeModel(
	string Value,
	Format Format,
	string? Label
);
