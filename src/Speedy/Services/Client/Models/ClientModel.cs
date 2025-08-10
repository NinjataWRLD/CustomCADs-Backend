using CustomCADs.Speedy.Services.Models;

namespace CustomCADs.Speedy.Services.Client.Models;

public record ClientModel(
	long ClientId,
	string ClientName,
	string ObjectName,
	string ContactName,
	AddressModel Address,
	string Email,
	bool PrivatePerson
);
