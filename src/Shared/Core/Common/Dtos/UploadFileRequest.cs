namespace CustomCADs.Shared.Core.Common.Dtos;

public record UploadFileRequest(
	string ContentType,
	string FileName
);
