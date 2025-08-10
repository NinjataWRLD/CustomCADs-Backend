namespace CustomCADs.Shared.Application.Dtos.Files;

public record DownloadFileResponse(
	string PresignedUrl,
	string ContentType
);
