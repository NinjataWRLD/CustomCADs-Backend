namespace CustomCADs.Shared.Core.Common.Dtos;

public record UploadFileResponse(
	string GeneratedKey,
	string PresignedUrl
);
