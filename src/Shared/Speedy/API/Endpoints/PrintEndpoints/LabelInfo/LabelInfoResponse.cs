namespace CustomCADs.Shared.Speedy.API.Endpoints.PrintEndpoints.LabelInfo;

using Dtos.ParcelToPrint;

public record LabelInfoResponse(
	LabelInfoDto[] PrintLabelsInfo,
	ErrorDto? Error
);
