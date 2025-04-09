using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Designer.GetCadUrlPost;

public class GetCustomCadPresignedUrlPostHandler(ICustomReads reads, IRequestSender sender)
    : IQueryHandler<GetCustomCadPresignedUrlPostQuery, UploadFileResponse>
{
    public async Task<UploadFileResponse> Handle(GetCustomCadPresignedUrlPostQuery req, CancellationToken ct)
    {
        Custom custom = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Custom>.ById(req.Id);

        if (custom.AcceptedCustom?.DesignerId != req.DesignerId)
            throw CustomAuthorizationException<Custom>.ById(custom.Id);

        return await sender.SendQueryAsync(
            new GetCadPresignedUrlPostByIdQuery(
                Name: custom.Name,
                File: req.Cad
            ),
            ct
        ).ConfigureAwait(false);
    }
}
