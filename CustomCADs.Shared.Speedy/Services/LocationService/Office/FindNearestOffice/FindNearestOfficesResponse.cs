﻿namespace CustomCADs.Shared.Speedy.Services.LocationService.Office.FindNearestOffice;

using Dtos.Errors;
using Dtos.SpecialDeliveryRequirements;

public record FindNearestOfficesResponse(
    OfficeResultDto[]? Offices,
    double? X,
    double? Y,
    ErrorDto? Error
);
