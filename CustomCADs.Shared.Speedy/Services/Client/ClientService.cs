using CustomCADs.Shared.Speedy.API.Endpoints.ClientEndpoints;
using CustomCADs.Shared.Speedy.Services.Client.Models;
using CustomCADs.Shared.Speedy.Services.Models;

namespace CustomCADs.Shared.Speedy.Services.Client;

public class ClientService(IClientEndpoints endpoints)
{
    public async Task<long> GetOwnClientIdAsync(
        AccountModel account,
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
        AccountModel account,
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
        AccountModel account,
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
        AccountModel account,
        CancellationToken ct = default)
    {
        var response = await endpoints.GetContractClientsAsync(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();

        return response.Clients!.Select(c => new ClientModel(
            ClientId: c.ClientId,
            ClientName: c.ClientName,
            ObjectName: c.ObjectName,
            ContactName: c.ContactName,
            Address: c.Address.ToModel(),
            Email: c.Email,
            PrivatePerson: c.PrivatePerson
        )).ToArray();
    }

    public async Task<ContractModel> ContractInfoAsync(
        AccountModel account,
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
            response.Id,
            response.AdministrativeFeeAllowed,
            response.SpecialDeliveryRequirements?.ToModel(),
            response.Cod?.ToModel()
        );
    }

    public async Task<long> CreateContactAsync(ContactModel contact, AccountModel account, CancellationToken ct = default)
    {
        var response = await endpoints.CreateContactAsync(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            ExternalContactId: contact.ExternalContactId,
            Phone1: contact.Phone1.ToDto(),
            ClientName: contact.ClientName,
            PrivatePerson: contact.PrivatePerson,
            Address: contact.Address.ToDto(),
            SecretKey: contact.SecretKey,
            Phone2: contact.Phone2?.ToDto(),
            ObjectName: contact.ObjectName,
            Email: contact.Email
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return response.ClientId;
    }
}
