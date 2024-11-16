namespace CustomCADs.Shared.Speedy.API.Endpoints.PrintEndpoints.ExtendedPrint;

using Dtos.ParcelToPrint;

public record ExtendedPrintResponse(
    byte[] Data,
    LabelInfoDto[] PrintLabelsInfo,
    ErrorDto? Error
);
