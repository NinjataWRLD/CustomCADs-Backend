using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Customs;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Designer.GetCadUrlPost;

public record GetCustomCadPresignedUrlPostQuery(
    CustomId Id,
    string ContentType,
    string FileName,
    AccountId DesignerId
) : IQuery<GetCustomCadPresignedUrlPostDto>;
