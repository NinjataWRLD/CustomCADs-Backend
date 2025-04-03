using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Designer.GetCadUrlPost;

public class GetCustomCadPresignedUrlPostHandler(ICustomReads reads, IRequestSender sender)
    : IQueryHandler<GetCustomCadPresignedUrlPostQuery, GetCustomCadPresignedUrlPostDto>
{
    public async Task<GetCustomCadPresignedUrlPostDto> Handle(GetCustomCadPresignedUrlPostQuery req, CancellationToken ct)
    {
        Custom custom = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Custom>.ById(req.Id);

        if (custom.AcceptedCustom?.DesignerId != req.DesignerId)
            throw CustomAuthorizationException<Custom>.ById(custom.Id);

        GetCadPresignedUrlPostByIdQuery cadQuery = new(
            Name: custom.Name,
            ContentType: req.ContentType,
            FileName: req.FileName
        );
        var (Key, Url) = await sender.SendQueryAsync(cadQuery, ct).ConfigureAwait(false);

        return new(
            GeneratedKey: Key,
            PresignedUrl: Url
        );
    }
}
