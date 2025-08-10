namespace CustomCADs.Shared.Application.Dtos.Files;

public record UploadFileResponse(
	string GeneratedKey,
	string PresignedUrl
);
