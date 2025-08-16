namespace CustomCADs.Speedy.Core.Services.Print.Models;

public record ParcelToPrintAdditionalBarcodeModel(
	string Value,
	Format Format,
	string? Label
);
