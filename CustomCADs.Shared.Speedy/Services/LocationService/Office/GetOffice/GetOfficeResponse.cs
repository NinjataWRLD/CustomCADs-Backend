namespace CustomCADs.Shared.Speedy.Services.LocationService.Office.GetOffice;

using Dtos.Errors;
using Dtos.Office;

public record GetOfficeResponse(
    OfficeDto? Office,
    ErrorDto? Erro
);