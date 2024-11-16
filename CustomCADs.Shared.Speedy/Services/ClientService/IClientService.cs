using Refit;

namespace CustomCADs.Shared.Speedy.Services.ClientService;

using ContractInfo;
using CreateContact;
using GetClient;
using GetContactByExternalId;
using GetContractClients;
using GetOwnClientId;

public interface IClientService
{
    [Post("{id}")]
    Task<GetClientResponse> GetClient(long id, GetClientRequest request, CancellationToken ct = default);

    [Post("contract")]
    Task<GetContractClientsResponse> GetContractClients(GetContractClientsRequest request, CancellationToken ct = default);

    [Post("contact")]
    Task<CreateContactResponse> CreateContact(CreateContactRequest request, CancellationToken ct = default);

    [Post("contact/external/{id}")]
    Task<GetContactByExternalIdResponse> GetContactByExternalId(long id, GetContactByExternalIdRequest request, CancellationToken ct = default);

    [Post("")]
    Task<GetOwnClientIdResponse> GetOwnClientId(GetOwnClientIdRequest request, CancellationToken ct = default);

    [Post("contact/info")]
    Task<ContractInfoResponse> ContractInfo(ContractInfoRequest request, CancellationToken ct = default);
}
