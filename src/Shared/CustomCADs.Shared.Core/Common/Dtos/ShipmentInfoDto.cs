namespace CustomCADs.Shared.Core.Common.Dtos;

public record ShipmentInfoDto(
    int Count,
    double Weight,
    string Recipient
);
