using CustomCADs.Speedy.Core.Services.Models;

namespace CustomCADs.Speedy.Core.Services.Client.Models;

public record ClientModel(
	long ClientId,
	string ClientName,
	string ObjectName,
	string ContactName,
	AddressModel Address,
	string Email,
	bool PrivatePerson
);
