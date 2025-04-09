using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.GetById;

public sealed class ClientGetCustomByIdHandler(ICustomReads reads, IRequestSender sender)
    : IQueryHandler<ClientGetCustomByIdQuery, CustomerGetCustomByIdDto>
{
    public async Task<CustomerGetCustomByIdDto> Handle(ClientGetCustomByIdQuery req, CancellationToken ct)
    {
        Custom custom = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Custom>.ById(req.Id);

        if (custom.BuyerId != req.BuyerId)
            throw CustomAuthorizationException<Custom>.ById(req.Id);

        string timeZone = await sender.SendQueryAsync(
            new GetTimeZoneByIdQuery(Id: custom.BuyerId),
            ct
        ).ConfigureAwait(false);

        string? designer = null;
        if (custom.AcceptedCustom is not null)
        {
            designer = await sender.SendQueryAsync(
                new GetUsernameByIdQuery(custom.AcceptedCustom.DesignerId),
                ct
            ).ConfigureAwait(false);
        }

        return custom.ToCustomerGetByIdDto(timeZone, designer);
    }
}
