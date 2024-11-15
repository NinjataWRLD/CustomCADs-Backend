namespace CustomCADs.Shared.Speedy.Services.PrintService.LabelInfo;

using Dtos.Errors;
using Dtos.ParcelToPrint;

public record LabelInfoResponse(
    LabelInfoDto[] PrintLabelsInfo,
    ErrorDto? Erorr
);
