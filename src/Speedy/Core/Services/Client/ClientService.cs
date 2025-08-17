using CustomCADs.Speedy.Http.Endpoints.ClientEndpoints;
using CustomCADs.Speedy.Core.Models;
using CustomCADs.Speedy.Core.Contracts.Client;

namespace CustomCADs.Speedy.Core.Services.Client;


internal class ClientService(IClientEndpoints endpoints) : IClientService
{
	public async Task<long> GetOwnClientIdAsync(
		SpeedyAccount account,
		CancellationToken ct = default)
	{
		var response = await endpoints.GetOwnClientIdAsync(new(
			UserName: account.Username,
			Password: account.Password,
			Language: account.Language,
			ClientSystemId: account.ClientSystemId
		), ct).ConfigureAwait(false);

		response.Error.EnsureNull();
		return response.ClientId;
	}

	public async Task<ClientModel> GetClientAsync(
		SpeedyAccount account,
		long clientId,
		CancellationToken ct = default)
	{
		var response = await endpoints.GetClientAsync(clientId, new(
			UserName: account.Username,
			Password: account.Password,
			Language: account.Language,
			ClientSystemId: account.ClientSystemId
		), ct).ConfigureAwait(false);

		response.Error.EnsureNull();
		if (response.Client is null)
		{
			throw new("No client with such id.");
		}

		return new(
			ClientId: response.Client.ClientId,
			ClientName: response.Client.ClientName,
			ObjectName: response.Client.ObjectName,
			ContactName: response.Client.ContactName,
			Address: response.Client.Address.ToModel(),
			Email: response.Client.Email,
			PrivatePerson: response.Client.PrivatePerson
		);
	}

	public async Task<ClientModel> GetContactByExternalIdAsync(
		SpeedyAccount account,
		long id,
		string? key = null,
		CancellationToken ct = default)
	{
		var response = await endpoints.GetContactByExternalIdAsync(id, new(
			UserName: account.Username,
			Password: account.Password,
			Langauge: account.Language,
			ClientSystemId: account.ClientSystemId,
			SecretKey: key
		), ct).ConfigureAwait(false);

		response.Error.EnsureNull();
		return new(
			ClientId: response.Client!.ClientId,
			ClientName: response.Client!.ClientName,
			ObjectName: response.Client!.ObjectName,
			ContactName: response.Client!.ContactName,
			Address: response.Client!.Address.ToModel(),
			Email: response.Client!.Email,
			PrivatePerson: response.Client!.PrivatePerson
		);
	}

	public async Task<ClientModel[]> GetContractClientsAsync(
		SpeedyAccount account,
		CancellationToken ct = default)
	{
		var response = await endpoints.GetContractClientsAsync(new(
			UserName: account.Username,
			Password: account.Password,
			Language: account.Language,
			ClientSystemId: account.ClientSystemId
		), ct).ConfigureAwait(false);

		response.Error.EnsureNull();

		return [.. response.Clients!.Select(c => new ClientModel(
			ClientId: c.ClientId,
			ClientName: c.ClientName,
			ObjectName: c.ObjectName,
			ContactName: c.ContactName,
			Address: c.Address.ToModel(),
			Email: c.Email,
			PrivatePerson: c.PrivatePerson
		))];
	}

	public async Task<ContactInfoModel> ContractInfoAsync(
		SpeedyAccount account,
		CancellationToken ct = default)
	{
		var response = await endpoints.ContractInfoAsync(new(
			UserName: account.Username,
			Password: account.Password,
			Language: account.Language,
			ClientSystemId: account.ClientSystemId
		), ct).ConfigureAwait(false);

		response.Error.EnsureNull();
		return new(
			Id: response.Id,
			AdministrativeFeeAllowed: response.AdministrativeFeeAllowed,
			SpecialDeliveryRequirements: response.SpecialDeliveryRequirements?.ToModel(),
			Cod: response.Cod?.ToModel()
		);
	}

	public async Task<long> CreateContactAsync(
		SpeedyAccount account,
		string externalContactId,
		PhoneNumberModel phone1,
		string clientName,
		bool privatePerson,
		ShipmentAddressModel address,
		string? secretKey = null,
		PhoneNumberModel? phone2 = null,
		string? objectName = null,
		string? email = null,
		CancellationToken ct = default)
	{
		var response = await endpoints.CreateContactAsync(new(
			UserName: account.Username,
			Password: account.Password,
			Language: account.Language,
			ClientSystemId: account.ClientSystemId,
			ExternalContactId: externalContactId,
			Phone1: phone1.ToDto(),
			ClientName: clientName,
			PrivatePerson: privatePerson,
			Address: address.ToDto(),
			SecretKey: secretKey,
			Phone2: phone2?.ToDto(),
			ObjectName: objectName,
			Email: email
		), ct).ConfigureAwait(false);

		response.Error.EnsureNull();
		return response.ClientId;
	}
}
