namespace CustomCADs.Speedy.Http.Endpoints.PrintEndpoints.ExtendedPrint;

using Dtos.ParcelToPrint;

internal record ExtendedPrintResponse(
	byte[] Data,
	LabelInfoDto[] PrintLabelsInfo,
	ErrorDto? Error
);
