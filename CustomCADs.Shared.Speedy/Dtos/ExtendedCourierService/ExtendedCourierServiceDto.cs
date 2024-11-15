namespace CustomCADs.Shared.Speedy.Dtos.ExtendedCourierService;

using CourierService;
using Enums;

public record ExtendedCourierServiceDto(
    string Deadline,

    // Copied from CourierServiceDto
    int Id,
    string Name,
    string NameEn,
    CargoType CargoType,
    bool RequireParcelWeight,
    bool RequireParcelSize,
    AdditionalCourierServicesDto AdditionalServices
);
