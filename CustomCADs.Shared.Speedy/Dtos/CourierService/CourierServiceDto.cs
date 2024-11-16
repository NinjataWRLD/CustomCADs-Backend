namespace CustomCADs.Shared.Speedy.Dtos.CourierService;

public record CourierServiceDto(
    int Id,
    string Name,
    string NameEn,
    CargoType CargoType,
    bool RequireParcelWeight,
    bool RequireParcelSize,
    AdditionalCourierServicesDto AdditionalServices
);
