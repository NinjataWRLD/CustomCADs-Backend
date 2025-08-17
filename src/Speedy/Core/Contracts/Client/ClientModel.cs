using CustomCADs.Speedy.Core.Models;

namespace CustomCADs.Speedy.Core.Contracts.Client;

public record ClientModel(
	long ClientId,
	string ClientName,
	string ObjectName,
	string ContactName,
	AddressModel Address,
	string Email,
	bool PrivatePerson
);
