namespace CustomCADs.Shared.Speedy.ShipmentService.Dtos;

public record Sender(
    long ClientId,
    string ClientName,
    string ObjectName,
    string ContactName,
    Address Address,
    string Email,
    bool PrivatePerson,
    int DropoffOfficeId,
    int DropoffGeoPUDOId
);
