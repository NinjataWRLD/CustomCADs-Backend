using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Client.GetCadUrlGet;

public sealed class GetCustomCadPresignedUrlGetHandler(ICustomReads reads, IRequestSender sender)
    : IQueryHandler<GetCustomCadPresignedUrlGetQuery, GetCustomCadPresignedUrlGetDto>
{
    public async Task<GetCustomCadPresignedUrlGetDto> Handle(GetCustomCadPresignedUrlGetQuery req, CancellationToken ct)
    {
        Custom custom = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Custom>.ById(req.Id);

        if (custom.BuyerId != req.BuyerId)
            throw CustomAuthorizationException<Custom>.Custom($"Cannot access another Buyer's Custom: {custom.Id}.");

        if (custom.CompletedCustom is null)
            throw CustomStatusException<Custom>.Custom($"Custom is not completed: {custom.Id}.");

        GetCadPresignedUrlGetByIdQuery query = new(custom.FinishedCustom!.CadId);
        var (Url, ContetType) = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        return new(
            PresignedUrl: Url,
            ContentType: ContetType
        );
    }
}
