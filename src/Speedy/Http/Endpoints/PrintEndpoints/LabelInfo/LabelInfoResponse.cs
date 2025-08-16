namespace CustomCADs.Speedy.Http.Endpoints.PrintEndpoints.LabelInfo;

using Dtos.ParcelToPrint;

internal record LabelInfoResponse(
	LabelInfoDto[] PrintLabelsInfo,
	ErrorDto? Error
);
