namespace CustomCADs.Shared.Core.Common.Dtos;

public record DownloadFileResponse(
	string PresignedUrl,
	string ContentType
);
