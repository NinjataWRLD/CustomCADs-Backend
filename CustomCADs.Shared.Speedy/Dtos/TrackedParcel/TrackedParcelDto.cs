﻿namespace CustomCADs.Shared.Speedy.Dtos.TrackedParcel;

using Errors;
using ExternalCarrierParcelNumberDetails;
using TrackedParcelOperation;

public record TrackedParcelDto(
    string ParcelId,
    string[]? ExternalCarrierParcelNumbers,
    TrackedParcelOperationDto[] Operations,
    Dictionary<string, ExternalCarrierParcelNumberDetailsDto>? ExternalCarrierParcelNumbersDetails,
    ErrorDto? Error
);
