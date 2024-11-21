using Refit;

namespace CustomCADs.Shared.Speedy.API.Endpoints.ClientEndpoints;

using ContractInfo;
using CreateContact;
using GetClient;
using GetContactByExternalId;
using GetContractClients;
using GetOwnClientId;

public interface IClientEndpoints
{
    [Post("{id}")]
    Task<GetClientResponse> GetClientAsync(long id, GetClientRequest request, CancellationToken ct = default);

    [Post("contract")]
    Task<GetContractClientsResponse> GetContractClientsAsync(GetContractClientsRequest request, CancellationToken ct = default);

    [Post("contact")]
    Task<CreateContactResponse> CreateContactAsync(CreateContactRequest request, CancellationToken ct = default);

    [Post("contact/external/{id}")]
    Task<GetContactByExternalIdResponse> GetContactByExternalIdAsync(long id, GetContactByExternalIdRequest request, CancellationToken ct = default);

    [Post("")]
    Task<GetOwnClientIdResponse> GetOwnClientIdAsync(GetOwnClientIdRequest request, CancellationToken ct = default);

    [Post("contact/info")]
    Task<ContractInfoResponse> ContractInfoAsync(ContractInfoRequest request, CancellationToken ct = default);
}
