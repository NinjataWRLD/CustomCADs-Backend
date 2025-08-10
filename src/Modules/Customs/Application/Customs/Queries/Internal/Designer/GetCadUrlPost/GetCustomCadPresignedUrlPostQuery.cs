using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Designer.GetCadUrlPost;

public record GetCustomCadPresignedUrlPostQuery(
	CustomId Id,
	UploadFileRequest Cad,
	AccountId DesignerId
) : IQuery<UploadFileResponse>;
