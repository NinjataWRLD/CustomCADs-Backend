namespace CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints.Office.GetOffice;

using Dtos.Office;

public record GetOfficeResponse(
    OfficeDto? Office,
    ErrorDto? Erro
);