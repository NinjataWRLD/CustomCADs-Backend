using CustomCADs.Speedy.Core.Services.Client.Models;
using CustomCADs.Speedy.Core.Services.Models;

namespace CustomCADs.Speedy.Core.Contracts.Client;

internal interface IClientService
{
	Task<ContactInfoModel> ContractInfoAsync(AccountModel account, CancellationToken ct = default);
	Task<long> CreateContactAsync(AccountModel account, string externalContactId, PhoneNumberModel phone1, string clientName, bool privatePerson, ShipmentAddressModel address, string? secretKey = null, PhoneNumberModel? phone2 = null, string? objectName = null, string? email = null, CancellationToken ct = default);
	Task<ClientModel> GetClientAsync(AccountModel account, long clientId, CancellationToken ct = default);
	Task<ClientModel> GetContactByExternalIdAsync(AccountModel account, long id, string? key = null, CancellationToken ct = default);
	Task<ClientModel[]> GetContractClientsAsync(AccountModel account, CancellationToken ct = default);
	Task<long> GetOwnClientIdAsync(AccountModel account, CancellationToken ct = default);
}
