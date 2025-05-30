using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Designer.GetCadUrlPost;

public record GetCustomCadPresignedUrlPostQuery(
	CustomId Id,
	UploadFileRequest Cad,
	AccountId DesignerId
) : IQuery<UploadFileResponse>;
