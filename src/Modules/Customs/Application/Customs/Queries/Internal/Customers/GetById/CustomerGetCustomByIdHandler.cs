using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.GetById;

public sealed class CustomerGetCustomByIdHandler(ICustomReads reads, IRequestSender sender)
    : IQueryHandler<CustomerGetCustomByIdQuery, CustomerGetCustomByIdDto>
{
    public async Task<CustomerGetCustomByIdDto> Handle(CustomerGetCustomByIdQuery req, CancellationToken ct)
    {
        Custom custom = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Custom>.ById(req.Id);

        if (custom.BuyerId != req.BuyerId)
            throw CustomAuthorizationException<Custom>.ById(req.Id);

        if (custom.AcceptedCustom is null)
        {
            return custom.ToCustomerGetByIdDto(
                accepted: null,
                finished: null,
                completed: null
            );
        }

        string designer = await sender.SendQueryAsync(
            new GetUsernameByIdQuery(custom.AcceptedCustom.DesignerId),
            ct
        ).ConfigureAwait(false);
        
        return custom.ToCustomerGetByIdDto(
            accepted: custom.AcceptedCustom.ToDto(designer),
            finished: custom.FinishedCustom?.ToDto(),
            completed: custom.CompletedCustom?.ToDto()
        );
    }
}
