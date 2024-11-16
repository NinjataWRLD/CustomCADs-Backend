namespace CustomCADs.Shared.Speedy.Services.PrintService.ExtendedPrint;

using Dtos.ParcelToPrint;

public record ExtendedPrintResponse(
    byte[] Data,
    LabelInfoDto[] PrintLabelsInfo,
    ErrorDto? Error
);
