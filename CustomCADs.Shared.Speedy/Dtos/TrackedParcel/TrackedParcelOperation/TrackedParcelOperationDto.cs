namespace CustomCADs.Shared.Speedy.Dtos.TrackedParcel.TrackedParcelOperation;

public record TrackedParcelOperationDto(
    string DateTime,
    int OperationCode,
    string Description,
    string? Place,
    string? Comment,
    string[] ExceptionCodes,
    string? ReturnShipmentId,
    string? RedirectShipmentId,
    string? Consignee,
    string? PodImageURL,
    TrackedParcelOperationAdditionalInfoDto? AdditionalInfo
);
