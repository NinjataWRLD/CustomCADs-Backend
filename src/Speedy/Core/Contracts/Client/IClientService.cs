using CustomCADs.Speedy.Core.Services.Client.Models;
using CustomCADs.Speedy.Core.Services.Models;

namespace CustomCADs.Speedy.Core.Contracts.Client;

internal interface IClientService
{
	Task<ContactInfoModel> ContractInfoAsync(SpeedyAccount account, CancellationToken ct = default);
	Task<long> CreateContactAsync(SpeedyAccount account, string externalContactId, PhoneNumberModel phone1, string clientName, bool privatePerson, ShipmentAddressModel address, string? secretKey = null, PhoneNumberModel? phone2 = null, string? objectName = null, string? email = null, CancellationToken ct = default);
	Task<ClientModel> GetClientAsync(SpeedyAccount account, long clientId, CancellationToken ct = default);
	Task<ClientModel> GetContactByExternalIdAsync(SpeedyAccount account, long id, string? key = null, CancellationToken ct = default);
	Task<ClientModel[]> GetContractClientsAsync(SpeedyAccount account, CancellationToken ct = default);
	Task<long> GetOwnClientIdAsync(SpeedyAccount account, CancellationToken ct = default);
}
