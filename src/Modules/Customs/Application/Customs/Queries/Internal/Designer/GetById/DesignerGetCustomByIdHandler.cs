using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using System.ComponentModel.Design;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Designer.GetById;

public sealed class DesignerGetCustomByIdHandler(ICustomReads reads, IRequestSender sender)
    : IQueryHandler<DesignerGetCustomByIdQuery, DesignerGetCustomByIdDto>
{
    public async Task<DesignerGetCustomByIdDto> Handle(DesignerGetCustomByIdQuery req, CancellationToken ct)
    {
        Custom custom = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Custom>.ById(req.Id);

        if (custom.CustomStatus is not CustomStatus.Pending
            && custom.AcceptedCustom?.DesignerId != req.DesignerId)
            throw CustomAuthorizationException<Custom>.ById(req.Id);

        string buyer = await sender.SendQueryAsync(
            new GetUsernameByIdQuery(custom.BuyerId),
            ct
        ).ConfigureAwait(false);

        if (custom.AcceptedCustom is null)
        {
            return custom.ToDesignerGetByIdDto(
                buyer: buyer,
                accepted: null,
                finished: null,
                completed: null
            );
        }

        string designer = await sender.SendQueryAsync(
            new GetUsernameByIdQuery(custom.AcceptedCustom.DesignerId),
            ct
        ).ConfigureAwait(false);

        return custom.ToDesignerGetByIdDto(
            buyer: buyer,
            accepted: custom.AcceptedCustom.ToDto(designer),
            finished: custom.FinishedCustom?.ToDto(),
            completed: custom.CompletedCustom?.ToDto()
        );
    }
}
