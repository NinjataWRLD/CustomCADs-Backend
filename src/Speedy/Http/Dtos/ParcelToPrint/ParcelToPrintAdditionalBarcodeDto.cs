namespace CustomCADs.Speedy.Http.Dtos.ParcelToPrint;

/// <param name="Value">
///     Value of the additional barcode.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Format">
///     Format of the additional barcode.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Label">
///     Label for the additional barcode, if applicable.
///     <br />
///     Required: No.
/// </param>
internal record ParcelToPrintAdditionalBarcodeDto(
	string Value,
	Format Format,
	string? Label
);
