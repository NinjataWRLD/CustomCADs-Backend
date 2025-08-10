namespace CustomCADs.Shared.Application.Dtos.Files;

public record UploadFileRequest(
	string ContentType,
	string FileName
);
