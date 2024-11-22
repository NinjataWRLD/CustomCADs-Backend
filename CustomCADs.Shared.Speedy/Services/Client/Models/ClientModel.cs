using CustomCADs.Shared.Speedy.Services.Models;

namespace CustomCADs.Shared.Speedy.Services.Client.Models;

public record ClientModel(
    long ClientId,
    string ClientName,
    string ObjectName,
    string ContactName,
    AddressModel Address,
    string Email,
    bool PrivatePerson
);
